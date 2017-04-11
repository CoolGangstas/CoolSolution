using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Database_Services
{
    using System.Collections;
    using System.Net.Http;

    using BLL.Cloud_Services;
    using BLL.Entities;
    using BLL.Mappers;

    using DAL.Interfaces;
    using DAL.Repositories;

    /// <summary>
    /// The database record service.
    /// </summary>
    public class RecordDbService
    {
        /// <summary>
        /// The record repository.
        /// </summary>
        private readonly RecordRepository recordRepository;

        /// <summary>
        /// The cloud service.
        /// </summary>
        private readonly RecordCloudService cloudService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordDbService"/> class.
        /// </summary>
        public RecordDbService()
        {
            this.cloudService = new RecordCloudService();
            this.recordRepository = new RecordRepository();
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public void Create(RecordEntity entity)
        {
            int recordId = this.recordRepository.Create(entity.ToDalRecord());
            this.CreateInCloudAsync(entity.ToCloudRecordModel(), entity.Id);
        }

        /// <summary>
        /// Get all records by id.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<RecordEntity> GetAllById(int userId)
        {
            return this.recordRepository.GetUserRecords(userId).Select(x => x.ToRecordEntity());
        }

        /// <summary>
        /// Updates record in database.
        /// </summary>
        /// <param name="recordEntity">
        /// The record Entity.
        /// </param>
        public void Update(RecordEntity recordEntity)
        {
            this.recordRepository.Update(recordEntity.Id, recordEntity.IsCompleted);
            this.UpdateInCloudAsync(recordEntity.ToCloudRecordModel());
        }

        /// <summary>
        /// Deletes record from database.
        /// </summary>
        /// <param name="recordEntity">
        /// The record Entity.
        /// </param>
        public void Delete(RecordEntity recordEntity)
        {
            this.recordRepository.Delete(recordEntity.Id);
            this.DeleteInCloudAsync(recordEntity.CloudId);
        }

        /// <summary>
        /// Finds id in array.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        /// <param name="targetId">
        /// The target id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int FindId(IList<CloudRecordModel> entities, int targetId)
        {
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                int id = Convert.ToInt32(entities[i].Name.Split((char)007)[1]);
                if (id == targetId)
                {
                    return entities[i].ToDoId;
                }
            }

            return 0;
        }

        /// <summary>
        /// The create in cloud async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="targetId">
        /// The target Id.
        /// </param>
        private async void CreateInCloudAsync(CloudRecordModel entity, int targetId)
        {
            HttpResponseMessage message = await this.cloudService.CreateItem(entity);
            IList<CloudRecordModel> entities = this.cloudService.GetItems(entity.UserId);
            int cloudId = FindId(entities, targetId);
            this.recordRepository.UpdateCloudId(targetId, cloudId);
        }

        /// <summary>
        /// The update in cloud async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        private async void UpdateInCloudAsync(CloudRecordModel entity)
        {
            await this.cloudService.UpdateItem(entity);
        }

        /// <summary>
        /// Deletes in cloud async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        private async void DeleteInCloudAsync(int id)
        {
            await this.cloudService.DeleteItem(id);
        }
    }
}

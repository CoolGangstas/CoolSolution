using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    using System.Net.Http;

    using BLL.Cloud_Services;
    using BLL.Database_Services;
    using BLL.Entities;
    using BLL.Mappers;

    /// <summary>
    /// The record service.
    /// </summary>
    public class RecordService
    {
        /// <summary>
        /// The cloud service.
        /// </summary>
        private readonly RecordCloudService cloudService;

        /// <summary>
        /// The database service.
        /// </summary>
        private readonly RecordDbService dbService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordService"/> class.
        /// </summary>
        public RecordService()
        {
            this.cloudService = new RecordCloudService();
            this.dbService = new RecordDbService();
        }

        /// <summary>
        /// Creates new record.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public void Create(RecordEntity entity)
        {
            this.dbService.Create(entity);
            this.CreateInCloudAsync(entity.ToCloudRecordModel(), entity.Id).Start();
        }

        /// <summary>
        /// Returns all records by id.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IEnumerable<RecordEntity> GetAllById(int userId)
        {
            return this.dbService.GetAllById(userId);
        }

        /// <summary>
        /// Updates record entity.
        /// </summary>
        /// <param name="recordEntity">
        /// The record entity.
        /// </param>
        public void Update(RecordEntity recordEntity)
        {
            this.dbService.Update(recordEntity);
            this.UpdateInCloudAsync(recordEntity.ToCloudRecordModel()).Start();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public void Delete(int id)
        {
            var cloudId = this.dbService.Delete(id);
            if (cloudId != null)
            {
                Task task = this.DeleteInCloudAsync(2836);
            }
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
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task CreateInCloudAsync(CloudRecordModel entity, int targetId)
        {
            HttpResponseMessage message = await this.cloudService.CreateItem(entity);
            IList<CloudRecordModel> entities = this.cloudService.GetItems(entity.UserId);
            int cloudId = FindId(entities, targetId);
            this.dbService.UpdateCloudId(targetId, cloudId);
        }

        /// <summary>
        /// The update in cloud async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private Task UpdateInCloudAsync(CloudRecordModel entity)
        {
            return Task.Run(() => this.cloudService.UpdateItem(entity));
        }

        /// <summary>
        /// Deletes in cloud async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private Task DeleteInCloudAsync(int id)
        {
            return Task.Run(() => this.cloudService.DeleteItem(id));
        }
    }
}

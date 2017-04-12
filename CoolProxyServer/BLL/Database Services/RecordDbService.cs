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
    class RecordDbService
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
        public int Create(RecordEntity entity)
        {
            return this.recordRepository.Create(entity.ToDalRecord());
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
        public IEnumerable<RecordEntity> GetAllById(int userId)
        {
            var records = this.recordRepository.GetUserRecords(userId);
            return records.AsEnumerable().Select(x => x.ToRecordEntity());
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
        }

        /// <summary>
        /// Deletes record from database.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int? Delete(int id)
        {
            return this.recordRepository.Delete(id);
        }

        /// <summary>
        /// Update cloud id in database.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="cloudId">
        /// The cloud id.
        /// </param>
        public void UpdateCloudId(int id, int cloudId)
        {
            this.recordRepository.UpdateCloudId(id, cloudId);
        }
    }
}

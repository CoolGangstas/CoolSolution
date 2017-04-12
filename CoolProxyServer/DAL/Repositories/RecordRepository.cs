
namespace DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;

    using DAL.DTO;
    using DAL.Interfaces;
    using DAL.Mappers;

    using ORM;

    /// <summary>
    /// The record repository.
    /// </summary>
    public class RecordRepository : ICloudUpdater
    {

        /// <summary>
        /// The context.
        /// </summary>
        private readonly DbModel context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordRepository"/> class. 
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public RecordRepository()
        {
            this.context = new DbModel();
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="dalRecord">
        /// The dal record.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Create(DalRecord dalRecord)
        {
            Record record = new Record() { Name = dalRecord.Name, UserId = dalRecord.UserId };
            this.context.Set<Record>().Add(record);
            this.context.SaveChanges();
            return record.Id;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="recordId">
        /// The record id.
        /// </param>
        public int? Delete(int recordId)
        {
            Record record = this.context.Set<Record>().Find(recordId);
            if (record != null)
            {
                this.context.Set<Record>().Remove(record);
                this.context.SaveChanges();
                return record.CloudId;
            }
            return null;
        }

        /// <summary>
        /// The get user records.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IEnumerable<DalRecord> GetUserRecords(int userId)
        {
            var recordsList = this.context.Set<Record>().Where(x => x.UserId == userId);
            return recordsList.AsEnumerable().Select(x => x.ToDalRecord());
        }

        /// <summary>
        /// Updates record.
        /// </summary>
        /// <param name="recordId">
        /// The record id.
        /// </param>
        /// <param name="isCompleted">
        /// The is completed.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int? Update(int recordId, bool isCompleted)
        {
            Record record = this.context.Set<Record>().Find(recordId);
            if (record != null)
            {
                record.IsCompleted = isCompleted;
                this.context.SaveChanges();
                return record.CloudId;
            }
            return null;

        }

        /// <summary>
        /// Updates the cloud identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cloudId">The cloud identifier.</param>
        public void UpdateCloudId(int id, int cloudId)
        {
            Record record = this.context.Set<Record>().Find(id);
            if (record != null)
            {
                record.CloudId = cloudId;
                try
                {
                    this.context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}

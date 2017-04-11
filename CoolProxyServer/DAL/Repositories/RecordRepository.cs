
namespace DAL.Repositories
{
    using System.Linq;

    using DAL.DTO;
    using DAL.Interfaces;
    using DAL.Mappers;

    using ORM;

    /// <summary>
    /// The record repository.
    /// </summary>
    public class RecordRepository : IRecordRepository, ICloudUpdater
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
        public RecordRepository(DbModel context)
        {
            this.context = context;
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        public void Create(string name, int userId)
        {
            this.context.Set<Record>().Add(new Record() { Name = name, UserId = userId });
            this.context.SaveChanges();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="recordId">
        /// The record id.
        /// </param>
        public void Delete(int recordId)
        {
            Record record = this.context.Set<Record>().Find(recordId);
            if (record != null)
            {
                this.context.Set<Record>().Remove(record);
            }
            this.context.SaveChanges();
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
        public IQueryable<DalRecord> GetUserRecords(int userId)
        {
            return this.context.Set<Record>().Where(x => x.UserId == userId).Select(x => x.ToDalRecord());
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
        public void Update(int recordId, bool isCompleted)
        {
            Record record = this.context.Set<Record>().Find(recordId);
            if (record != null)
            {
                record.IsCompleted = isCompleted;
            }

            this.context.SaveChanges();
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
            }
        }
    }
}

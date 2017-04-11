
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
            Record user = new Record() { Name = dalRecord.Name, UserId = dalRecord.UserId };
            this.context.Set<Record>().Add(user);
            this.context.SaveChanges();
            return user.Id;
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

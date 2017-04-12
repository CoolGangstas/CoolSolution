using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    using DAL.DTO;

    /// <summary>
    /// The RecordRepository interface.
    /// </summary>
    public interface IRecordRepository
    {
        /// <summary>
        /// Returns user records.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable{DalRecord}"/>.
        /// </returns>
        IEnumerable<DAL.DTO.DalRecord> GetUserRecords(int userId);

        /// <summary>
        /// Creates new <see cref="DalRecord"/>.
        /// </summary>
        /// <param name="record">
        /// The record.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Create(DalRecord record);

        /// <summary>
        /// Updates record.
        /// </summary>
        /// <param name="recordId">
        /// The record id.
        /// </param>
        /// <param name="isCompleted">
        /// The is completed.
        /// </param>
        void Update(int recordId, bool isCompleted);

        /// <summary>
        /// Deletes record.
        /// </summary>
        /// <param name="recordId">
        /// The record id.
        /// </param>
        void Delete(int recordId);
    }
}

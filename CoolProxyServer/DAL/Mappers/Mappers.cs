using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    using DAL.DTO;

    using ORM;

    /// <summary>
    /// The mappers.
    /// </summary>
    public static class Mappers
    {
        /// <summary>
        /// The to dal record.
        /// </summary>
        /// <param name="record">
        /// The record.
        /// </param>
        /// <returns>
        /// The <see cref="DalRecord"/>.
        /// </returns>
        public static DalRecord ToDalRecord(this Record record)
        {
            return new DalRecord()
                       {
                           Id = record.Id,
                           Name = record.Name,
                           CloudId = record.CloudId,
                           IsCompleted = record.IsCompleted
                       };
        }
    }
}

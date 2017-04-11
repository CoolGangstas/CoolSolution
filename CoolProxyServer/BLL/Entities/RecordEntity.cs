using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    /// <summary>
    /// The record entity.
    /// </summary>
    public class RecordEntity
    {
        /// <summary>
        /// Gets or sets the record id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the to do id.
        /// </summary>
        public int CloudId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the record name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether record task is completed.
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}

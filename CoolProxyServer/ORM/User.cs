using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The user.
    /// </summary>
    public class User
    {

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the cloud id.
        /// </summary>
        public int CloudId { get; set; }

        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        public virtual ICollection<Record> Records { get; set; }
    }
}

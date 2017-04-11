using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    using System.Data.Entity;

    /// <summary>
    /// The drop create database.
    /// </summary>
    public class DropCreateDb : DropCreateDatabaseIfModelChanges<DbModel>
    {

        /// <summary>
        /// The seed.
        /// </summary>
        /// <param name="context">
        /// The database context.
        /// </param>
        protected override void Seed(DbModel context)
        {
            base.Seed(context);
        }
    }
}

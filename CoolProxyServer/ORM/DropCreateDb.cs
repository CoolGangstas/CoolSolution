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
        protected override void Seed(DbModel dbModel)
        {
            base.Seed(dbModel);
            dbModel.
        }
    }
}

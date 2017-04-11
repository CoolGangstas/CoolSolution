using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// The user config.
    /// </summary>
    public class UserConfig : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserConfig"/> class.
        /// </summary>
        public UserConfig()
            : base()
        {
            this.HasMany(x => x.Records).WithRequired(x => x.User).WillCascadeOnDelete(true);
        }
    }
}

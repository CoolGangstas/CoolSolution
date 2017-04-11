namespace ORM
{
    using System.Data.Entity;

    /// <summary>
    /// The database model.
    /// </summary>
    public class DbModel : DbContext
    {
        /// <summary>
        /// Initializes static members of the <see cref="DbModel"/> class.
        /// </summary>
        static DbModel()
        {
            System.Data.Entity.Database.SetInitializer(new DropCreateDb());
        }
    }
}
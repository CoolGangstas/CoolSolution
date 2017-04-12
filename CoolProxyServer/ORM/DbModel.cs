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

        public DbModel()
            : base("name=CloudInfoDb")
        {
        }

        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        public virtual DbSet<Record> Records { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserConfig());
        }
    }
}
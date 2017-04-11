﻿
namespace DAL.Repositories
{
    using DAL.Interfaces;

    using ORM;

    /// <summary>
    /// The user repository.
    /// </summary>
    public class UserRepository : IUserRepository,ICloudUpdater
    {
        /// <summary>
        /// The context.
        /// </summary>
        private readonly DbModel context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public UserRepository(DbModel context)
        {
            this.context = context;
        }

        /// <summary>
        /// The create user.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CreateUser()
        {
            return this.context.Set<User>().Add(new User()).Id;
        }

        /// <summary>
        /// Updates the cloud identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cloudId">The cloud identifier.</param>
        public void UpdateCloudId(int id, int cloudId)
        {
            User user = this.context.Set<User>().Find(id);
            if (user != null)
            {
                user.CloudId = cloudId;
            }
            this.context.SaveChanges();
        }
    }

}

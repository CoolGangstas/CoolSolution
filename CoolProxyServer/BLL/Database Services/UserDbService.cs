using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    using DAL.Interfaces;
    using DAL.Repositories;

    /// <summary>
    /// The user database service.
    /// </summary>
    public class UserDbService
    {
        /// <summary>
        /// The user repository.
        /// </summary>
        private readonly UserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDbService"/> class.
        /// </summary>
        public UserDbService()
        {
            this.userRepository = new UserRepository();
        }

        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="name">
        /// The users name.
        /// </param>
        /// <returns>
        /// The new user id.
        /// </returns>
        public int CreateUser(string name)
        {
            return this.userRepository.CreateUser(name);
        }

        /// <summary>
        /// Updates cloud user id in database.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="cloudId">
        /// The cloud id.
        /// </param>
        public void UpdateCloudId(int id, int cloudId)
        {
            this.userRepository.UpdateCloudId(id, cloudId);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    using System.Net.Http;

    using BLL.Cloud_Services;
    using BLL.Database_Services;
    using BLL.Entities;

    /// <summary>
    /// The user service.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// The database service.
        /// </summary>
        private readonly UserDbService databaseService;

        /// <summary>
        /// The cloud service.
        /// </summary>
        private readonly UserCloudService cloudService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService()
        {
            this.cloudService = new UserCloudService();
            this.databaseService = new UserDbService();
        }

        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CreateUser(string name)
        {
            int recordId = this.databaseService.CreateUser(name);
            this.CreateCloudUserAsync(name, recordId);
            return recordId;
        }

        /// <summary>
        /// Creates cloud user async.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="localId">
        /// The local id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private Task CreateCloudUserAsync(string name, int localId)
        {
            return Task.Run(() => this.AsyncStore(name,localId));
        }

        /// <summary>
        /// The async storage.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="localId">
        /// The local id.
        /// </param>
        private void AsyncStore(string name, int localId)
        {
            var response = this.cloudService.CreateUser(name);
            int userId = Convert.ToInt32(response.Result.Content.ReadAsStringAsync().Result);
            this.databaseService.UpdateCloudId(localId, userId);
        }
    }
}

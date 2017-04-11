using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Cloud_Services
{
    using System.Configuration;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Script.Serialization;

    using Newtonsoft.Json;

    /// <summary>
    /// Works with Users backend.
    /// </summary>
    public class UserCloudService
    {
        /// <summary>
        /// The url for users' creation.
        /// </summary>
        private const string CreateUrl = "Users";

        /// <summary>
        /// The service URL.
        /// </summary>
        private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

        /// <summary>
        /// The http client.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCloudService"/> class. 
        /// </summary>
        public UserCloudService()
        {
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The User Id.</returns>
        public int CreateUser(string userName)
        {
            var response =
                this.httpClient.PostAsync(
                        this.serviceApiUrl + CreateUrl,
                        new StringContent(new JavaScriptSerializer().Serialize(userName), Encoding.UTF8, "application/json"))
                    .Result;
            response.EnsureSuccessStatusCode();
            return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
        }
    }
}

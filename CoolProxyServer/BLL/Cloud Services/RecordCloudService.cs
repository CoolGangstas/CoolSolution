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

    using BLL.Entities;

    using Newtonsoft.Json;

    /// <summary>
    /// The record cloud service.
    /// </summary>
    class RecordCloudService
    {
        /// <summary>
        /// The url for getting all records.
        /// </summary>
        private const string GetAllUrl = "ToDos?userId={0}";

        /// <summary>
        /// The url for updating a record.
        /// </summary>
        private const string UpdateUrl = "ToDos";

        /// <summary>
        /// The url for a record creation.
        /// </summary>
        private const string CreateUrl = "ToDos";

        /// <summary>
        /// The url for a record deletion.
        /// </summary>
        private const string DeleteUrl = "ToDos/{0}";

        /// <summary>
        /// The service URL.
        /// </summary>
        private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

        /// <summary>
        /// The http client.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordCloudService"/> class. 
        /// </summary>
        public RecordCloudService()
        {
            this.httpClient = new HttpClient();
        }

        /// <summary>
        /// Gets all records for the user.
        /// </summary>
        /// <param name="userId">The User Id.</param>
        /// <returns>The list of records.</returns>
        public IList<CloudRecordModel> GetItems(int userId)
        {
            var dataAsString = this.httpClient.GetStringAsync(string.Format(this.serviceApiUrl + GetAllUrl, userId)).Result;
            return JsonConvert.DeserializeObject<IList<CloudRecordModel>>(dataAsString);
        }

        /// <summary>
        /// Creates a record. UserId is taken from the model.
        /// </summary>
        /// <param name="item">
        /// The record to create.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<HttpResponseMessage> CreateItem(CloudRecordModel item)
        {
            var message = this.httpClient.PostAsync(
                this.serviceApiUrl + CreateUrl,
                new StringContent(new JavaScriptSerializer().Serialize(item), Encoding.UTF8, "application/json"));
            message.Result.EnsureSuccessStatusCode();
            return message;
        }

        /// <summary>
        /// Updates a record.
        /// </summary>
        /// <param name="item">
        /// The record to update.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<HttpResponseMessage> UpdateItem(CloudRecordModel item)
        {
            var httpMessage = this.httpClient.PutAsync(
                this.serviceApiUrl + UpdateUrl,
                new StringContent(new JavaScriptSerializer().Serialize(item), Encoding.UTF8, "application/json"));
            try
            {
                httpMessage.Result.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                await Task.Delay(10000);
                httpMessage = this.httpClient.PutAsync(
                this.serviceApiUrl + UpdateUrl,
                new StringContent(new JavaScriptSerializer().Serialize(item), Encoding.UTF8, "application/json"));
                httpMessage.Result.EnsureSuccessStatusCode();
            }
                
            return httpMessage.Result;
        }

        /// <summary>
        /// Deletes a record.
        /// </summary>
        /// <param name="id">
        /// The record Id to delete.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<HttpResponseMessage> DeleteItem(int id)
        {
            var message = this.httpClient.DeleteAsync(string.Format(this.serviceApiUrl + DeleteUrl, id));
            try
            {
                message.Result.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                await Task.Delay(10000);
                message = this.httpClient.DeleteAsync(string.Format(this.serviceApiUrl + DeleteUrl, id));
                message.Result.EnsureSuccessStatusCode();
            }
            return message.Result;
        }
    }
}

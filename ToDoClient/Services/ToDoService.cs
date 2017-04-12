using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ToDoClient.Models;

namespace ToDoClient.Services
{
    using System;

    /// <summary>
    /// Works with ToDo backend.
    /// </summary>
    public class ToDoService
    {
        /// <summary>
        /// The api url string.
        /// </summary>
        public readonly string ApiUrlString;

        /// <summary>
        /// The url for getting all todos.
        /// </summary>
        private const string GetAllUrl = "ToDos?userId={0}";

        /// <summary>
        /// The url for updating a todo.
        /// </summary>
        private const string UpdateUrl = "ToDos";

        /// <summary>
        /// The url for a todo's creation.
        /// </summary>
        private const string CreateUrl = "ToDos";

        /// <summary>
        /// The url for a todo's deletion.
        /// </summary>
        private const string DeleteUrl = "Todos/{0}";

        private readonly HttpClient httpClient;

        /// <summary>
        /// Creates the service.
        /// </summary>
        public ToDoService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                this.ApiUrlString = ConfigurationManager.AppSettings["ToDoProxyUrl"];
                //var ping = new System.Net.NetworkInformation.Ping();
                //var result = ping.Send(this.ApiUrlString + "/Ping");
            }
            catch (Exception e)
            {
                this.ApiUrlString = ConfigurationManager.AppSettings["ToDoCloudUrl"];
            }
        }

        /// <summary>
        /// Gets all todos for the user.
        /// </summary>
        /// <param name="userId">The User Id.</param>
        /// <returns>The list of todos.</returns>
        public IList<ToDoItemViewModel> GetItems(int userId)
        {
            string dataAsString;
            dataAsString = httpClient.GetStringAsync(string.Format(this.ApiUrlString + GetAllUrl, userId)).Result;
            return JsonConvert.DeserializeObject<IList<ToDoItemViewModel>>(dataAsString);
        }

        /// <summary>
        /// Creates a todo. UserId is taken from the model.
        /// </summary>
        /// <param name="item">The todo to create.</param>
        public void CreateItem(ToDoItemViewModel item)
        {
            httpClient.PostAsJsonAsync(this.ApiUrlString + CreateUrl, item)
                .Result.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Updates a todo.
        /// </summary>
        /// <param name="item">The todo to update.</param>
        public void UpdateItem(ToDoItemViewModel item)
        {
            httpClient.PutAsJsonAsync(this.ApiUrlString + UpdateUrl, item)
                .Result.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Deletes a todo.
        /// </summary>
        /// <param name="id">The todo Id to delete.</param>
        public void DeleteItem(int id)
        {
            httpClient.DeleteAsync(string.Format(this.ApiUrlString + DeleteUrl, id))
                .Result.EnsureSuccessStatusCode();
        }
    }
}
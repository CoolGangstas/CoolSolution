﻿using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace ToDoClient.Services
{
    /// <summary>
    /// Works with Users backend.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// The url api string.
        /// </summary>
        public readonly string ApiUrlString;

        /// <summary>
        /// The url for users' creation.
        /// </summary>
        private const string CreateUrl = "Users";

        private readonly HttpClient httpClient;
        
        /// <summary>
        /// Creates the service.
        /// </summary>
        public UserService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                ApiUrlString = ConfigurationManager.AppSettings["ToDoProxyUrl"];

                //var ping = new System.Net.NetworkInformation.Ping();
                //var result = ping.Send(this.ApiUrlString + "/Ping");

            }
            catch (Exception e)
            {
                ApiUrlString = ConfigurationManager.AppSettings["ToDoCloudUrl"];
            }
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The User Id.</returns>
        private int PostUser(string userName)
        {
            var response = this.httpClient.PostAsJsonAsync(this.ApiUrlString + CreateUrl, userName).Result;
            return response.Content.ReadAsAsync<int>().Result;
        }

        /// <summary>
        /// Tries to take existing user from cookies. If fails then creates a new user with autogenerated name.
        /// </summary>
        /// <returns>The User Id.</returns>
        public int GetUser()
        {
            var userCookie = HttpContext.Current.Request.Cookies["user"];
            int userId;

            if (userCookie != null)
            {
                bool success = int.TryParse(userCookie.Value, out userId);
                if (success)
                {
                    return userId;
                }
            }

            return this.CreateUser();
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CreateUser()
        {
            var userId = this.PostUser("Noname: " + Guid.NewGuid());

            var cookie = new HttpCookie("user", userId.ToString())
            {
                Expires = DateTime.Today.AddMonths(1)
            };

            HttpContext.Current.Response.SetCookie(cookie);
            return userId;
        }
    }
}
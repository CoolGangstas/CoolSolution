using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoolProxyServer.Controllers
{
    using BLL;

    /// <summary>
    /// The users controller.
    /// </summary>
    public class UsersController : ApiController
    {
        /// <summary>
        /// The user service.
        /// </summary>
        private readonly UserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        public UsersController()
        {
            this.userService = new UserService();
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userName">
        /// The user Name.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody]string userName)
        {
            this.userService.CreateUser(userName);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

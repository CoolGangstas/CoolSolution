using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoClient.Services;

namespace todoclient.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserService userService = new UserService();

        public void Post()
        {
            userService.CreateUser();
        }
    }
}

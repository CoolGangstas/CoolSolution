using System.Web.Http;
using ToDoClient.Services;

namespace todoclient.Controllers
{
    /// <summary>
    /// Works with users
    /// </summary>
    public class UserController : ApiController
    {
        private readonly UserService userService = new UserService();

        /// <summary>
        /// Creates a new user
        /// </summary>
        public void Post()
        {
            userService.CreateUser();
        }
    }
}

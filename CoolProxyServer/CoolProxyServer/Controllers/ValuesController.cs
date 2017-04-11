using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoolProxyServer.Models;

namespace CoolProxyServer.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {



        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //public string Get(int id)
        //{
        //    return "value";
        //}


        public User Get(int id)
        {
            
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">User</param>
        public void CreateUser()
        {
        }


        //[HttpPost]
        //public void CreateItem([FromBody]string value)
        //{
        //}

        //[HttpPut]
        //public void EditItem(int id, [FromBody]string value)
        //{
        //}

        //[HttpDelete]
        //public void RemoveItem(int id)
        //{
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoolProxyServer.Models;

namespace CoolProxyServer.Controllers
{
    using BLL;
    using BLL.Entities;
    using BLL.Mappers;

    using CoolProxyServer.Mapper;

    using Mappers = BLL.Mappers.Mappers;

    /// <summary>
    /// The records controller.
    /// </summary>
    public class ToDosController : ApiController
    {
        /// <summary>
        /// The record service.
        /// </summary>
        private RecordService recordService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDosController"/> class.
        /// </summary>
        public ToDosController()
        {
            this.recordService = new RecordService();
        }

        /// <summary>
        /// Creates item.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpPost]
        public HttpResponseMessage CreateItem([FromBody]Record value)
        {
            try
            {
                this.recordService.Create(value.ToRecordEntityFromModel());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Updates item.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpPut]
        public HttpResponseMessage UpdateItem([FromBody]Record value)
        {
            try
            {
                this.recordService.Update(value.ToRecordEntityFromModel());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Deletes item.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpDelete]
        public HttpResponseMessage DeleteItem(int id)
        {
            try
            {
                this.recordService.Delete(id);
            }
            catch (Exception e)
            {

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpGet]
        public HttpResponseMessage GetAllItems(int userId)
        {
            return new HttpResponseMessage()
                       {
                           Content =
                               new ObjectContent<IList<CloudRecordModel>>(
                                   this.recordService.GetAllById(userId).AsEnumerable()
                                       .Select(x => x.ToClientRecordModel())
                                       .ToList(),
                                   this.Configuration.Formatters.JsonFormatter)
                       };
        }
    }
}

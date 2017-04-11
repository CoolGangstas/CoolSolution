using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolProxyServer.Mapper
{
    using BLL.Entities;

    using CoolProxyServer.Models;

    /// <summary>
    /// The mappers.
    /// </summary>
    public static class Mappers
    {
        /// <summary>
        /// The to record entity.
        /// </summary>
        /// <param name="record">
        /// The record.
        /// </param>
        /// <returns>
        /// The <see cref="RecordEntity"/>.
        /// </returns>
        public static RecordEntity ToRecordEntityFromModel(this Record record)
        {
            return new RecordEntity()
                       {
                           Id = record.ToDoId,
                           Name = record.Name,
                           IsCompleted = record.IsCompleted,
                           UserId = record.UserId
                       };
        }
    }
}
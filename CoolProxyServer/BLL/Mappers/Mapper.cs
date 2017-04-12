using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    using BLL.Entities;

    using DAL.DTO;

    public static class Mappers
    {
        /// <summary>
        /// The mapper to user entity.
        /// </summary>
        /// <param name="dalUser">
        /// The dal user.
        /// </param>
        /// <returns>
        /// The <see cref="UserEntity"/>.
        /// </returns>
        public static UserEntity ToUserEntity(this DalUser dalUser)
        {
           return new UserEntity()
                      {
                          Id = dalUser.Id,
                          Name = dalUser.Name
                      };
        }

        /// <summary>
        /// The mapper to record entity.
        /// </summary>
        /// <param name="dalRecord">
        /// The dal record.
        /// </param>
        /// <returns>
        /// The <see cref="RecordEntity"/>.
        /// </returns>
        public static RecordEntity ToRecordEntity(this DalRecord dalRecord)
        {
            return new RecordEntity()
                       {
                           Id = dalRecord.Id,
                           UserId = dalRecord.UserId,
                           Name = dalRecord.Name,
                           IsCompleted = dalRecord.IsCompleted
                       };
        }

        /// <summary>
        /// The mapper to dal record.
        /// </summary>
        /// <param name="recordEntity">
        /// The record entity.
        /// </param>
        /// <returns>
        /// The <see cref="DalRecord"/>.
        /// </returns>
        public static DalRecord ToDalRecord(this RecordEntity recordEntity)
        {
            return new DalRecord()
                       {
                           Id = recordEntity.Id,
                           IsCompleted = recordEntity.IsCompleted,
                           Name = recordEntity.Name,
                           UserId = recordEntity.UserId
                       };
        }

        public static CloudRecordModel ToCloudRecordModel(this RecordEntity recordEntity)
        {
            return new CloudRecordModel()
                       {
                           IsCompleted = recordEntity.IsCompleted,
                           Name = recordEntity.Name,
                           ToDoId = recordEntity.Id,
                           UserId = recordEntity.UserId
                       };
        }

        public static RecordEntity ToRecordEntity(this CloudRecordModel cloudRecordModel)
        {
            return new RecordEntity()
                       {
                           CloudId = cloudRecordModel.ToDoId,
                           UserId = cloudRecordModel.UserId,
                           IsCompleted = cloudRecordModel.IsCompleted,
                           Name = cloudRecordModel.Name
                       };
        }
    }
}

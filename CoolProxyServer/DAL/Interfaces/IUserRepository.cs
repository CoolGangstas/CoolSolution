using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// The UserRepository interface.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <returns></returns>
        int CreateUser();

    }
}

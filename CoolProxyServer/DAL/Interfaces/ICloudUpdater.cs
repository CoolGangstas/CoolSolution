using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// The CloudUpdater interface.
    /// </summary>
    public interface ICloudUpdater
    {
        /// <summary>
        /// Updates the cloud identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cloudId">The cloud identifier.</param>
        void UpdateCloudId(int id, int cloudId);
    }
}

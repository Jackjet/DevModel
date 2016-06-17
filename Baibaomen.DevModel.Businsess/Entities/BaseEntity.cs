using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess
{
    /// <summary>
    /// Provide base class for database entities.
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// The time the record created. Should be maintained by related services.
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// The time the record updated. Should be maintained by related services.
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Record version to do concurrency check.
        /// </summary>
        [Timestamp]
        public byte[] RecordVersion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess.Entities
{
    /// <summary>
    /// Business user.
    /// </summary>
    public class User:BaseEntity
    {
        public string ClaimIdentity { get; set; }
    }
}

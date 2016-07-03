using Baibaomen.DevModel.Businsess.DBContexts;
using Baibaomen.DevModel.Businsess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess.DataServices
{
    public class PropertyDataService:BaseDataService<Property>
    {
        public PropertyDataService(SqlDbContext db) : base(db,()=>db.Properties) { }
    }
}

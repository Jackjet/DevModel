using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess.DataServices
{
    public abstract class BaseDataService
    {
        public DbContextTransaction BeginTransaction(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted) {
            return DB.Database.BeginTransaction(isolationLevel);
        }

        protected SqlDbContext DB { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public BaseDataService(SqlDbContext db)
        {
            DB = db;
        }
    }
}

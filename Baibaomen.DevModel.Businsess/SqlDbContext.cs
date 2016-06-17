using Baibaomen.DevModel.Businsess.Entities;
using System;
using System.Data.Entity;

namespace Baibaomen.DevModel.Businsess
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext() : base("name=SqlDb")
        {
        }

        /// <summary>
        /// Set the CreateTime and UpdateTime; Specify that the record needs concurrency check during update/delete.
        /// </summary>
        /// <param name="entity"></param>
        public void PrepareEntityForEdit(BaseEntity entity) {
            if (entity.CreateTime == default(DateTime)) {
                entity.CreateTime = DateTime.Now;
            }

            entity.UpdateTime = DateTime.Now;

            if (Entry(entity).State != EntityState.Added)
            {
                Entry(entity).OriginalValues["RecordVersion"] = entity.RecordVersion;
            }
        }

        public IDbSet<Property> Properties { get; set; }

        public IDbSet<Communication> Communications { get; set; }
    }
}

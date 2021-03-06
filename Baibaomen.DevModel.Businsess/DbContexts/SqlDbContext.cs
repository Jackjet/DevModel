﻿using Baibaomen.DevModel.Businsess.Entities;
using System;
using System.Data.Entity;

namespace Baibaomen.DevModel.Businsess.DBContexts
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext() : base("name=SqlDb")
        {
            ///Disable lazy loading to bypass the serializing issue.(All related data loaded because of the serializing process). You should call the Load method explicitly.
            /// refer to :https://msdn.microsoft.com/en-us/data/jj574232.aspx
            /// how to do explict load on nested element:
            /// context.Entry(post).Reference(p => p.Blog).Load(); 
            /// context.Entry(blog).Collection(p => p.Posts).Load(); 
            //Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Set the CreateTime and UpdateTime; Specify that the record needs concurrency check during update/delete.
        /// </summary>
        /// <param name="entity"></param>
        public void PrepareEntityToSave(BaseEntity entity,bool needConcurrencyCheck = true, int operatorId = default(int)){
            if (entity.CreateTime == default(DateTime))  {
                entity.CreateTime = DateTime.Now;
            }

            entity.UpdateTime = DateTime.Now;

            if (operatorId != 0) {
                if (entity.Id == default(int))
                {
                    entity.CreatorId = operatorId;
                }
                else
                {
                    entity.UpdatorId = operatorId;
                }
            }

            if (needConcurrencyCheck && Entry(entity).State != EntityState.Added)
            {
                Entry(entity).OriginalValues["RecordVersion"] = entity.RecordVersion;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <typeparam name="TUserId"></typeparam>
        /// <param name="entity"></param>
        /// <param name="operatorId"></param>
        public void PrepareEntityToDelete<TRecordVersion>(BaseEntity entity,TRecordVersion clientRecordVersion = default(TRecordVersion), int operatorId = default(int))
        {
            if (Entry(entity).State != EntityState.Added && !clientRecordVersion.Equals(default(TRecordVersion)))
            {
                Entry(entity).OriginalValues["RecordVersion"] = clientRecordVersion;
            }
        }

        public IDbSet<Property> Properties { get; set; }

        public IDbSet<Communication> Communications { get; set; }

        public IDbSet<User> Users { get; set; }
    }
}

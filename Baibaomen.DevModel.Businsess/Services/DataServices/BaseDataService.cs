using Baibaomen.DevModel.Businsess.DBContexts;
using Baibaomen.DevModel.Businsess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess.DataServices
{
    public abstract class BaseDataService<T> where T :BaseEntity
    {
        public DbContextTransaction BeginTransaction(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted) {
            return DB.Database.BeginTransaction(isolationLevel);
        }

        protected SqlDbContext DB { get; private set; }

        private Func<IDbSet<T>> _col;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public BaseDataService(SqlDbContext db,Func<IDbSet<T>> getEntityFunc)
        {
            DB = db;
            _col = getEntityFunc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(User theOperator = null)
        {
            return _col();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id, User theOperator = null)
        {
            return _col().Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(T item, bool needConcurrencyCheck = true, User theOperator = null)
        {
            _col().Add(item);
            DB.PrepareEntityToSave(item, needConcurrencyCheck, theOperator == null?0:theOperator.Id);
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        public virtual async Task<T> UpdateAsync(int id, Func<T, T> updateAction, bool needConcurrencyCheck = true, User theOperator = null)
        {
            var dbData = _col().Find(id);

            updateAction(dbData);

            DB.PrepareEntityToSave(dbData, needConcurrencyCheck, theOperator == null ? 0 : theOperator.Id);
            await DB.SaveChangesAsync();

            return dbData;
        }

        public virtual async Task DeleteAsync(T item, User theOperator = null) {
            await DeleteAsync(item, default(int), theOperator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync<TRecordVersion>(T item, TRecordVersion clientRecordVersion, User theOperator = null)
        {
            DB.PrepareEntityToDelete(item,clientRecordVersion, theOperator == null ? 0 : theOperator.Id);
            _col().Remove(item);
            await DB.SaveChangesAsync();
        }
    }
}

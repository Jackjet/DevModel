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
    public class PropertyDataService:BaseDataService
    {
        public PropertyDataService(SqlDbContext db) : base(db) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<Property> GetAll() {
            return DB.Properties;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Property Get(int id, string operatorId = null) {
            return DB.Properties.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(Property item, bool needConcurrencyCheck = true, string operatorId = null) {
            DB.Properties.Add(item);
            DB.PrepareEntityToSave(item, needConcurrencyCheck, operatorId);
            await DB.SaveChangesAsync();
        }

        public virtual void Add(Property item, bool needConcurrencyCheck = true, string operatorId = null) {
            DB.Properties.Add(item);
            DB.PrepareEntityToSave(item, needConcurrencyCheck, operatorId);
            DB.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        public virtual async Task<Property> UpdateAsync(int id,Func<Property,Property> updateAction, bool needConcurrencyCheck = true, string operatorId = null) {
            var dbData = DB.Properties.Find(id);

            updateAction(dbData);

            DB.PrepareEntityToSave(dbData,needConcurrencyCheck , operatorId);
            await DB.SaveChangesAsync();

            return dbData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(Property item, bool needConcurrencyCheck, string operatorId = null) {
            DB.PrepareEntityToDelete(item, needConcurrencyCheck, operatorId);
            DB.Properties.Remove(item);
            await DB.SaveChangesAsync();
        }
    }
}

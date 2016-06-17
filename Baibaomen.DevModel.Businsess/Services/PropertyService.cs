using Baibaomen.DevModel.Businsess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess.Services
{
    public class PropertyService
    {
        private SqlDbContext db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public PropertyService(SqlDbContext db) {
            this.db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Property> GetAll() {
            return db.Properties;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Property Get(int id) {
            return db.Properties.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AddAsync(Property item) {
            db.Properties.Add(item);
            db.PrepareEntityToSave(item);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        public async Task<Property> UpdateAsync(int id,Func<Property,Property> updateAction) {
            var dbData = db.Properties.Find(id);

            updateAction(dbData);

            db.PrepareEntityToSave(dbData);
            await db.SaveChangesAsync();

            return dbData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Property item) {
            db.Properties.Remove(item);
            await db.SaveChangesAsync();
        }
    }
}

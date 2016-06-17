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
        public IQueryable<Communication> GetAllCommunications() {
            return db.Communications;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Communication GetCommunication(int id) {
            return db.Communications.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public async Task AddCommunicationAsync(Communication communication) {
            db.Communications.Add(communication);
            db.PrepareEntityForEdit(communication);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        public async Task<Communication> UpdateCommunicationAsync(int id,Func<Communication,Communication> updateAction) {
            var dbData = db.Communications.Find(id);

            updateAction(dbData);

            db.PrepareEntityForEdit(dbData);
            await db.SaveChangesAsync();

            return dbData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public async Task DeleteCommunicationAsync(Communication communication) {
            db.Communications.Remove(communication);
            await db.SaveChangesAsync();
        }
    }
}

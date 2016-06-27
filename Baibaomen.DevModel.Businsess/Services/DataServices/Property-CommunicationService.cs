using Baibaomen.DevModel.Businsess.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess.DataServices
{
    public class PropertyCommunicationDataService:BaseDataService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public PropertyCommunicationDataService(SqlDbContext db) : base(db) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<Communication> GetAll()
        {
            return DB.Communications;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Communication Get(int id)
        {
            return DB.Communications.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(Communication communication, int? operatorId = null)
        {
            if (DB.Properties.Find(communication.PropertyId) == null)
            {
                throw new ApplicationException(string.Format(Resources.ErrorMessageResource.PropertyNotExistForCommunication, communication.PropertyId));
            }

            DB.Communications.Add(communication);
            DB.PrepareEntityToSave(communication, operatorId);
            await DB.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public virtual void Add(Communication communication, int? operatorId = null)
        {
            if (DB.Properties.Find(communication.PropertyId) == null)
            {
                throw new ApplicationException(string.Format(Resources.ErrorMessageResource.PropertyNotExistForCommunication, communication.PropertyId));
            }

            DB.Communications.Add(communication);
            DB.PrepareEntityToSave(communication, operatorId);
            DB.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        public virtual async Task<Communication> UpdateAsync(int id, Func<Communication, Communication> updateAction, int? operatorId = null)
        {
            var dbData = DB.Communications.Find(id);

            updateAction(dbData);

            DB.PrepareEntityToSave(dbData, operatorId);
            await DB.SaveChangesAsync();

            return dbData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(Communication communication)
        {
            DB.Communications.Remove(communication);
            await DB.SaveChangesAsync();
        }
    }
}

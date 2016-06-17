using Baibaomen.DevModel.Businsess.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess.Services
{
    public class PropertyCommunicationService
    {
        private SqlDbContext db;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="db"></param>
    public PropertyCommunicationService(SqlDbContext db)
    {
        this.db = db;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IQueryable<Communication> GetAll()
    {
        return db.Communications;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Communication Get(int id)
    {
        return db.Communications.Find(id);
    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public async Task AddAsync(Communication communication)
        {
            if (db.Properties.Find(communication.PropertyId) == null)
            {
                throw new ApplicationException(string.Format(Resources.ErrorMessageResource.PropertyNotExistForCommunication,communication.PropertyId));
            }

            db.Communications.Add(communication);
            db.PrepareEntityToSave(communication);
            await db.SaveChangesAsync();
        }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateAction"></param>
    /// <returns></returns>
    public async Task<Communication> UpdateAsync(int id, Func<Communication, Communication> updateAction)
    {
        var dbData = db.Communications.Find(id);

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
    public async Task DeleteAsync(Communication communication)
    {
        db.Communications.Remove(communication);
        await db.SaveChangesAsync();
    }
}
}

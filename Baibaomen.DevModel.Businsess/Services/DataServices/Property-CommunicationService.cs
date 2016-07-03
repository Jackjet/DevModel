using Baibaomen.DevModel.Businsess.DBContexts;
using Baibaomen.DevModel.Businsess.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess.DataServices
{
    public class PropertyCommunicationDataService:BaseDataService<Communication>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public PropertyCommunicationDataService(SqlDbContext db) : base(db,()=>db.Communications) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public override async Task AddAsync(Communication communication,bool needConcurrencyCheck = true, User theOperator = null)
        {
            if (DB.Properties.Find(communication.PropertyId) == null)
            {
                throw new ApplicationException(string.Format(Resources.ErrorMessageResource.PropertyNotExistForCommunication, communication.PropertyId));
            }

            await base.AddAsync(communication, needConcurrencyCheck, theOperator);
        }
    }
}

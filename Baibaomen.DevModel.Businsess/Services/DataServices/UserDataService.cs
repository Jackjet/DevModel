using Baibaomen.DevModel.Businsess.DataServices;
using Baibaomen.DevModel.Businsess.DBContexts;
using Baibaomen.DevModel.Businsess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess.DataServices
{
    public class UserDataService : BaseDataService<User>
    {
        public UserDataService(SqlDbContext db) : base(db, () => db.Users) { }

        public User GetUserByClaimIdentity(string claimIdentity) {
            return GetAll().FirstOrDefault(x => x.ClaimIdentity == claimIdentity);
        }
    }
}

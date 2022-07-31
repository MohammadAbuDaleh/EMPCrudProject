using AdminEntity.Infrastructure;
using AdminEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminEntity.Service
{
    public class USERS_SERVICE
    {
        public static USERS CheckUser(string sUsername, string sPassowrd,string connection)
        {
            using (ADMIN_MODELS oContext = new ADMIN_MODELS(connection))
            {
                return oContext.USERS.Where(oUser => oUser.USERNAME.Equals(sUsername, StringComparison.OrdinalIgnoreCase) && oUser.PASSWORD.Equals(sPassowrd)).ToList().FirstOrDefault();
            }
        }
    }
}

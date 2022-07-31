using AdminEntity.Infrastructure;
using AdminEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminEntity.Service
{
    public static class EMPLOYEES_SERVICE
    {
        public static List<EMPLOYEES> LoadAll(string connectionstring)
        {
            using (ADMIN_MODELS oContext = new ADMIN_MODELS(connectionstring))
            {
                return oContext.EMPLOYEES.ToList();
            }
        }
        public static EMPLOYEES Load(int nId, string connectionstring)
        {
            using (ADMIN_MODELS oContext = new ADMIN_MODELS(connectionstring))
            {
                return oContext.EMPLOYEES.Where(x => x.ID == nId).ToList()[0];
            }
        }
        public static void Insert(EMPLOYEES oUserProfile, string connectionstring)
        {
            using (ADMIN_MODELS objContext = new ADMIN_MODELS(connectionstring))
            {
                oUserProfile.ID = nGetMaxID(connectionstring) + 1;
                objContext.Add(oUserProfile);
                objContext.SaveChanges();
            }
        }
        public static void Update(EMPLOYEES oUserProfile, string connectionstring)
        {
            using (ADMIN_MODELS objContext = new ADMIN_MODELS(connectionstring))
            {
                objContext.Update(oUserProfile);
                objContext.SaveChanges();
            }
        }
        public static int? nGetMaxID(string connectionstring)
        {
            using (ADMIN_MODELS objContext = new ADMIN_MODELS(connectionstring))
            {
                var vMaxId = objContext.EMPLOYEES.Count() > 0 ? objContext.EMPLOYEES.Max(x => x.ID) : 0;
                return vMaxId;
            }
        }
        public static void Delete(EMPLOYEES oUserProfile, string connectionstring)
        {
            using (ADMIN_MODELS objContext = new ADMIN_MODELS(connectionstring))
            {
                objContext.Remove(oUserProfile);
                objContext.SaveChanges();
            }
        }
    }
}

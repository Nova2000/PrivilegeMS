using PrivilegeMS.IBLL;
using PrivilegeMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeMS.BLL
{
    public partial class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {
       public  bool SetRoleActionInfo(int roleId, List<int> actionIdList)
        {
            var roleInfo = this.LoadEntities(r => r.ID == roleId && r.DelFlag == true).FirstOrDefault();
            if (roleInfo!=null)
            {
                roleInfo.ActionInfo.Clear();
                foreach (int actionId in actionIdList)
                {
                    var actionInfo = this.CurrentDBSession.ActionInfoDal.LoadEntities(a => a.ID == actionId && a.DelFlag == true).FirstOrDefault();
                    if (actionInfo!=null)
                    {
                        roleInfo.ActionInfo.Add(actionInfo);
                    }
                }
                return this.CurrentDBSession.SaveChanges();
            }
            return false;
        }
    }
}

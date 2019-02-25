using PrivilegeMS.IBLL;
using PrivilegeMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeMS.BLL
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        public bool SetUserRoleInfo(int userId, List<int> roleIdList)
        {
            var userInfo = this.LoadEntities(u => u.ID == userId && u.DelFlag == true).FirstOrDefault();
            if (userInfo!=null)
            {
                userInfo.RoleInfo.Clear();
                foreach (int roleId in roleIdList)
                {
                    var roleInfo = this.CurrentDBSession.RoleInfoDal.LoadEntities(r => r.ID == roleId && r.DelFlag == true).FirstOrDefault();
                    if (roleInfo!=null)
                    {
                        userInfo.RoleInfo.Add(roleInfo);
                    }
                }
                return this.CurrentDBSession.SaveChanges();
            }
            return false;

        }
    }
}

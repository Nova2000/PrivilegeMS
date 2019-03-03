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
                if (roleIdList.Count>0)
                {
                    foreach (int roleId in roleIdList)
                    {
                        var roleInfo = this.CurrentDBSession.RoleInfoDal.LoadEntities(r => r.ID == roleId && r.DelFlag == true).FirstOrDefault();
                        if (roleInfo != null)
                        {
                            userInfo.RoleInfo.Add(roleInfo);
                        }
                    } 
                }
                return this.CurrentDBSession.SaveChanges();
            }
            return false;

        }
        public bool SetUserActionInfo(int actionId, int userId, bool isPass)
        {
            var r_userInfo_actionInfo = this.CurrentDBSession.RUserInfoActionInfoDal.LoadEntities(a => a.ActionInfoID == actionId && a.UserInfoID == userId).FirstOrDefault();
            if (r_userInfo_actionInfo==null)
            {
                RUserInfoActionInfo userInfoActionInfo = new RUserInfoActionInfo();
                userInfoActionInfo.ActionInfoID = actionId;
                userInfoActionInfo.UserInfoID = userId;
                userInfoActionInfo.IsPass = isPass;
                this.CurrentDBSession.RUserInfoActionInfoDal.AddEntity(userInfoActionInfo);
            }
            else{
                r_userInfo_actionInfo.IsPass = isPass;
                this.CurrentDBSession.RUserInfoActionInfoDal.EditEntity(r_userInfo_actionInfo);
            }
            return this.CurrentDBSession.SaveChanges();
        }
    }
}

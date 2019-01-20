	

 
using PrivilegeMS.IBLL;
using PrivilegeMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeMS.BLL
{
	
	public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.ActionInfoDal;
        }
    }   
	
	public partial class RoleInfoService :BaseService<RoleInfo>,IRoleInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.RoleInfoDal;
        }
    }   
	
	public partial class RUserInfoActionInfoService :BaseService<RUserInfoActionInfo>,IRUserInfoActionInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.RUserInfoActionInfoDal;
        }
    }   
	
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.UserInfoDal;
        }
    }   
	
}
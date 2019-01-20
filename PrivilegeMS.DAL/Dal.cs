 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivilegeMS.IDAL;
using PrivilegeMS.Model;
using System.Linq.Expressions;

namespace PrivilegeMS.DAL
{
		
	public partial class ActionInfoDal :BaseDal<ActionInfo>,IActionInfoDal
    {

    }
		
	public partial class RoleInfoDal :BaseDal<RoleInfo>,IRoleInfoDal
    {

    }
		
	public partial class RUserInfoActionInfoDal :BaseDal<RUserInfoActionInfo>,IRUserInfoActionInfoDal
    {

    }
		
	public partial class UserInfoDal :BaseDal<UserInfo>,IUserInfoDal
    {

    }
	
}
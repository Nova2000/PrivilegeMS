 
using PrivilegeMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeMS.IDAL
{
   
	
	public partial interface IActionInfoDal :IBaseDal<ActionInfo>
    {
      
    }
	
	public partial interface IRoleInfoDal :IBaseDal<RoleInfo>
    {
      
    }
	
	public partial interface IRUserInfoActionInfoDal :IBaseDal<RUserInfoActionInfo>
    {
      
    }
	
	public partial interface IUserInfoDal :IBaseDal<UserInfo>
    {
      
    }
	
}
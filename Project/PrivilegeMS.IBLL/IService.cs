 

using PrivilegeMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeMS.IBLL
{
	
	public partial interface IActionInfoService : IBaseService<ActionInfo>
    {
       
    }   
	
	public partial interface IRoleInfoService : IBaseService<RoleInfo>
    {
       
    }   
	
	public partial interface IRUserInfoActionInfoService : IBaseService<RUserInfoActionInfo>
    {
       
    }   
	
	public partial interface IUserInfoService : IBaseService<UserInfo>
    {
       
    }   
	
}
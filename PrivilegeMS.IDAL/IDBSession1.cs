 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PrivilegeMS.IDAL
{
	public partial interface IDBSession
    {

	
		IActionInfoDal ActionInfoDal{get;set;}
	
		IRoleInfoDal RoleInfoDal{get;set;}
	
		IRUserInfoActionInfoDal RUserInfoActionInfoDal{get;set;}
	
		IUserInfoDal UserInfoDal{get;set;}
	}	
}
 

using PrivilegeMS.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeMS.DALFactory
{
    public partial class AbstractFactory
    {
      
   
		
	    public static IActionInfoDal createActionInfoDal()
        {

		 string fullClassName = NameSpace + ".ActionInfoDal";
          return CreateInstance(fullClassName) as IActionInfoDal;

        }
		
	    public static IRoleInfoDal createRoleInfoDal()
        {

		 string fullClassName = NameSpace + ".RoleInfoDal";
          return CreateInstance(fullClassName) as IRoleInfoDal;

        }
		
	    public static IRUserInfoActionInfoDal createRUserInfoActionInfoDal()
        {

		 string fullClassName = NameSpace + ".RUserInfoActionInfoDal";
          return CreateInstance(fullClassName) as IRUserInfoActionInfoDal;

        }
		
	    public static IUserInfoDal createUserInfoDal()
        {

		 string fullClassName = NameSpace + ".UserInfoDal";
          return CreateInstance(fullClassName) as IUserInfoDal;

        }
	}
	
}
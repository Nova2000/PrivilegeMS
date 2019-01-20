using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivilegeMS.IDAL;
using PrivilegeMS.Model;

namespace PrivilegeMS.DAL
{
    public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
    {
        //BaseDal不需要实现IBaseDal接口，因为IUserInfoDal已经实现了IBaseDal，BaseDal在此不过是对IUserInfoDal所实现的IUserInfoDal接口的进一步封装
    }
}

using PrivilegeMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeMS.IBLL
{
    public partial interface IRoleInfoService : IBaseService<RoleInfo>
    {
        bool SetRoleActionInfo(int roleId, List<int> actionIdList);
    }
}

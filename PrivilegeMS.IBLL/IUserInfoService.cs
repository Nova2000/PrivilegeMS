/************************************************************************************
* Copyright (c) 2018Microsoft all Rights Reserved
* CLR版本: 4.0.30319.42000
*机器名称: LIRUISEN
*命名空间: PrivilegeMS.IBLL
*文件名: IUserInfoService
*版本号: V1.0.0.0
*唯一标识: dcda9e46-a6b2-438f-ad4d-79121fc4aab8
*当前用户域: LIRUISEN
*创建人: Administrator
*创建时间: 2018/12/29 21:51:46
*
*描述:
*
*
*
*====================================================================
*修改标记
*修改时间: 2018/12/29 21:51:46
*修改人: Administrator
*版本号: V1.0.0.0
*描述：
*
*
************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivilegeMS.Model;

namespace PrivilegeMS.IBLL
{
    public partial interface  IUserInfoService:IBaseService<UserInfo>
    {
        bool SetUserRoleInfo(int userId, List<int> roleIdList);
        bool SetUserActionInfo(int actionId,int  userId, bool isPass);
    }
}

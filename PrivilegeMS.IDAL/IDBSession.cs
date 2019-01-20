/************************************************************************************
* Copyright (c) 2018Microsoft all Rights Reserved
* CLR版本: 4.0.30319.42000
*机器名称: LIRUISEN
*命名空间: Privilege.IDAL
*文件名: IDBSession
*版本号: V1.0.0.0
*唯一标识: 76b62ece-45df-40a0-918c-0bf51323e2ff
*当前用户域: LIRUISEN
*创建人: Administrator
*创建时间: 2018/12/29 21:22:45
*
*描述:
*
*
*
*====================================================================
*修改标记
*修改时间: 2018/12/29 21:22:45
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
using System.Data.Entity;

namespace PrivilegeMS.IDAL
{
    public partial interface IDBSession
    {
        DbContext Db { get; }
        /// <summary>
        /// 一次保存多次EF操作
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();
    }
}

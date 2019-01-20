/************************************************************************************
* Copyright (c) 2018Microsoft all Rights Reserved
* CLR版本: 4.0.30319.42000
*机器名称: LIRUISEN
*命名空间: PrivilegeMS.DALFactiory
*文件名: DBSession
*版本号: V1.0.0.0
*唯一标识: 2e841aed-b258-4f11-b580-5a000d201b60
*当前用户域: LIRUISEN
*创建人: Administrator
*创建时间: 2018/12/29 21:32:17
*
*描述:
*
*
*
*====================================================================
*修改标记
*修改时间: 2018/12/29 21:32:17
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
using PrivilegeMS.IDAL;

namespace PrivilegeMS.DALFactory
{
    public partial class DBSession : IDBSession
    {
        public System.Data.Entity.DbContext Db
        {
            get
            {
                return DAL.DbContextFactory.createDbContext();
                //创建线程内唯一的EF操作对象
            }
        }

       
        /// <summary>
        /// 连接一次数据库完成多次数据库操作
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
    }
}

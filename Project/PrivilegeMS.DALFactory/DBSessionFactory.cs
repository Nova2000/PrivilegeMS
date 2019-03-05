/************************************************************************************
* Copyright (c) 2018Microsoft all Rights Reserved
* CLR版本: 4.0.30319.42000
*机器名称: LIRUISEN
*命名空间: PrivilegeMS.DALFactiory
*文件名: DBSessionFactory
*版本号: V1.0.0.0
*唯一标识: cb5f4246-6ac1-4559-8f24-f513daf27cd3
*当前用户域: LIRUISEN
*创建人: Administrator
*创建时间: 2018/12/29 21:45:05
*
*描述:
*
*
*
*====================================================================
*修改标记
*修改时间: 2018/12/29 21:45:05
*修改人: Administrator
*版本号: V1.0.0.0
*描述：
*
*
************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeMS.DALFactory
{
    public class DBSessionFactory
    {
        public static IDAL.IDBSession creatDBSession()
        {
            IDAL.IDBSession DbSession = (IDAL.IDBSession)CallContext.GetData("dbSession");
            if (DbSession == null)
            {
                DbSession = new DALFactory.DBSession();
                CallContext.SetData("dbSession", DbSession);
            }
            return DbSession;
        }
    }
}

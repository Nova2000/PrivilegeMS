/************************************************************************************
* Copyright (c) 2018Microsoft all Rights Reserved
* CLR版本: 4.0.30319.42000
*机器名称: LIRUISEN
*命名空间: PrivilegeMS.IBLL
*文件名: IBaseService
*版本号: V1.0.0.0
*唯一标识: 9d0a7de6-d03a-4342-9cf9-cb5760d9190b
*当前用户域: LIRUISEN
*创建人: Administrator
*创建时间: 2018/12/29 21:51:23
*
*描述:
*
*
*
*====================================================================
*修改标记
*修改时间: 2018/12/29 21:51:23
*修改人: Administrator
*版本号: V1.0.0.0
*描述：
*
*
************************************************************************************/
using PrivilegeMS.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeMS.IBLL
{
    public interface IBaseService<T> where T:class, new()
    {

        IDBSession CurrentDBSession { get; }
        IDAL.IBaseDal<T> CurrentDal { get; set; }

        #region 简单查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);

        #endregion

        #region 分页查询
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderByLambda, bool isAsc);

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteEntity(T entity);

        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool EditEntity(T entity);

        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T AddEntity(T entity); 
        #endregion
    }
}

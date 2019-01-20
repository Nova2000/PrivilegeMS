/************************************************************************************
* Copyright (c) 2018Microsoft all Rights Reserved
* CLR版本: 4.0.30319.42000
*机器名称: LIRUISEN
*命名空间: PrivilegeMS.BLL
*文件名: BaseService
*版本号: V1.0.0.0
*唯一标识: f4d4c96e-1de5-41ec-ab73-ebcaa7e9550b
*当前用户域: LIRUISEN
*创建人: Administrator
*创建时间: 2018/12/29 21:58:08
*
*描述:
*
*
*
*====================================================================
*修改标记
*修改时间: 2018/12/29 21:58:08
*修改人: Administrator
*版本号: V1.0.0.0
*描述：
*
*
************************************************************************************/
using PrivilegeMS.DALFactory;
using PrivilegeMS.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeMS.BLL
{
    public abstract class BaseService<T> where T :class,new()
    {
        public IDBSession CurrentDBSession
        {
            get
            {
                return DBSessionFactory.creatDBSession();
            }
        }
        public IDAL.IBaseDal<T> CurrentDal { get; set; }
        public abstract void SetCurrentDal();
        public BaseService()
        {
            SetCurrentDal();//子类一定要实现的抽象方法
        }
        #region 查询
        /// <summary>
        /// 查询过滤
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.LoadEntities(whereLambda);
        }
        #endregion

        #region 分页过滤
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s">排序条件的类型</typeparam>
        /// <param name="pageIndex">当前页码数</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="totalCount">总数据条数</param>
        /// <param name="whereLambda">查询过滤条件</param>
        /// <param name="orderByLambda">排序条件</param>
        /// <param name="isAsc">是否为升序，否则为倒序</param>
        /// <returns>排序结果集</returns>
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderByLambda, bool isAsc)
        {
            return CurrentDal.LoadPageEntities<s>(pageIndex, pageSize, out totalCount, whereLambda, orderByLambda, isAsc);
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">需要删除的对象</param>
        /// <returns>bool值</returns>
        public bool DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
            return this.CurrentDBSession.SaveChanges();
        }
        #endregion

        #region 更新数据
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">需要更新的对象</param>
        /// <returns>bool结果</returns>
        public bool EditEntity(T entity)
        {
            CurrentDal.EditEntity(entity);
            return this.CurrentDBSession.SaveChanges();
        }
        #endregion

        #region 添加数据
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">需要添加的对象</param>
        /// <returns>添加成功后的对象</returns>
        public T AddEntity(T entity)
        {
            CurrentDal.AddEntity(entity);
            if (this.CurrentDBSession.SaveChanges())
            {
                return entity;
            }
            else
            {
                return null;
            }
        }
        #endregion


    }
}

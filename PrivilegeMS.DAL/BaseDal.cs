using PrivilegeMS.IDAL;
using PrivilegeMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrivilegeMS.DAL
{
    public class BaseDal<T> where T : class,new()//BaseDal不需要实现IBaseDal接口，因为IUserInfoDal已经实现了IBaseDal，BaseDal在此不过是对IUserInfoDal所实现的IUserInfoDal接口的进一步实现     
    {
        DbContext Db = DbContextFactory.createDbContext();

        #region 查询
        /// <summary>
        /// 查询过滤
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns>查询结果集</returns>
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            //try
            //{
                return Db.Set<T>().Where<T>(whereLambda); 
            //}
            //catch
            //{
            //    throw new NotImplementedException();
            //}
        }
        #endregion

        #region 分页

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
            try
            {
                var temp = Db.Set<T>().Where<T>(whereLambda);
                totalCount = temp.Count();
                if (isAsc)//升序
                {
                    temp = temp.OrderBy<T, s>(orderByLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
                }
                else//降序
                {
                    temp = temp.OrderByDescending<T, s>(orderByLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
                }
                return temp;
            }
            catch
            {
                throw new NotImplementedException();
            }
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
            try
            {
                Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
                //return Db.SaveChanges() > 0;
                return true;
            }
            catch
            {
                throw new NotImplementedException();

            }
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
            try
            {
                Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
                //return Db.SaveChanges() > 0;
                return true;

            }
            catch
            {
                throw new NotImplementedException();
            }
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
            try
            {
                Db.Set<T>().Add(entity);
                //Db.SaveChanges();
                return entity;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
        #endregion

    }
}

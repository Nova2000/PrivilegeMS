using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegeMS.IDAL
{
    public interface IBaseDal<T> where T:class,new()
    {
        #region 查询
		/// <summary>
        /// 查询数据接口
        /// </summary>
        /// <param name="whereLambda">查询的Lambda表达式</param>
        /// <returns>IQueryable结果集</returns>
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda); 
	#endregion

        #region 分页
		/// <summary>
        /// 分页接口
        /// </summary>
        /// <typeparam name="s">排序标准的类型（根据T的字段进行排序）</typeparam>
        /// <param name="pageIndex">页码数</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="totalCount">总数据条数</param>
        /// <param name="whereLambda">查询的Lambda表达式</param>
        /// <param name="orderByLambda">排序的Lambda表达式</param>
        /// <param name="isAsc">是否为升序</param>
        /// <returns>IQueryable结果集</returns>
        IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderByLambda, bool isAsc);
 
	#endregion

        #region 删除
		/// <summary>
        /// 删除数据接口
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        bool DeleteEntity(T entity); 
	#endregion

        #region 更新
		/// <summary>
        /// 更新数据接口
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool EditEntity(T entity);
 
	#endregion

        #region 添加
		 /// <summary>
        /// 添加数据接口
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T AddEntity(T entity); 
	#endregion
    }
    
}

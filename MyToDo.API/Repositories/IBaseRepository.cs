using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Repositories
{
    public interface IBaseRepository<T> where T : class, new()
    {
        /// <summary>
        /// 异步插入单个实体
        /// </summary>
        public Task<T> InsertAsync(T entity);

        //#region 基础CRUD操作

        ///// <summary>
        ///// 异步获取所有数据
        ///// </summary>
        //public Task<List<T>> GetAllAsync();

        ///// <summary>
        ///// 异步根据ID获取数据
        ///// </summary>
        //public Task<T> GetByIdAsync(object id);

        ///// <summary>
        ///// 异步插入单个实体
        ///// </summary>
        //public Task<T> InsertAsync(T entity);

        ///// <summary>
        ///// 异步批量插入
        ///// </summary>
        //public Task<int> InsertRangeAsync(List<T> entities);

        ///// <summary>
        ///// 异步更新实体
        ///// </summary>
        //public Task<bool> UpdateAsync(T entity);

        ///// <summary>
        ///// 异步根据条件更新
        ///// </summary>
        //public Task<int> UpdateAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> updateExpression);

        ///// <summary>
        ///// 异步删除实体
        ///// </summary>
        //public Task<bool> DeleteAsync(T entity);

        ///// <summary>
        ///// 异步根据ID删除
        ///// </summary>
        //public Task<bool> DeleteByIdAsync(object id);

        ///// <summary>
        ///// 异步根据条件删除
        ///// </summary>
        //public Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression);

        ///// <summary>
        ///// 异步根据ID列表批量删除
        ///// </summary>
        //public Task<bool> DeleteByIdsAsync(List<object> ids);

        //#endregion


    }
}

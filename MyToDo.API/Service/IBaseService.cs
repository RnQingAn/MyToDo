using MyToDo.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Service
{
    public  interface IBaseService<TEntity>
    {
        #region 基础CRUD操作

        //<summary>
        //异步插入单个实体
        //</summary>
        public Task<bool> InsertAsync(TEntity entity);

        /// <summary>
        /// 异步获取所有数据
        /// </summary>
        public Task<List<TEntity>> GetListAsync();

        /// <summary>
        /// 异步根据ID获取数据
        /// </summary>
        public Task<TEntity> GetByIdAsync(object id);


        /// <summary>
        /// 异步批量插入
        /// </summary>
        //public Task<int> InsertRangeAsync(List<TEntity> entities);

        /// <summary>
        /// 异步更新实体
        /// </summary>
        public Task<bool> UpdateAsync(TEntity entity);


        /// <summary>
        /// 异步根据条件更新
        /// </summary>
        public Task<bool> UpdateSetColumnsTrueAsync(Expression<Func<TEntity, TEntity>> updateExpression, Expression<Func<TEntity, bool>> whereExpression);

        
        /// <summary>
        /// 异步根据ID删除
        /// </summary>
        public Task<bool> DeleteByIdAsync(dynamic id);

        /// <summary>
        /// 异步根据条件删除
        /// </summary>
        public Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 异步根据ID列表批量删除
        /// </summary>
        public Task<bool> DeleteByIdsAsync(dynamic[] ids);

        #endregion
    }
}

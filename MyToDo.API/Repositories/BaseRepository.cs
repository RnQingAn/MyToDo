
using MyToDo.API.Content;
using SqlSugar;
using System.Linq.Expressions;

namespace MyToDo.API.Repositories
{
    public  class BaseRepository<T> : SimpleClient<T>, IBaseRepository<T> where T : class, new()
    {
        protected  ISqlSugarClient _db;

        /// <summary>
        /// 构造函数 - 通过依赖注入获取 ISqlSugarClient
        /// </summary>
        /// <param name="db">数据库客户端</param>
        public BaseRepository(ISqlSugarClient db)
        {
            _db = db;
        }

        //public Task<bool> AnyAsync(Expression<Func<T, bool>> whereExpression)
        //{
        //    return _db.Queryable<T>().AnyAsync(whereExpression);
        //}

        //public async Task<bool> DeleteByIdsAsync(List<object> ids)
        //{
        //    return await _db.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
        //}

        //public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> whereExpression = null)
        //{
        //    return await _db.Queryable<T>().Where(whereExpression).FirstAsync();
        //}

        //public async Task<List<T>> GetAllAsync()
        //{
        //    return await _db.Queryable<T>().ToListAsync();
        //}

        //public async Task<int> UpdateAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> updateExpression)
        //{
        //    return await _db.Updateable<T>()
        //        .SetColumns(updateExpression)
        //        .Where(whereExpression)
        //        .ExecuteCommandAsync();
        //}

        async Task<T> IBaseRepository<T>.InsertAsync(T entity)
        {
           return await _db.Insertable(entity).ExecuteReturnEntityAsync();
        }

        //async Task<int> IBaseRepository<T>.InsertRangeAsync(List<T> entities)
        //{
        //    return await _db.Insertable(entities).ExecuteCommandAsync();
        //}
    }
}


using MyToDo.API.Content;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Linq.Expressions;

namespace MyToDo.API.Repositories
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {
        public BaseRepository(ISqlSugarClient context) : base(context)
        {
            if (base.Context is null) throw new ArgumentNullException(nameof(ISqlSugarClient), "数据库连接不能为空!");
        }

        public virtual Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
            => base.Context.Queryable<TEntity>().Where(func).ToListAsync();

        public Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
            => base.Context.Queryable<TEntity>().ToPageListAsync(page,size,total);
    }
}

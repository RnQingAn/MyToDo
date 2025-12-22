
using MyToDo.API.Content;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Linq.Expressions;

namespace MyToDo.API.Repositories
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {
        public BaseRepository(ISqlSugarClient context):base (context) {
            if (base.Context is null) throw new ArgumentNullException(nameof(ISqlSugarClient), "数据库连接不能为空!");
        }
    }
}

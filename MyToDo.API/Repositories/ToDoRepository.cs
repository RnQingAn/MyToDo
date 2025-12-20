using MyToDo.API.Content;
using MyToDo.API.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Repositories
{
    public class ToDoRepository :IToDoRepository
    {
        private readonly ISqlSugarClient _db;

        // 正确：只依赖数据库上下文
        public ToDoRepository(ISqlSugarClient db)
        {
            _db= db;
        }
        public async Task<ToDo> InsertAsync(ToDo entity)
        {
            return await _db.Insertable(entity).ExecuteReturnEntityAsync(); 
        }
    }
}

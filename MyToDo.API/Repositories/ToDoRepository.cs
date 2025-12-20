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
    public class ToDoRepository :BaseRepository<ToDo>,IToDoRepository
    {
        // 构造函数：调用基类构造函数
        public ToDoRepository(ISqlSugarClient db) : base(db)
        {
        }
        public async Task<ToDo> InsertAsync(ToDo entity)
        {
            return await _db.Insertable(entity).ExecuteReturnEntityAsync(); 
        }
    }
}

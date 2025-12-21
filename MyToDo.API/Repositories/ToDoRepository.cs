using MyToDo.API.Content;
using MyToDo.API.Entity;
using NLog;
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

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        // 构造函数：调用基类构造函数
        public ToDoRepository(ISqlSugarClient db) : base(db)
        {
        }
        public async Task<ToDo> InsertAsync(ToDo entity)
        {
            Logger.Info("Inserting a new ToDo entity into the database.");
            return await _db.Insertable(entity).ExecuteReturnEntityAsync(); 
        }
    }
}

using MyToDo.API.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Repositories
{
    public interface IToDoRepository :IBaseRepository<ToDo>
    {
        /// <summary>
        /// 异步插入单个实体
        /// </summary>
        public Task<ToDo> InsertAsync(ToDo entity);
    }
}

using MyToDo.API.Entity;
using MyToDo.API.Repositories;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Service
{
    public class ToDoService :BaseService<ToDo>,IToDoService
    {
        
        public ToDoService(IBaseRepository<ToDo> iBaseRepository) :base(iBaseRepository) {
        
        }
        
        public async Task<ToDo> InsertAsync(ToDo entity)
        {
           return await iBaseRepository.InsertAsync(entity);
        }
    }
}

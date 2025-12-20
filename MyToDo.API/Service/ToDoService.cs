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
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository toDoRepository;

        public ToDoService(IToDoRepository toDoRepository)
        {
            this.toDoRepository = toDoRepository;
        }
        public async Task<ToDo> InsertAsync(ToDo entity)
        {
           return await toDoRepository.InsertAsync(entity);
        }
    }
}

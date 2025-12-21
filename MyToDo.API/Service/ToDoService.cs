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
    public class ToDoService : BaseService<ToDo>, IToDoService
    {
        private readonly IToDoRepository iToDoRepository;

        public ToDoService(IToDoRepository iToDoRepository)
        {
            base.iBaseRepository = iToDoRepository;
            this.iToDoRepository = iToDoRepository;
        }


        
    }
}

using MyToDo.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Service
{
    public interface IToDoService
    {
        public Task<ToDo> InsertAsync(ToDo entity);
    }
}

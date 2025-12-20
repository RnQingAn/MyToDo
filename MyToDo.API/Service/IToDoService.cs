using MyToDo.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Service
{
    public interface IToDoService:IBaseService<ToDo>
    {
        //public Task<ToDo> InsertAsync(ToDo entity);
    }
}

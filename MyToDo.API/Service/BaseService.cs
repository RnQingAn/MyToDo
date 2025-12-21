using MyToDo.API.Entity;
using MyToDo.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Service
{
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {

        protected  IBaseRepository<T> iBaseRepository;

        

        public  async Task<T> InsertAsync(T createDto)
        {
            return await iBaseRepository.InsertAsync(createDto);
        }
    }
}

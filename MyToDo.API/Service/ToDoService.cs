using Microsoft.IdentityModel.Tokens;
using MyToDo.API.Entity;
using MyToDo.API.Repositories;
using MyToDo.Parameters;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<bool> ByIdQueryAsync(int id)
        {
            List<ToDo> existToDo = await iToDoRepository.QueryAsync(t => t.Id == id);
            if (existToDo.Count == 1) return existToDo.Count > 0;
            else return false;
        }

        public async Task<bool> ByIdUpdateAsync(int id)
        {
            return false;
            
        }

        public override async Task<bool> InsertAsync(ToDo entity)
        {
            ToDo existToDo = await iToDoRepository.GetByIdAsync(entity.Id);
            if (existToDo is null)
                return await base.InsertAsync(entity);
            else
                return false;
        }

        public override  async Task<bool> UpdateAsync(ToDo entity)
        {
            ToDo existToDo = await iToDoRepository.GetByIdAsync(entity.Id);
            if (existToDo is not null)
                return await base.UpdateAsync(entity);
            else
                return false;
        }

        public async Task<PageResult<ToDo>> QueryByConditionAsync(ToDoParameter toDoParameter)
        {
            var result = await iToDoRepository.QueryAsync(
                t => (string.IsNullOrEmpty(toDoParameter.Search) ? true : t.Title.Contains(toDoParameter.Search))
                && (toDoParameter.Status == 3 ? true : t.Status == toDoParameter.Status),
                toDoParameter.PageIndex, toDoParameter.PageSize, toDoParameter.PageTotal);
            return new PageResult<ToDo>
            {
                Data = result,
                Page = toDoParameter.PageIndex,
                PageSize = toDoParameter.PageSize,
                TotalCount = toDoParameter.PageTotal
            };
        }
    }
}

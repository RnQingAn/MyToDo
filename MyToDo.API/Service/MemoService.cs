using MyToDo.API.Entity;
using MyToDo.API.Repositories;
using MyToDo.Parameters;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Service
{
    public class MemoService: BaseService<Memo>,IMemoService
    {
        private readonly IMemoRepository _imemoRepository;

        public MemoService(IMemoRepository memoRepository)
        {
            base.iBaseRepository = memoRepository;
            this._imemoRepository = memoRepository;
        }

        public async Task<PageResult<Memo>> QueryByConditionAsync(QueryParameter parameter)
        {
            var result = await _imemoRepository.QueryAsync(
                t => string.IsNullOrEmpty(parameter.Search) || t.Title.Contains(parameter.Search),
                parameter.PageIndex, parameter.PageSize, parameter.PageTotal);
            return new PageResult<Memo>
            {
                Data = result,
                Page = parameter.PageIndex,
                PageSize = parameter.PageSize,
                TotalCount = parameter.PageTotal
            };
        }
    }
}

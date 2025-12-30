using MyToDo.API.Entity;
using MyToDo.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Service
{
    public interface IMemoService: IBaseService<Memo>
    {
        

        // 新增方法：传入参数而不是表达式
        Task<PageResult<Memo>> QueryByConditionAsync(QueryParameter parameter);
    }
}

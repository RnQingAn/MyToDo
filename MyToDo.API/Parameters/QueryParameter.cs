using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Parameters
{
    public class QueryParameter
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 15;

        public RefAsync<int> PageTotal { get; set; }=new RefAsync<int>(0);

        public string? Search { get; set; }



    }
}

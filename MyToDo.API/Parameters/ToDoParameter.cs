using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Parameters
{
    public class ToDoParameter:QueryParameter
    {
        public int? Status { get; set; }
    }
}

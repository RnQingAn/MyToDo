using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Entity
{
    [SugarTable("ToDo")]
    public class ToDo:BaseEntity
    {
        [SugarColumn(Length = 50)]
        public string Title { get; set; }
        [SugarColumn(Length = 50)]
        public string Content { get; set; }
        [SugarColumn(Length = 50)]
        public int Status { get; set; }
    }
}

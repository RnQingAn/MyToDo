using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace MyToDo.API.Entity
{
    [SugarTable("Memo")]
    public class Memo: BaseEntity
    {
        [SugarColumn(Length = 50)]
        public string Title { get; set; }
        [SugarColumn(Length = 200)]
        public string Content { get; set; }
    }
}

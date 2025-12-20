using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Entity
{
    [SugarTable("User")]
    public class User: BaseEntity
    {
        [SugarColumn(Length = 20)]
        public string Name { get; set; }
        [SugarColumn(Length = 20)]
        public string UserName { get; set; }
        [SugarColumn(Length = 20)]
        public string Password { get; set; }
        
    }
}

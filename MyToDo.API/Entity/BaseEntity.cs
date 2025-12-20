using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Entity
{
    public class BaseEntity
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        [SugarColumn(Length =30)]
        public string CreateDate { get; set; }

        [SugarColumn(Length =30)]
        public string UpdateDate { get; set; }
    }
}

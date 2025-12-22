using MyToDo.API.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.API.Repositories
{
    public class MemoRepository : BaseRepository<Memo>, IMemoRepository
    {
        public MemoRepository(ISqlSugarClient db) : base(db) { }
    }
}

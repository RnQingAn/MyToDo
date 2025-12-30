using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common.Events
{
    // 页码变更请求事件（从分页控件发往数据区域）
    public class PageIndexChangedEvent : PubSubEvent<int>
    {

    }
}

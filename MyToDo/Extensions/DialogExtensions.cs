using MyToDo.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Extensions
{
    public static class DialogExtensions
    {
        /// <summary>
        /// 推送等待消息
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="model"></param>
        public static void UpdateLoading(this IEventAggregator eventAggregator,UpdateModel model) {
            eventAggregator.GetEvent<UpdateLoadingEventHandler>().Publish(model);
        }
        /// <summary>
        /// 注册等待消息
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="action"></param>
        public static void Register(this IEventAggregator eventAggregator, Action<UpdateModel> action)
        {
            eventAggregator.GetEvent<UpdateLoadingEventHandler>().Subscribe(action);
        }
    }
}

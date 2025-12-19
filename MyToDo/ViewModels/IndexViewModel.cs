using MyToDo.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        private ObservableCollection<TaskBar> taskBars = new ObservableCollection<TaskBar>();

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { 
                taskBars = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<ToDoDto> toDoDtos = new ObservableCollection<ToDoDto>();

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set
            {
                toDoDtos = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<MemoDto> memoDtos = new ObservableCollection<MemoDto>();

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set
            {
                memoDtos = value;
                RaisePropertyChanged();
            }
        }

        public IndexViewModel()
        {
            CreateTaskBars();
            CreateToDoDtos();
        }

        private void CreateToDoDtos()
        {
            for (int i = 0; i < 10; i++)
            {
                ToDoDtos.Add(new() { Title = "代办"+i, Content ="正在处理....",Status=0 });
                MemoDtos.Add(new() { Title = "备忘"+i, Content = "我的密码", Status = 0 });
            }
        }

        private void CreateTaskBars()
        {
            taskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Content = "1", Color = "#FF0CA0FF", Target = "" });
            taskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Content = "11", Color = "#FF1ECA3A", Target = "" });
            taskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成率", Content = "100%", Color = "#FF02C6DC", Target = "" });
            taskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Content = "1111", Color = "#FFFFA000", Target = "" });
        }
    }
}

using AutoMapper;
using MyToDo.API.Entity;
using MyToDo.API.Service;
using MyToDo.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        private readonly IToDoService _toDoService;
        private readonly IMapper _mapper;
        private readonly IMemoService _memoService;
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

        public IndexViewModel(IContainerProvider containerProvider,IToDoService toDoService,IMapper mapper,IMemoService memoService) : base(containerProvider)
        {
            _toDoService = toDoService;
            _mapper = mapper;
            _memoService = memoService;
            InitData();
            CreateTaskBars();
        }

        private async void InitData()
        {
            UpdateLodingg(true);
            await Task.Delay(1000);
            var toDoLists =await _toDoService.GetListAsync();
            var memoLists= await _memoService.GetListAsync();
            ToDoDtos = _mapper.Map<ObservableCollection<ToDoDto>>(toDoLists);
            MemoDtos = _mapper.Map<ObservableCollection<MemoDto>>(memoLists);
            UpdateLodingg(false);
        }
        private async void CreateTaskBars()
        {
            
            taskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Content = "1", Color = "#FF0CA0FF", Target = "" });
            taskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Content = "11", Color = "#FF1ECA3A", Target = "" });
            taskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成率", Content = "100%", Color = "#FF02C6DC", Target = "" });
            taskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Content = "1111", Color = "#FFFFA000", Target = "" });
            
            
        }
    }
}

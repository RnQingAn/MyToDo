using AutoMapper;
using MyToDo.API.Entity;
using MyToDo.API.Repositories;
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
   public class ToDoViewModel: NavigationViewModel
    {
        private readonly IToDoService _iToDoService;
        private readonly IMapper mapper;
        public DelegateCommand AddCommand { get;private set; }
        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }


        public ToDoViewModel(IToDoService toDoService,IMapper mapper, IContainerProvider containerProvider):base(containerProvider)
        {
            this._iToDoService = toDoService;
            this.mapper = mapper;
           
            AddCommand = new DelegateCommand(Add);
            //InitToDoDtos();
        }
        /// <summary>
        /// 添加待办
        /// </summary>
        private void Add()
        {
            IsRightDrawerOpen = true;
        }

        private int page=0;

        public int Page
        {
            get => page;
            set { page = value; }
        } 

        private int pageSize=15;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int pageTotal;

        public int PageTotal
        {
            get { return pageTotal; }
            set { pageTotal = value; }
        }
        /// <summary>
        /// 初始化待办列表
        /// </summary>
        /// <returns></returns>
        private async Task InitToDoDtos()
        {
            UpdateLodingg(true);
            await Task.Delay(2000);
            var toDoLists =await _iToDoService.QueryAsync(page, PageSize, PageTotal);
            ToDoDtos = mapper.Map<ObservableCollection<ToDoDto>>(toDoLists);
            UpdateLodingg(false);
        }

        private ObservableCollection<ToDoDto> toDoDtos=new();
        

        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { 
                toDoDtos = value;
                RaisePropertyChanged();
            }
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await InitToDoDtos();
        }
    }
}

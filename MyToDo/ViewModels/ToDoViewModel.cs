using AutoMapper;
using MyToDo.API.Entity;
using MyToDo.API.Repositories;
using MyToDo.API.Service;
using MyToDo.Common.Events;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using MyToDo.Parameters;
using MyToDo.Views;
using NLog;
using Prism.Navigation.Regions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyToDo.ViewModels
{
    public class ToDoViewModel : NavigationViewModel
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IRegionManager _regionManager;
        private readonly IToDoService _iToDoService;
        private readonly IMapper _mapper;
        private ToDoParameter Parameter { get; set; } = new();
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<ToDoDto> SelectedCommand { get; private set; }
        public DelegateCommand<ToDoDto> DeleteCommand { get; private set; }
        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set
            {
                isRightDrawerOpen = value;
                RaisePropertyChanged();
            }
        }
        //private ToDoParameter Parameter { get;  set; }=new ();

        //public ToDoParameter Parameter
        //{
        //    get { return parameter; }
        //    set
        //    {
        //        parameter = value;
        //        RaisePropertyChanged();
        //    }
        //}


        public ToDoViewModel(IRegionManager regionManager, IToDoService toDoService, IMapper mapper, IContainerProvider containerProvider) : base(containerProvider)
        {
            _regionManager = regionManager;
            _iToDoService = toDoService;
            _mapper = mapper;
            SelectedCommand = new DelegateCommand<ToDoDto>(Selected);
            ExecuteCommand = new DelegateCommand<string>(Excute);
            DeleteCommand = new DelegateCommand<ToDoDto>(Delete);
            

            // 订阅页码变更事件
            _eventAggregator.GetEvent<PageIndexChangedEvent>()
                .Subscribe(OnPageIndexChanged);
        }

        private async void OnPageIndexChanged(int obj)
        {
            Parameter.PageIndex = obj;
            await InitToDoDtos(Parameter);
        }

        /// <summary>
        /// 下拉列表选中索引
        /// </summary>
        private int selectedIndex;

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                RaisePropertyChanged();
            }
        }

        private async void Delete(ToDoDto dto)
        {
            var deleteResult = await _iToDoService.DeleteByIdAsync(dto.Id);
            if (deleteResult)
            {
                var todo = ToDoDtos.FirstOrDefault(t => t.Id == dto.Id);
                if (todo != null)
                {
                    //ToDoDtos.Remove(todo);
                    if ((Parameter.PageTotal - 1) % Parameter.PageSize == 0)
                        Parameter.PageIndex = Math.Max(1, Parameter.PageIndex-1);
                    else
                        Parameter.PageIndex = PageData.TotalPages;
                    await InitToDoDtos(Parameter);
                }
            }
        }

        private async void Excute(string obj)
        {
            switch (obj)
            {
                case "Add":
                    Add();
                    break;
                case "Search":
                    await InitToDoDtos(Parameter);
                    break;
                case "Save":
                    await Save();
                    break;
            }
        }

        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(CurrentDto.Title) ||
                string.IsNullOrWhiteSpace(CurrentDto.Content))
                return;
            UpdateLodingg(true);

            if (CurrentDto.Id > 0)
            {
                var toDo = _mapper.Map<ToDo>(CurrentDto);
                var updateResult = await _iToDoService.UpdateAsync(toDo);
                if (updateResult)
                {
                    var todo = ToDoDtos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                    if (todo != null)
                    {
                        todo.Title = CurrentDto.Title;
                        todo.Content = CurrentDto.Content;
                        todo.Status = CurrentDto.Status;
                    }
                }
                //isRightDrawerOpen = false;
            }
            else
            {
                var addResult = await _iToDoService.InsertReturnEntityAsync(_mapper.Map<ToDo>(CurrentDto));
                if (addResult != null)
                {
                    // 新增成功，总记录数加1
                    Parameter.PageTotal += 1;
                    if (Parameter.PageTotal % Parameter.PageSize == 0)
                        Parameter.PageIndex += 1;
                    else
                        Parameter.PageIndex = PageData.TotalPages;
                    
                    
                    //计算出的正确值
                    Parameter.PageIndex = (int)Math.Ceiling((double)Parameter.PageTotal / Parameter.PageSize);
                    await InitToDoDtos(Parameter);
                }
            }
            IsRightDrawerOpen = false;
            await Task.Delay(200);
            UpdateLodingg(false);
        }



        /// <summary>
        /// 搜索条件
        /// </summary>
        private string search;

        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// 添加待办
        /// </summary>
        private void Add()
        {
            CurrentDto = new ToDoDto();
            IsRightDrawerOpen = true;
        }


        private ObservableCollection<ToDoDto> toDoDtos = new();


        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set
            {
                toDoDtos = value;
                RaisePropertyChanged();
            }
        }
        private PageResult<ToDo> _pageData;

        public PageResult<ToDo> PageData
        {
            get { return _pageData; }
            set { _pageData = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 初始化待办列表
        /// </summary>
        /// <returns></returns>
        private async Task InitToDoDtos(ToDoParameter parameter)
        {
            UpdateLodingg(true);
            await Task.Delay(1000);
            //3是全部 1 2 分别表示两种状态
            int status = SelectedIndex == 0 ? 3 : selectedIndex == 1 ? 1 : 0;
            parameter.Status = status;
            parameter.Search = Search;
            ToDoDtos.Clear();
            PageData = await _iToDoService.QueryByConditionAsync(parameter);
            Parameter.PageIndex = PageData.Page;
            Console.WriteLine($"=====>{PageData.Page}");
            ToDoDtos = _mapper.Map<ObservableCollection<ToDoDto>>(PageData.Data);
            
            //数据分页更新
            _regionManager.Regions[PrismManager.ToDoPageViewRegionName].RequestNavigate(nameof(PageView));
            _eventAggregator.GetEvent<PaginationChangedEvent>().Publish(new PaginationData
            {
                PageIndex = PageData.Page,
                PageSize = PageData.PageSize,
                TotalCount = PageData.TotalCount
            });
            UpdateLodingg(false);
        }
        /// <summary>
        /// 编辑选中/新增对象
        /// </summary>
        private ToDoDto currentDto;

        public ToDoDto CurrentDto
        {
            get { return currentDto; }
            set
            {
                currentDto = value;
                RaisePropertyChanged();
            }
        }


        private async void Selected(ToDoDto dto)
        {
            try
            {
                UpdateLodingg(true);
                var existDto = await _iToDoService.GetByIdAsync(dto.Id);

                if (existDto != null)
                {
                    CurrentDto = _mapper.Map<ToDoDto>(existDto);
                    IsRightDrawerOpen = true;
                }
                await Task.Delay(500);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            finally
            {
                UpdateLodingg(false);
            }

        }
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await InitToDoDtos(Parameter);
        }


        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            // 离开页面时取消订阅
            _eventAggregator.GetEvent<PageIndexChangedEvent>().Unsubscribe(OnPageIndexChanged);
        }
    }
}

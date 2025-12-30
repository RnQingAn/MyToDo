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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class MemoViewModel : NavigationViewModel
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IRegionManager _regionManager;
        private readonly IMemoService _iMemoService;
        private readonly IMapper _mapper;
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<MemoDto> SelectedCommand { get; private set; }
        public DelegateCommand<MemoDto> DeleteCommand { get; private set; }
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
        private QueryParameter _parameter = new()
        {
            PageIndex = 1,
            PageSize = 15
        };

        public QueryParameter Parameter
        {
            get { return _parameter; }
            set { _parameter = value; RaisePropertyChanged(); }
        }


        public MemoViewModel(IRegionManager regionManager, IMemoService memoService, IMapper mapper, IContainerProvider containerProvider) : base(containerProvider)
        {
            _regionManager = regionManager;
            _iMemoService = memoService;
            _mapper = mapper;
            SelectedCommand = new DelegateCommand<MemoDto>(Selected);
            ExecuteCommand = new DelegateCommand<string>(Excute);
            DeleteCommand = new DelegateCommand<MemoDto>(Delete);
            //InitToDoDtos();

            // 订阅页码变更事件
            _eventAggregator.GetEvent<PageIndexChangedEvent>()
                .Subscribe(OnPageIndexChanged);
        }

        private async void OnPageIndexChanged(int obj)
        {
            Parameter.PageIndex = obj;
            await InitMemoDtos(Parameter);
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

        private async void Delete(MemoDto dto)
        {
            var deleteResult = await _iMemoService.DeleteByIdAsync(dto.Id);
            if (deleteResult)
            {
                var memo = MemoDtos.FirstOrDefault(t => t.Id == dto.Id);
                if (memo != null)
                {
                    //ToDoDtos.Remove(todo);
                    if ((Parameter.PageTotal - 1) % Parameter.PageSize == 0)
                        Parameter.PageIndex = Math.Max(1, Parameter.PageIndex - 1);
                    else
                        Parameter.PageIndex = PageData.TotalPages;
                    await InitMemoDtos(Parameter);
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
                    await InitMemoDtos(Parameter);
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
                var currentMemo = _mapper.Map<Memo>(CurrentDto);
                var updateResult = await _iMemoService.UpdateAsync(currentMemo);
                if (updateResult)
                {
                    var memo = MemoDtos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                    if (memo != null)
                    {
                        memo.Title = CurrentDto.Title;
                        memo.Content = CurrentDto.Content;
                    }
                }
                //isRightDrawerOpen = false;
            }
            else
            {
                var addResult = await _iMemoService.InsertReturnEntityAsync(_mapper.Map<Memo>(CurrentDto));
                if (addResult != null)
                {
                    Parameter.PageTotal += 1;
                    if (Parameter.PageTotal % Parameter.PageSize == 0)
                        Parameter.PageIndex += 1;
                    else
                        Parameter.PageIndex = PageData.TotalPages;
                    Parameter.PageIndex = (int)Math.Ceiling((double)Parameter.PageTotal / Parameter.PageSize);
                    await InitMemoDtos(Parameter);
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
            CurrentDto = new();
            IsRightDrawerOpen = true;
        }


        private ObservableCollection<MemoDto> _memoDtos = new();


        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return _memoDtos; }
            set
            {
                _memoDtos = value;
                RaisePropertyChanged();
            }
        }
        private PageResult<Memo> _pageData;

        public PageResult<Memo> PageData
        {
            get { return _pageData; }
            set { _pageData = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 初始化待办列表
        /// </summary>
        /// <returns></returns>
        private async Task InitMemoDtos(QueryParameter parameter)
        {
            UpdateLodingg(true);
            await Task.Delay(1000);

            parameter.Search = Search;
            MemoDtos.Clear();
            PageData = new();
            PageData = await _iMemoService.QueryByConditionAsync(parameter);
            MemoDtos = _mapper.Map<ObservableCollection<MemoDto>>(PageData.Data);


            //数据分页更新
            _regionManager.Regions[PrismManager.MemoPageViewRegionName].RequestNavigate(nameof(PageView));
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
        private MemoDto currentDto;

        public MemoDto CurrentDto
        {
            get { return currentDto; }
            set
            {
                currentDto = value;
                RaisePropertyChanged();
            }
        }


        private async void Selected(MemoDto dto)
        {
            try
            {
                UpdateLodingg(true);
                var existDto = await _iMemoService.GetByIdAsync(dto.Id);

                if (existDto != null)
                {
                    CurrentDto = _mapper.Map<MemoDto>(existDto);
                    IsRightDrawerOpen = true;
                }
                await Task.Delay(500);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                UpdateLodingg(false);
            }

        }
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await InitMemoDtos(Parameter);
        }

    }
}

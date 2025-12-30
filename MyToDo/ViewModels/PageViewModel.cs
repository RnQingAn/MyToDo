using MyToDo.Common.Events;
using MyToDo.Common.Models;
using NetTaste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    public class PageViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public DelegateCommand<string> GoPageCommand { get; private set; }

        public PageViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            GoPageCommand = new DelegateCommand<string>(GoPage);
            

        }

        private void GoPage(string obj)
        {
            if (CurrentPage <= 0 || CurrentPage > Pagination.TotalPages) return;

            switch (obj)
            {
                case "NextPage":
                    CurrentPage += 1;
                    HasNextPage = CurrentPage != Pagination.TotalPages;
                    break;
                case "PreviousPage":
                    CurrentPage -= 1;
                    HasPreviousPage = CurrentPage != 1;
                    break;
                case "GoPageIndex":
                    if (int.TryParse(GoPageIndex, out int result))
                        CurrentPage = result;
                    break;
            }
            _eventAggregator.GetEvent<PageIndexChangedEvent>().Publish(CurrentPage);

        }
        private string _goPageIndex = "";

        public string GoPageIndex
        {
            get { return _goPageIndex; }
            set { _goPageIndex = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        private bool _hasPreviousPage = false;

        public bool HasPreviousPage
        {
            get { return _hasPreviousPage; }
            set
            {
                _hasPreviousPage = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        private bool _hasNextPage = false;

        public bool HasNextPage
        {
            get { return _hasNextPage; }
            set
            {
                _hasNextPage = value;
                RaisePropertyChanged();
            }
        }

        private PaginationData pagination;

        public PaginationData Pagination
        {
            get { return pagination; }
            set
            {
                pagination = value;
                RaisePropertyChanged();
            }
        }
        private int _currentPage = 0;

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged();
            }
        }
        private int _totalPage;

        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                RaisePropertyChanged();
            }
        }


        private void OnPaginationChanged(PaginationData data)
        {
            Pagination = data;
            Pagination.PageIndex = Pagination.TotalPages == 0 ? 0 : Pagination.PageIndex;
            UpdateCurrentPage(pagination);
        }

        private void UpdateCurrentPage(PaginationData pageIndex)
        {
            CurrentPage = pageIndex.PageIndex;
            TotalPage = pageIndex.TotalPages;
            HasNextPage = pagination.TotalPages > 1 && CurrentPage != pagination.TotalPages;
            HasPreviousPage = CurrentPage != 1 && CurrentPage != 0;
        }

        void IRegionAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<PaginationChangedEvent>().Subscribe(OnPaginationChanged, ThreadOption.UIThread);
        }

        bool IRegionAware.IsNavigationTarget(NavigationContext navigationContext)
            => true;

        void IRegionAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<PaginationChangedEvent>().Unsubscribe(OnPaginationChanged);
        }
    }
}

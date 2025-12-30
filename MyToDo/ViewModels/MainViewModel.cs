using CommunityToolkit.Mvvm.ComponentModel;
using Dm.util;
using MyToDo.API.Entity;
using MyToDo.API.Service;
using MyToDo.Common.Models;
using MyToDo.Extensions;
using MyToDo.Views;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.ViewModels
{
    
    public partial class MainViewModel:BindableBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IToDoService _toDoService;
        private readonly IMemoService _memoService;
        private readonly IRegionManager _regionManager;
        private IRegionNavigationJournal? _journal;

        public DelegateCommand<MenuBar> NavigateCommand { get;private set; }
        public DelegateCommand GoBackCommand { get; private set; }

        public DelegateCommand GoForwardCommand { get; private set; }

        
        //ctor 构造函数 
        public MainViewModel(IRegionManager regionManager, IToDoService toDoService,IMemoService memoService)
        {
            CreateMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this._regionManager = regionManager;
            _toDoService = toDoService;
            this._memoService = memoService;
            GoBackCommand = new DelegateCommand(() => {
                if (_journal is not null || _journal.CanGoBack)
                    _journal.GoBack();
            });
            GoForwardCommand = new DelegateCommand(() => {
                if (_journal is not null || _journal.CanGoForward)
                    _journal.GoForward();
            });
            
        }

        private async void Navigate(MenuBar bar)
        {
            if (bar is null || string.IsNullOrWhiteSpace(bar.NameSpace)) return;

            _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(bar.NameSpace, back => {
                _journal = back.Context.NavigationService.Journal;
            });
           
        }

        private  ObservableCollection<MenuBar> _menuBars = new ();
        

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return _menuBars; }
            set { 
                _menuBars = value; 
                RaisePropertyChanged();
            }
        }

        
        void CreateMenuBar() {
            MenuBars.Add(new MenuBar { Icon = "Home", NameSpace = "IndexView", Title = "首页" });
            MenuBars.Add(new MenuBar { Icon = "NotebookOutline", NameSpace = "ToDoView", Title = "代办事项" });
            MenuBars.Add(new MenuBar { Icon = "NotebookPlusOutline", NameSpace = "MemoView", Title = "备忘录" });
            MenuBars.Add(new MenuBar { Icon = "CogOutline", NameSpace = "SettingsView", Title = "设置" });

        }

    }
}

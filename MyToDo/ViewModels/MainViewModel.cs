using CommunityToolkit.Mvvm.ComponentModel;
using Dm.util;
using MyToDo.API.Entity;
using MyToDo.API.Service;
using MyToDo.Common.Models;
using MyToDo.Extensions;
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

        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal? journal;

        public DelegateCommand<MenuBar> NavigateCommand { get;private set; }
        public DelegateCommand GoBackCommand { get; private set; }

        public DelegateCommand GoForwardCommand { get; private set; }

        
        //ctor 构造函数 
        public MainViewModel(IRegionManager regionManager, IToDoService toDoService)
        {
            CreateMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;

            GoBackCommand = new DelegateCommand(() => {
                if (journal is not null || journal.CanGoBack)
                    journal.GoBack();
            });
            GoForwardCommand = new DelegateCommand(() => {
                if (journal is not null || journal.CanGoForward)
                    journal.GoForward();
            });
            _toDoService= toDoService;
            
        }

        private async void Navigate(MenuBar bar)
        {
            if (bar is null || string.IsNullOrWhiteSpace(bar.NameSpace)) return;

            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(bar.NameSpace, back => { 
                journal=back.Context.NavigationService.Journal;
            });
            ToDo toDo = new ToDo
            {
                Title = "测试",
                Content = "测试内容",
                UpdateDate = DateTime.Now.ToString(),
                Status = 0,




                CreateDate = DateTime.Now.ToString()
            };
            
            //Logger.Info("添加数据成功");
            //var result= await _toDoService.InsertAsync(toDo);
            var result = await _toDoService.GetByIdAsync(1);
            Logger.Info("添加数据成功");
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

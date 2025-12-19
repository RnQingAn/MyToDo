using CommunityToolkit.Mvvm.ComponentModel;
using MyToDo.Common.Models;
using MyToDo.Extensions;
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
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal journal;

        public DelegateCommand<MenuBar> NavigateCommand { get;private set; }
        public DelegateCommand GoBackCommand { get; private set; }

        public DelegateCommand GoForwardCommand { get; private set; }

        
        //ctor 构造函数 
        public MainViewModel(IRegionManager regionManager)
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
        }

        private void Navigate(MenuBar bar)
        {
            if (bar is null || string.IsNullOrWhiteSpace(bar.NameSpace)) return;

            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(bar.NameSpace, back => { 
                journal=back.Context.NavigationService.Journal;
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

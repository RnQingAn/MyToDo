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
    public class SettingsViewModel:BindableBase
    {
        private readonly IRegionManager regionManager;
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        private ObservableCollection<MenuBar> _menuBars = new();
        public SettingsViewModel(IRegionManager regionManager)
        {
            CreateMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;
        }

        private void Navigate(MenuBar bar)
        {
            if (bar is null || string.IsNullOrWhiteSpace(bar.NameSpace)) return;

            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(bar.NameSpace);
        }

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return _menuBars; }
            set
            {
                _menuBars = value;
                RaisePropertyChanged();
            }
        }
        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar { Icon = "Palette", NameSpace = "SkinView", Title = "个性化" });
            MenuBars.Add(new MenuBar { Icon = "Cog", NameSpace = "", Title = "系统设置" });
            MenuBars.Add(new MenuBar { Icon = "Information", NameSpace = "AboutView", Title = "关于更多" });
        }
    }
}

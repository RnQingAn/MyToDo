

using MyToDo.API.Content;
using MyToDo.API.Repositories;
using MyToDo.API.Service;
using MyToDo.ViewModels;
using MyToDo.Views;
using SqlSugar;
using System.Windows;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册导航
            containerRegistry.RegisterForNavigation<IndexView,IndexViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>();

            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<AboutView>();

            // 方式1：注册 ISqlSugarClient 为单例（推荐）
            try
            {
                containerRegistry.RegisterSingleton<ISqlSugarClient>(() =>
                {
                    // 初始化数据库
                    DbContext.CreateIniDatabase();
                    return DbContext.Db;
                });

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化数据库失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            

            containerRegistry.Register<IToDoRepository, ToDoRepository>();
            containerRegistry.Register<IToDoService, ToDoService>();
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
           
        }
       
    }

}

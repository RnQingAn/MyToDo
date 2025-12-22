

using MyToDo.API.Content;
using MyToDo.API.Repositories;
using MyToDo.API.Service;
using MyToDo.ViewModels;
using MyToDo.Views;
using NLog;
using Prism.Ioc;
using SqlSugar;
using System.Windows;
using System.Windows.Threading;

namespace MyToDo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }
        

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //初始化数据库表
            DbContext.CreateIniDatabase();
            // 方式1：注册 ISqlSugarClient 为单例（推荐）
            // 这行代码只是"告诉"容器如何创建实例，并不会立即执行 DbContext.Db
            // 例如：创建第一个 BaseRepository 时，容器需要提供 ISqlSugarClient
            containerRegistry.RegisterSingleton<ISqlSugarClient>(() => DbContext.Db);


            //注册导航
            containerRegistry.RegisterForNavigation<IndexView,IndexViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>();

            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<AboutView>();

            
            containerRegistry.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            containerRegistry.Register<IToDoRepository, ToDoRepository>();

            containerRegistry.Register(typeof(IBaseService<>), typeof(BaseService<>));
            containerRegistry.Register<IToDoService, ToDoService>();
        }
       

       

        
    }

}

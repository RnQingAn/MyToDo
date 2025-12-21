

using MyToDo.API.Content;
using MyToDo.API.Repositories;
using MyToDo.API.Service;
using MyToDo.ViewModels;
using MyToDo.Views;
using NLog;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            // UI线程未处理异常
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            // 非UI线程未处理异常
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Task异常
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
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
            containerRegistry.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            containerRegistry.Register<IToDoRepository, ToDoRepository>();

            containerRegistry.Register(typeof(IBaseService<>), typeof(BaseService<>));
            containerRegistry.Register<IToDoService, ToDoService>();
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
           
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // 取消事件订阅
            this.DispatcherUnhandledException -= App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException -= TaskScheduler_UnobservedTaskException;
            base.OnExit(e);
        }

        /// <summary>
        /// UI线程异常
        /// </summary>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // 记录日志
            Logger.Error(e.Exception, "UI线程未处理异常");

            // 可以选择处理异常，防止应用程序关闭
            e.Handled = true;

            // 显示错误信息
            MessageBox.Show($"应用程序发生错误：{e.Exception.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// 非UI线程异常
        /// </summary>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            Logger.Error(exception, "非UI线程未处理异常");

            // 如果是终止性错误，记录并退出
            if (e.IsTerminating)
            {
                Logger.Fatal(exception, "应用程序即将终止");
            }
        }

        /// <summary>
        /// Task异常
        /// </summary>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Logger.Error(e.Exception, "Task未观察到的异常");
            e.SetObserved(); // 标记为已观察，避免进程崩溃
        }
    }

}

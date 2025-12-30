

using AutoMapper;
using MyToDo.API.Content;
using MyToDo.API.Repositories;
using MyToDo.API.Service;
using MyToDo.AutoMapper;
using MyToDo.ViewModels;
using MyToDo.Views;
using NLog;
using Prism.Ioc;
using SqlSugar;
using System.Net.Http;
using System.Reflection;
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
            //"当需要构建HttpClient时" → "需要名字为webUri的字符串"
            //containerRegistry.GetContainer().Register<HttpClient>(made: Parameters.Of.Type<string>(serviceKey:"webUri"));
            //containerRegistry.GetContainer().RegisterInstance<string>(@"https://localhost:7232/",serviceKey:"webUri");

            // 注册AutoMapper
            // 注册 AutoMapper 配置
            var config = new MapperConfiguration(cfg =>
            {
                // 扫描当前程序集
                //cfg.AddMaps(Assembly.GetExecutingAssembly());
                cfg.AddProfile<CustomAutoMapperProfile>();
            });

            // 验证配置（建议在开发环境使用）

            config.AssertConfigurationIsValid();


            // 创建 Mapper 实例
            var mapper = config.CreateMapper();

            // 注册到容器
            containerRegistry.RegisterInstance<IMapper>(mapper);
            containerRegistry.RegisterInstance<IConfigurationProvider>(config);

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
            containerRegistry.RegisterForNavigation<PageView,PageViewModel>();

            containerRegistry.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            containerRegistry.Register<IToDoRepository, ToDoRepository>();
            containerRegistry.Register<IMemoRepository, MemoRepository>();

            containerRegistry.Register(typeof(IBaseService<>), typeof(BaseService<>));
            containerRegistry.Register<IToDoService, ToDoService>();
            containerRegistry.Register<IMemoService, MemoService>();
        }
    }

}

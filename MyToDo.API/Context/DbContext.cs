
using Dm.util;
using MyToDo.API.Entity;
using SqlSugar;

using System.IO;


namespace MyToDo.API.Content
{
    public static class DbContext
    {
        
        private static readonly string _dbFileName = "ToDo.db";


        
        public static readonly string _dbPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "SQLite",
            _dbFileName);
        private static readonly string _connectionString =
            $"Data Source={_dbPath};Cache=Shared";
        private static ISqlSugarClient _db;

        public static ISqlSugarClient Db
        {
            get
            {
                // 这里实际上创建的是 SqlSugarScope，但赋值给 ISqlSugarClient 接口变量
                _db ??= new SqlSugarScope(new ConnectionConfig
                {
                    ConnectionString = $"Data Source={_dbPath};Cache=Shared",
                    DbType = DbType.Sqlite,
                    IsAutoCloseConnection = true
                },
                db =>
                {
                    // 配置
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        Console.WriteLine($"SQL: {sql}");
                        //Console.WriteLine("参数: " + Newtonsoft.Json.JsonConvert.SerializeObject(pars));
                    };
                });
                return _db;
            }
        }
        public static void CreateIniDatabase()
        {
            try
            {
                // 创建数据库文件（SQLite会在连接时自动创建，但我们需要确保表结构存在）
                // 注意：如果文件不存在，SqlSugar在第一次操作时会自动创建数据库文件，但我们需要创建表
                // 这里可以调用CodeFirst.InitTables来创建表

                // 检查表是否存在
                var tableExists1 = Db.DbMaintenance.IsAnyTable(nameof(ToDo));
                var tableExists2 = Db.DbMaintenance.IsAnyTable(nameof(Memo));
                var tableExists3 = Db.DbMaintenance.IsAnyTable(nameof(User));

                if (!tableExists1) Db.CodeFirst.InitTables(typeof(ToDo)); // 请替换为你的实体类
                if (!tableExists2) Db.CodeFirst.InitTables(typeof(Memo));
                if (!tableExists3) Db.CodeFirst.InitTables(typeof(User));
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建数据库失败: {ex.Message}");
            }
        }

        // 提供一个属性让外部可以获取当前使用的数据库路径
        public static string DatabasePath => _dbPath;
        //static DbContent()
        //{
        //    // 静态构造函数中输出调试信息
        //    Console.WriteLine($"=== 数据库路径调试信息 ===");
        //    Console.WriteLine($"BaseDirectory: {AppDomain.CurrentDomain.BaseDirectory}");
        //    Console.WriteLine($"数据库文件完整路径: {_dbPath}");
        //    Console.WriteLine($"文件是否存在: {File.Exists(_dbPath)}");

        //    if (!File.Exists(_dbPath))
        //    {
        //        Console.WriteLine($"警告：未找到数据库文件！");
        //        // 可以在这里尝试创建初始数据库
        //        //CreateInitialDatabase();
        //    }
        //}
    }
}

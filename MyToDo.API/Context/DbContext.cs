
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
                // 检查表是否存在
                var tableExists1 = Db.DbMaintenance.IsAnyTable(nameof(ToDo));
                var tableExists2 = Db.DbMaintenance.IsAnyTable(nameof(Memo));
                var tableExists3 = Db.DbMaintenance.IsAnyTable(nameof(User));

                if (!tableExists1) Db.CodeFirst.InitTables(typeof(ToDo)); 
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
        
    }
}

using System;
using Autofac;

namespace DatabaseOpExample
{
    /// <summary>
    /// 数据库操作使用Autofac示例
    /// </summary>
    class DatabaseOpExample
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DatabaseManager>();
            builder.RegisterType<SqlServerDatabase>().As<IDatabase>();
            using (var container = builder.Build())
            {
                var manager = container.Resolve<DatabaseManager>();
                manager.Search("SELECT * FROM USERS");
            }
        }
    }

    public interface IDatabase
    {
        string Name { get; }
        void Select(string commandText);
        void Insert(string commandText);
        void Update(string commandText);
        void Delete(string commandText);
    }

    public class SqlServerDatabase : IDatabase
    {
        //public string Name
        //{
        //    get { return "sqlserver"; }
        //}

        // 使用表达式主体
        public string Name => "sqlserver";

        public void Delete(string commandText)
        {
            Console.WriteLine($"{commandText} is a delete sql in {Name}");
        }

        public void Insert(string commandText)
        {
            Console.WriteLine($"{commandText} is a insert sql in {Name}");
        }

        public void Select(string commandText)
        {
            Console.WriteLine($"{commandText} is a query sql in {Name}");
        }

        public void Update(string commandText)
        {
            Console.WriteLine($"{commandText} is a update sql in {Name}");
        }
    }

    public class OracleDatabase : IDatabase
    {
        //public string Name
        //{
        //    get { return "sqlserver"; }
        //}

        // 使用表达式主体
        public string Name => "oracle";

        public void Delete(string commandText)
        {
            Console.WriteLine($"{commandText} is a delete sql in {Name}");
        }

        public void Insert(string commandText)
        {
            Console.WriteLine($"{commandText} is a insert sql in {Name}");
        }

        public void Select(string commandText)
        {
            Console.WriteLine($"{commandText} is a query sql in {Name}");
        }

        public void Update(string commandText)
        {
            Console.WriteLine($"{commandText} is a update sql in {Name}");
        }
    }

    public class DatabaseManager
    {
        IDatabase _database;
        public DatabaseManager(IDatabase database)
        {
            _database = database;
        }
        public void Search(string commandText)
        {
            _database.Select(commandText);
        }
        public void Add(string commandText)
        {
            _database.Insert(commandText);
        }

        public void Save(string commandText)
        {
            _database.Update(commandText);
        }

        public void Remove(string commandText)
        {
            _database.Delete(commandText);
        }
    }
}

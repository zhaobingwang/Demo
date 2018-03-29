using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection connection = new MySqlConnection("server=127.0.0.1;database=Example;uid=root;pwd=123456;charset='utf8'");


            //connection.Execute("insert into users value(null,'王五','wangwu@qq.com','北京')");


            string name = "zhangsan";
            string email = $"{name}@qq.com";
            string address = "hangzhou";

            int pageIndex = 5;
            int pageSize = 10;
            int start = (pageIndex - 1) * pageSize;

            // 1.Insert
            string sqlInsert = "insert into users value (null,@name,@email,@address)";
            // 1.1 Insert
            //connection.Execute(sqlInsert, new { name, email, address });

            // 1.2 InsertBulk
            var userList = Enumerable.Range(1, 10).Select(i => new User()
            {
                Name = $"Test{i}",
                Email = $"Test{i}@qq.com",
                Address = "Hangzhou"
            });
            //connection.Execute(sqlInsert, userList);

            // 2.Update
            string sqlUpdate = "update users set name=@name where id=@id";
            //connection.Execute(sqlUpdate, new { name = "zhangsan", id = 41 });

            // 3.Delete
            string sqlDelete = "delete from users where id=@id";
            //connection.Execute(sqlDelete, new { id = 41 });

            // 4.Query
            var sqlQuery = "select * from users limit @start,@pageSize";
            var list = connection.Query<User>(sqlQuery, new { start, pageSize });
            Console.WriteLine("用户名\t\t邮箱\t\t地址\t\t");
            foreach (var item in list)
            {
                Console.WriteLine($"{item.Name}\t\t{item.Email}\t\t{item.Address}");
            }

            Console.WriteLine("ok");
        }
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}

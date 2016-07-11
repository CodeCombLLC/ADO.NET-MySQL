# MySQL for .NET Core

You can access this library by using MyGet Feed: `https://www.myget.org/F/pomelo/api/v2/`

### MySQL for ADO.NET Core

After adding myget feed, you can put `Pomelo.Data.MySql` into your `project.json`, and the version should be `1.0.0`.

`MySqlConnection`, `MySqlCommand` and etc are included in the namespace `Pomelo.Data.MySql`. The following console application sample will show you how to use this library to write a record into MySQL database.

```C#
using Pomelo.Data.MySql;

namespace MySqlAdoSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var conn = new MySqlConnection("server=localhost;database=adosample;uid=root;pwd=yourpwd"))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("INSERT INTO `test` (`content`) VALUES ('Hello MySQL')", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
```

### MySQL for Entity Framework Core

You can also use mysql in Entity Framework Core now, We have implemented MySQL Entity Framework Core interfaces. By using a few of lines to use Entity Framework Core with MySQL database, There is a console application sample for accessing MySQL database by using Entity Framework:

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace MySqlTest
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }
    }
    
    public class Blog
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }

    public class MyContext : DbContext
    {
        private static readonly IServiceProvider _serviceProvider
            = new ServiceCollection()
                .AddEntityFrameworkMySql()
                .BuildServiceProvider();

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseInternalServiceProvider(_serviceProvider)
                .UseMySql(@"Server=localhost;database=ef;uid=root;pwd=19931101;");
    }

    public class Program
    {
        public static void Main()
        {
            using (var context = new MyContext())
            {
                // Create database
                context.Database.EnsureCreated();

                // Init sample data
                var user = new User { Name = "Yuuko" };
                context.Add(user);
                var blog = new Blog { Title = "Blog Title", UserId = user.Id };
                context.Add(blog);
                context.SaveChanges();

                // Detect changes test
                blog.Title = "Changed Title";
                context.SaveChanges();

                // Output data
                var ret = context.Blogs.ToList();
                foreach (var x in ret)
                {
                    Console.WriteLine($"{ x.Id } { x.Title }");
                    x.Title = "Hello MySql";
                }
                context.SaveChanges();
            }

            Console.Read();
        }
    }
}
```

Besides, you can view a full project which is a single-user blog system, it was based on this library(MySQL for Entity Framework Core): [View on GitHub](https://github.com/kagamine/yuukoblog-netcore-mysql)

### Contribution

One of the easiest ways to contribute is to participate in discussions and discuss issues. You can also contribute by submitting pull requests with code changes.

### License

MIT

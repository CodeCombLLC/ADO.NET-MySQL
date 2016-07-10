using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace MySqlTest
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }

    public class MyContext : DbContext
    {
        private static readonly IServiceProvider _serviceProvider
            = new ServiceCollection()
                .AddEntityFrameworkMySql()
                .BuildServiceProvider();

        public DbSet<Blog> Blogs { get; set; }

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
                context.Database.EnsureCreated();

                var blog = new Blog() { Title = "Blog Titlle" };

                Console.WriteLine("Adding...");
                context.Add(blog);

                Console.WriteLine("Saving...");
                context.SaveChanges();
                blog.Title = "Changed Title";

                Console.WriteLine("Detecting change...");
                context.ChangeTracker.DetectChanges();

                Console.WriteLine("Saving...");
                context.SaveChanges();
            }
        }
    }
}

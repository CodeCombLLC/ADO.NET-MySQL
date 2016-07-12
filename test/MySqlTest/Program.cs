﻿using System;
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
        public int UserId { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        public DateTimeOffset dto_test { get; set; }
    }
    
    public class Blog
    {
        public Guid Id { get; set; }

        [MaxLength(32)]
        public string Title { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

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
                var user = new User { Name = "Yuuko", dto_test = new DateTimeOffset(new DateTime(2000,1,1), new TimeSpan(1,0,0)) };
                context.Add(user);
                var blog = new Blog { Title = "Blog Title", UserId = user.UserId };
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

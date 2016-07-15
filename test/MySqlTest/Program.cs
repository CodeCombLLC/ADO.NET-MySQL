using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MySqlTest
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }

    public class Blog
    {
        public Guid Id { get; set; }

        [MaxLength(32)]
        public string Title { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string Content { get; set; }

        public JsonObject<List<string>> Tags { get; set; } // Json storage (MySQL 5.7 only)
        
        public JsonObject<Test> Test { get; set; }
    }

    public class Test
    {
        public string Hello { get; set; }

        public int MySql { get; set; }
    }

    public class MyContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
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
                var blog1 = new Blog {
                    Title = "Title #1",
                    UserId = user.UserId,
                    Tags = new List<string>() { "ASP.NET Core", "MySQL", "Pomelo" },
                    Test = new Test { Hello = "#1", MySql = 1 }
                };
                context.Add(blog1);
                var blog2 = new Blog
                {
                    Title = "Title #2",
                    UserId = user.UserId,
                    Tags = new List<string>() { "ASP.NET Core", "MySQL" },
                    Test = new Test { Hello = "#2", MySql = 2 }
                };
                context.Add(blog2);
                context.SaveChanges();

                // Changing and save json object #1
                blog1.Tags.Object.Clear();
                context.SaveChanges();

                // Changing and save json object #2
                blog1.Tags.Object.Add("Pomelo");
                context.SaveChanges();

                var ret2 = context.Blogs.ToList();

                // Output data
                var readUser = context.Users
                    .AsNoTracking()
                    .Include(x => x.Blogs)
                    .Last();
                Console.WriteLine($"{readUser} has written {readUser.Blogs.Count} articles.");
            }
            Console.Read();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Pomelo.Data.MySql;

namespace MySqlTest
{
    public class Aaa
    {
        public int Id { get; set; }

        public string Test { get; set; }

        public virtual ICollection<Bbb> Bbbs { get; set; } = new List<Bbb>();
    }

    public class Bbb
    {
        public int Id { get; set; }

        [ForeignKey("A")]
        public int AaaId { get; set; }

        public virtual Aaa A { get; set; }

        public string Test { get; set; }

        public JsonObject<string[]> Json { get; set; }

        public virtual ICollection<Ccc> Cccs { get; set; } = new List<Ccc>();
    }

    public class Ccc
    {
        public int Id { get; set; }

        [ForeignKey("B")]
        public int BbbId { get; set; }

        public virtual Bbb Bbb { get; set; }

        public string Test { get; set; }
    }

    public class MyContext : DbContext
    {
        public DbSet<Aaa> Aaas { get; set; }

        public DbSet<Bbb> Bbbs { get; set; }

        public DbSet<Ccc> Cccs { get; set; }

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
                
                var a = new Aaa();
                context.Aaas.Add(a);
                context.SaveChanges();

                var b1 = new Bbb { AaaId = a.Id };
                var b2 = new Bbb { AaaId = a.Id };
                context.Bbbs.Add(b1);
                context.Bbbs.Add(b2);
                context.SaveChanges();

                var c1 = new Ccc { BbbId = b1.Id, Test = "#c1" };
                var c2 = new Ccc { BbbId = b1.Id, Test = "#c2" };
                var c3 = new Ccc { BbbId = b2.Id, Test = "#c3" };
                var c4 = new Ccc { BbbId = b2.Id, Test = "#c4" };
                context.Cccs.Add(c1);
                context.Cccs.Add(c2);
                context.Cccs.Add(c3);
                context.Cccs.Add(c4);
                context.SaveChanges();

                var ret = context.Aaas.AsNoTracking().Include(x => x.Bbbs).ThenInclude(x => x.Cccs).First();
                Console.WriteLine(ret.Bbbs.First().Cccs.Count);
            }
            Console.Read();
        }
    }
}

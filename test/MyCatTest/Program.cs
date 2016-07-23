using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Pomelo.Data.MySql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCatTest
{
    public class Program
    {
        [Table("article")]
        public class article
        {
            [Key]
            [Column("id")]
            public int id { get; set; }

            [MaxLength(255)]
            public string title { get; set; }

            public string desc { get; set; }
        }

        public class TestContext : DbContext
        {
            public DbSet<article> article { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);

                optionsBuilder.UseMySql("server=139.129.210.149;port=8066;database=test;uid=test;pwd=test;charset=utf8");
            }
        }

        public static void Main(string[] args)
        {
            //using (var conn = new MySqlConnection("server=139.129.210.149;port=8066;database=test;uid=test;pwd=test;charset=utf8"))
            ////using (var conn = new MySqlConnection("server=localhost;database=blog;uid=root;pwd=19931101;charset=utf8"))
            //{
            //    conn.Open();
            //    //INSERT INTO `article` (`desc`, `title`)\r\nVALUES ('FuckFuck', 'FuckFuck');\r\n\r\nSELECT LAST_INSERT_ID();
            //    using (var cmd = new MySqlCommand("INSERT INTO `article` (`desc`, `title`)\r\nVALUES ('FuckFuck', 'FuckFuck');\r\n\r\nSELECT LAST_INSERT_ID(); ", conn))
            //    using (var reader = cmd.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            for (var i = 0; i < reader.VisibleFieldCount; i++)
            //                Console.Write(reader[i] + " ");
            //            Console.WriteLine();
            //        }
            //    }
            //}


            using (var ctx = new TestContext())
            {
                var a = new article { title = "MyCat Yuuuuuuuuuuuko", desc = "Hello" };
                ctx.Add(a);
                var b= new article { title = "AAAA MyCat Yuuuuuuuuuuuko", desc = "Hello" };
                ctx.Add(b);
                ctx.SaveChanges();
                Console.WriteLine($"a is {a.id}");
                a.desc = "shabi";
                b.desc = DateTime.Now.ToString();
                var c = new article { title = "======", desc = "Hello" };
                ctx.Add(c);
                ctx.Remove(a);
                ctx.SaveChanges();
            }
            Console.WriteLine("Finished");
            Console.Read();
        }
    }
}

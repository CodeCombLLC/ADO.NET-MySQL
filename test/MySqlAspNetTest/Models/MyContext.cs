using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MySqlAspNetTest.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions opt) : base(opt) { }

        public DbSet<Blog> Blogs { get; set; }
    }
}

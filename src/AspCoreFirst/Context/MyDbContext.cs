using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AspCoreFirst.Context
{
    public class MyDbContext: IdentityDbContext<ApplicationUser,IdentityRole<Guid>, Guid>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) :base(options)
        {
            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}

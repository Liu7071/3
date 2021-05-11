using Core;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFWork
{
    public class WebDbContext:DbContext
    {
        public DbSet<Users> users { get; set; }
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        { 
        
        }

         
    }
}

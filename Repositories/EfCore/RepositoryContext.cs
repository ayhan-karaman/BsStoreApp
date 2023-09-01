using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.EfCore.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Repositories.EfCore
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.ApplyConfiguration(new BookConfig());
            // modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
    }
}
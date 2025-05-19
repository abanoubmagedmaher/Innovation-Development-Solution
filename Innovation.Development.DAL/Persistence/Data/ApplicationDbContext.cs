using Innovation.Development.DAL.Entities.Students;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.DAL.Persistence.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext() :base()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString:"Server=.;Database=InnovationDevelopment_db;Trusted_Connection=True ;Encrypt=False");
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>()
                 .HasMany(s => s.Subjects)
                .WithMany(s => s.Students);
            // Seed initial subjects data
            modelBuilder.Entity<Subject>().HasData(
            new Subject { Id = 1, Name = "Arabic" },
            new Subject { Id = 2, Name = "English" },
            new Subject { Id = 3, Name = "Science" },
            new Subject { Id = 4, Name = "History" }
                        );
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

    }
}

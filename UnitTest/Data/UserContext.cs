using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class UserContext : DbContext
    {
        //public UserContext(DbContextOptions<UserContext> options) : base(options)
        //{

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.Id);

            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"server=127.0.0.1;port=3306;database=example;userid=root;password=123456;SslMode=None");
        }
        public DbSet<User> User { get; set; }
    }
}

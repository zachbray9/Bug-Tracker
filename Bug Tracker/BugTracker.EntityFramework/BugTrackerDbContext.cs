using BugTracker.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.EntityFramework
{
    public class BugTrackerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(a => a.Comments)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Ticket)
                .WithMany(t => t.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Author)
                .WithMany(a => a.Tickets)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BugTrackerDB;Trusted_Connection=true");

            base.OnConfiguring(optionsBuilder);
        }

    }
}

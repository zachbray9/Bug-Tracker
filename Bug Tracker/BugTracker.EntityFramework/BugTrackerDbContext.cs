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

        public BugTrackerDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.ProjectUsers)
            //    .WithOne(pu => pu.User);

            //modelBuilder.Entity<ProjectUser>()
            //    .HasOne(pu => pu.Project)
            //    .WithMany(p => p.ProjectUsers);

            //modelBuilder.Entity<ProjectUser>()
            //    .HasMany(pu => pu.AuthoredTickets)
            //    .WithOne(t => t.Author)
            //    .HasForeignKey(t => t.AuthorId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<ProjectUser>()
            //    .HasMany(pu => pu.AssignedTickets)
            //    .WithOne(t => t.Assignee)
            //    .HasForeignKey(t => t.AssigneeId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<ProjectUser>()
            //    .HasMany(pu => pu.Comments)
            //    .WithOne(t => t.Author)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Project>()
            //    .HasMany(p => p.Tickets)
            //    .WithOne(t => t.Project);

            //modelBuilder.Entity<Ticket>()
            //    .HasMany(t => t.Comments)
            //    .WithOne(c => c.Ticket); 

            modelBuilder.Entity<ProjectUser>()
                .HasKey(pu => new { pu.UserId, pu.ProjectId });

            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(pu => pu.UserId);

            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.Project)
                .WithMany(p => p.Users)
                .HasForeignKey(pu => pu.ProjectId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Author)
                .WithMany(u => u.AuthoredTickets)
                .HasForeignKey(t => t.AuthorId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Assignee)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(t => t.AssigneeId)
                .IsRequired(false);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AuthorId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Ticket)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TicketId);
        }
    }
}

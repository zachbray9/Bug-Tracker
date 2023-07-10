using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.EntityFramework
{
    public class BugTrackerDbContextFactory : IDesignTimeDbContextFactory<BugTrackerDbContext>
    {

        public BugTrackerDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<BugTrackerDbContext>();
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BugTrackerDB;Trusted_Connection=true");
            options.EnableSensitiveDataLogging(true);
            options.UseLazyLoadingProxies();

            return new BugTrackerDbContext(options.Options);
        }
    }
}

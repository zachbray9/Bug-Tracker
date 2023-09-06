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
    public class BugTrackerDbContextFactory
    {
        private readonly string ConnectionString;

        public BugTrackerDbContextFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public BugTrackerDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<BugTrackerDbContext>();
            options.UseSqlite(ConnectionString);
            options.EnableSensitiveDataLogging(true);
            options.UseLazyLoadingProxies();

            return new BugTrackerDbContext(options.Options);
        }
    }
}

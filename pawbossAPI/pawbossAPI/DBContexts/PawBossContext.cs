using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using pawbossAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pawbossAPI.DBContexts
{
    public class PawBossContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Pet> Pet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql("server=127.0.0.1;port=3306;user=root;password=;database=PawBossDB", new MySqlServerVersion(new Version(10, 1, 29)))
                .UseLoggerFactory(LoggerFactory.Create(b => b
                    .AddConsole()
                    .AddFilter(Level => Level >= LogLevel.Information)))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

        }
    }
}

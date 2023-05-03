using Microsoft.EntityFrameworkCore;
using Shipping.Infrastructure.Logging.Models;

namespace Shipping.Infrastructure.Logging
{
    public class LogDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "LogDbSchema";
        public virtual DbSet<Log> Logs { get; set; }
        public LogDbContext() { }
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options) { }
    }
}

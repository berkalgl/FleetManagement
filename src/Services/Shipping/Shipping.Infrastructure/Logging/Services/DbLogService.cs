namespace Shipping.Infrastructure.Logging.Services
{
    public class DbLogService : ILogService
    {
        private readonly LogDbContext _dbContext;

        public DbLogService(LogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Log(string message)
        {
            await _dbContext.Logs.AddAsync(new Models.Log { Message = message });
            await _dbContext.SaveChangesAsync();
        }
    }
}

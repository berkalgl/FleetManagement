namespace Shipping.Infrastructure.Logging.Services
{
    public interface ILogService
    {
        Task Log(string message);
    }
}

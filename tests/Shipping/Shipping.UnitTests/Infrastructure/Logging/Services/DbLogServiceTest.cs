using Moq;
using Moq.EntityFrameworkCore;
using Shipping.Infrastructure.Logging;
using Shipping.Infrastructure.Logging.Models;
using Shipping.Infrastructure.Logging.Services;

namespace Shipping.UnitTests.Infrastructure.Logging.Services
{
    public class DbLogServiceTest
    {
        [Fact]
        public async Task Log_ValidMessage_AddsLog()
        {
            //Arrange
            var data = new List<Log>
            {
                new Log()
                {
                    Message = "LogMessage"
                }
            };
            var messageToLog = "message";
            CancellationToken ct = default;

            var logDbContext = new Mock<LogDbContext>();
            logDbContext.Setup(x => x.Logs)
                .ReturnsDbSet(data);

            //Act
            var dbLogService = new DbLogService(logDbContext.Object);
            await dbLogService.Log(messageToLog);

            //Assert
            logDbContext.Verify(logDbContext => logDbContext.Logs.AddAsync(It.IsAny<Log>(), ct), Times.Once);
            logDbContext.Verify(logDbContext => logDbContext.SaveChangesAsync(ct), Times.Once);
        }
    }
}

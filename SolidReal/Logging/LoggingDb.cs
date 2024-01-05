using MySolidAPI;
using SolidReal.Entities;

namespace SolidReal.Logging
{
    public class LoggingDb : ILogging
    {
        private readonly ApplicationDbContext context;

        public LoggingDb(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task Log(string message)
        {
            var log = new Log
            {
                LogDatetime = DateTime.UtcNow,
                Message = message
            };
            context.Logs.Add(log);
            await context.SaveChangesAsync();
        }

        public async Task LogException(Exception exception)
        {
            var log = new Log
            {
                LogDatetime = DateTime.UtcNow,
                Message = exception.Message
            };
            await context.Logs.AddAsync(log);
            await context.SaveChangesAsync();
        }
    }
}

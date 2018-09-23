using Serilog;
using Serilog.Core;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Shared
{
    public sealed class StructuredLogger : IStructuredLogger
    {
        private readonly Logger _logger;

        public StructuredLogger() =>
            _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            _logger.Error(messageTemplate, propertyValues);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            _logger.Debug(messageTemplate, propertyValues);
        }
    }
}

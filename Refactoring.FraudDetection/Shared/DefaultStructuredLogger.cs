using Serilog;
using Serilog.Core;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Shared
{
    public sealed class DefaultStructuredLogger : IStructuredLogger
    {
        private readonly Logger _logger;

        public DefaultStructuredLogger() => _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            _logger.Error(messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate)
        {
            _logger.Error(messageTemplate);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            _logger.Warning(messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate)
        {
            _logger.Warning(messageTemplate);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            _logger.Debug(messageTemplate, propertyValues);
        }

        public void Debug(string messageTemplate)
        {
            _logger.Debug(messageTemplate);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            _logger.Information(messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate)
        {
            _logger.Information(messageTemplate);
        }
    }
}

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Shared
{
    public interface IStructuredLogger
    {
        void Error(string messageTemplate, params object[] propertyValues);
        void Error(string messageTemplate);
        void Warning(string messageTemplate, params object[] propertyValues);
        void Warning(string messageTemplate);
        void Debug(string messageTemplate, params object[] propertyValues);
        void Debug(string messageTemplate);
        void Information(string messageTemplate, params object[] propertyValues);
        void Information(string messageTemplate);
    }
}

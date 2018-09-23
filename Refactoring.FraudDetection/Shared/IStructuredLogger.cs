namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Shared
{
    public interface IStructuredLogger
    {
        void Error(string messageTemplate, params object[] propertyValues);
        void Debug(string messageTemplate, params object[] propertyValues);
    }
}

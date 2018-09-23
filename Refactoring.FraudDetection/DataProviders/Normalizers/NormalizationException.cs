using System;
using System.Runtime.Serialization;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers
{
    public class NormalizationException : Exception
    {
        public NormalizationException() { }

        public NormalizationException(string message) : base(message) { }

        public NormalizationException(string message, Exception innerException) : base(message, innerException) { }

        protected NormalizationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

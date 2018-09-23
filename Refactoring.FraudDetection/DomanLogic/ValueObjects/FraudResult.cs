namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.ValueObjects
{
    public struct FraudResult
    {
        public FraudResult(int orderId, bool isFraudulent)
        {
            OrderId = orderId;
            IsFraudulent = isFraudulent;
        }

        public int OrderId { get; }

        public bool IsFraudulent { get; }

        //public IEnumerable<IComparable> GetAtomicValues()
        //{
        //    yield return OrderId;
        //    yield return IsFraudulent;
        //}

        //public bool Equals(FraudResult other)
        //{
        //    using (IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator())
        //    using (IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator())
        //    {
        //        while (thisValues.MoveNext() && otherValues.MoveNext())
        //        {
        //            if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
        //            {
        //                return false;
        //            }

        //            if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
        //            {
        //                return false;
        //            }
        //        }

        //        return !thisValues.MoveNext() && !otherValues.MoveNext();
        //    }
        //}

        //public override bool Equals(object obj)
        //{
        //    if (ReferenceEquals(null, obj))
        //    {
        //        return false;
        //    }

        //    return obj is FraudResult result && Equals(result);
        //}

        //public override int GetHashCode()
        //{
        //    return GetAtomicValues()
        //        .Select(x => null != x ? x.GetHashCode() : 0)
        //        .Aggregate((x, y) => x ^ y);
        //}
    }
}

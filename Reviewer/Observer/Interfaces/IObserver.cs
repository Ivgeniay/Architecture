using System;

namespace Review
{
    public interface IObserver : IDisposable
    {
        public void AddReview(IReview ireview);
    }
}

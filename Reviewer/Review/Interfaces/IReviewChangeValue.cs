using System;

namespace Review
{
    public interface IReviewChangeValue<T>
    {
        public event Action<T, T> OnChange;
    }
}

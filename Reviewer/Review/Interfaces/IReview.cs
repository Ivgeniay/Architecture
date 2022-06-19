using System;

namespace Review
{
    public interface IReview
    {
        public event Action<object> OnChange;
    }
}

using System;

namespace Review
{
    public class ReviewVariable<T> : IReview
    {
        public ReviewVariable()
        {
            Value = default;
        }

        public ReviewVariable(T defaultValue)
        {
            Value = defaultValue;
        }

        public event Action<object> OnChange;

        public T Value {
            get => _value;
            set {
                _value = value;
                OnChange?.Invoke(value);
            }
        }
        private T _value;

        public override string ToString() =>
                                Value.ToString();
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review
{
    public class ReviewVariableChangeValue<T> : IReviewChangeValue<T>
    {
        public ReviewVariableChangeValue()
        {
            Value = default;
        }

        public ReviewVariableChangeValue(T defaultValue)
        {
            Value = defaultValue;
        }
        /// <summary>
        /// old value, new value
        /// </summary>
        public event Action<T, T> OnChange;

        private T value;
        public T Value {
            get => value;
            set {
                var oldValue = this.value;
                this.value = value;
                OnChange?.Invoke(oldValue, value);
            }
        }

        public override string ToString() =>
                                Value.ToString();


    }
}

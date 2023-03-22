using System;

namespace Utilits.ObservebleVariables
{
    internal class ObservebleVariable<T>
    {
        public Action<T> OnValueChangedEvent;

        private T value;
        public T Value { 
            get  { 
                return value; 
            }
            set {
                if (!this.value.Equals(value))
                    OnValueChangedEvent?.Invoke(value);
                
                this.value = value;
            }
        }
    }
}

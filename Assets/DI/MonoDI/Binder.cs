using DI.Containers;
using UnityEngine;

namespace DI.MonoDI
{
    public abstract class Binder : MonoBehaviour
    {
        public IContainerBuilder Bind(IContainerBuilder builder) {
            return BindContainer(builder);
        }

        protected virtual IContainerBuilder BindContainer(IContainerBuilder builder) {
            return builder;
        }
    }
}

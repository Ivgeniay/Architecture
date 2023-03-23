using DI.Containers;
using UnityEngine;

namespace DI.MonoDI
{
    public abstract class Binder : MonoBehaviour
    {
        public virtual IContainerBuilder BindContainer(IContainerBuilder builder) {
            return builder;
        }
    }
}

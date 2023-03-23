using System.Collections;
using System.Reflection;
using UnityEngine;

namespace DI.MonoDI
{
    public static class ContainerExtension
    {
        public static T Instantiate<T>(this MonoDI container, T instance) where T : MonoBehaviour
        {
            var inst = UnityEngine.Object.Instantiate(instance);
            InvokeConstructor(container, instance);
            return inst;
        }
        public static T Instantiate<T>(this MonoDI container, T instance, Transform parent = null) where T : MonoBehaviour
        {
            var inst = UnityEngine.Object.Instantiate(instance, parent);
            InvokeConstructor(container, instance);
            return inst;
        }
        public static T Instantiate<T>(this MonoDI container, T instance, Vector3 position, Quaternion quaternion) where T : MonoBehaviour
        {
            var inst = UnityEngine.Object.Instantiate(instance, position, quaternion);
            InvokeConstructor(container, instance);
            return inst;
        }

        public static T Instantiate<T>(this MonoDI container, T instance, Vector3 position, Quaternion quaternion, Transform parent) where T : MonoBehaviour
        {
            var inst = UnityEngine.Object.Instantiate(instance, position, quaternion);
            InvokeConstructor(container, instance);
            return inst;
        }

        public static T Instantiate<T>(this MonoDI container, T instance, Transform parent, bool worldPositionStays) where T : MonoBehaviour
        {
            var inst = UnityEngine.Object.Instantiate(instance, parent, worldPositionStays);
            InvokeConstructor(container, instance);
            return inst;
        }



        private static void InvokeConstructor<T>(this MonoDI container, T inst) where T : MonoBehaviour {
            var getMonoFromGO = container.GetMethod("GetMonoFromGO");
            var invokeConstructor = container.GetMethod("InvokeConstructor");

            var monoBehavioursOnObject = (IEnumerable)getMonoFromGO.Invoke(container, new object[] { inst.gameObject });

            foreach (var monoClasses in monoBehavioursOnObject)
            {
                invokeConstructor.Invoke(container, new object[] { monoClasses });
            }
        }

        private static MethodInfo GetMethod(this MonoDI container, string methodName) =>
            container.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        


    }
}

using DI.ActivationBuilds;
using DI.Containers;
using DI.Containers.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DI.MonoDI
{
    public class MonoDI : MonoBehaviour {

        private IScope scope;
        private static MonoDI instance;
        public static MonoDI Instance { get  {
                if (instance == null) {
                    instance = FindObjectOfType<MonoDI>();
                    if (instance is null) {
                        var go = new GameObject("[CONTAINER]");
                        instance = go.AddComponent<MonoDI>();
                        DontDestroyOnLoad(go);
                    }
                }
                return instance;
            }
        }
        private bool isStarted;

        private IContainerBuilder DependenciesRegister(IContainerBuilder builder)
        {
            return builder;
        }


        private IScope ContainerBuild() {
            var builder = new ContainerBuilder(new LambdaBasedActivationBuild());

            var binders = FindObjectsOfType<Binder>().ToList();
            binders.ForEach(el =>
            {
                el.Bind(builder);
            });
            DependenciesRegister(builder);

            return builder
                    .Build()
                    .CreateScope();
        }


        private void Awake() {
            StartContainer();
        }


        private IEnumerable GetMonoFromGO(GameObject gameObject) =>        
             gameObject.GetComponents<MonoBehaviour>();
        
        private void InvokeConstructor(MonoBehaviour el) {
            var ctor = el.GetType().GetMethod("Construct", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (ctor is not null) {
                var @params = ctor.GetParameters();
                var listScopeParam = new List<object>();

                foreach (var param in @params) {
                    var type = param.ParameterType;
                    var resolve = scope.Resolve(type);
                    listScopeParam.Add(resolve);
                }
                ctor.Invoke(el, listScopeParam.ToArray());
            }
        }

        public T GetResolve<T>()
        {
            var type = typeof(T);
            return (T)scope.Resolve(type);
        }
        public void Register()
        {

        }

        public void StartContainer() {
            if (isStarted) return;

            scope = ContainerBuild();
            var monos = FindObjectsOfType<MonoBehaviour>().ToList();

            foreach (var el in monos)            
                InvokeConstructor(el);
            
            isStarted = true;
        }
    }
}

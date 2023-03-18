using Architecture.DI;
using Architecture.DI.ActivationBuilds;
using Architecture.DI.Containers;
using Architecture.DI.Containers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Timers;
using UnityEngine;

namespace Assets.MainProject.Scripts
{
    public delegate int ImportantDelegate(int x, int y);

    internal class TestScr : MonoBehaviour
    {
        public ImportantDelegate Important;

        private void Awake()
        {
            Timer timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
            timer.Start();
            timer.Dispose();

            Func<int, int, int> func = (x, y) => (x + y) * 2;
            Debug.Log("func result: " + func(5, 4));

            ParameterExpression xParam = Expression.Parameter(typeof(int), "x");
            ParameterExpression yParam = Expression.Parameter(typeof(int), "y");
            ConstantExpression @const = Expression.Constant(2);

            Expression sum = Expression.Add(xParam, yParam);
            Expression mult = Expression.Multiply(sum, @const);

            LambdaExpression expr = Expression.Lambda(mult, xParam, yParam);

            var func2 = (Func<int, int, int>)expr.Compile();

            Debug.Log(func2(5, 4));

            var peoples = new List<Person>()
            {
                new Person()
                {
                    Name = "Kolya",
                    Id = 0,
                    Title = "C",
                    dateTime = DateTime.Now,
                },

                new Person()
                {
                    Name = "Zagir",
                    Id = 1,
                    Title = "B",
                    dateTime = DateTime.Now,
                },

                new Person()
                {
                    Name = "Anton",
                    Id = 2,
                    Title = "A",
                    dateTime = DateTime.Now - TimeSpan.FromHours(2),
                }
            };

            var people2 = peoples.OrderBuy("Title").ToList();

            people2.ForEach(el =>
            {
                Debug.Log(el.Name);
            });

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Debug.Log("Hello");
        }

        public class Person
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime dateTime { get; set; }

        }


        //IContainerBuilder builder;

        //private void Awake()
        //{
        //    builder = new ContainerBuilder(new LambdaBasedActivationBuild());
        //    var counainer = builder
        //        .RegistrationSingleton<ITestInterface, TestClass>()
        //        .RegistrationScoped<MainViewModel, MainViewModel>()
        //        .Build();

        //    var scope = counainer.CreateScope(); 
        //    var service = scope.Resolve(typeof(MainViewModel));
        //    var srt = (MainViewModel)service;
        //    srt.ff();
        //    var service2 = scope.Resolve(typeof(MainViewModel));
        //    var srt2 = (MainViewModel)service2;
        //    srt2.ff();

        //    if (srt == srt2)
        //    {

        //    }

        //    var scope2 = counainer.CreateScope();

        //    var service3 = scope2.Resolve(typeof(MainViewModel));
        //    var srt3 = (MainViewModel)service3;
        //    srt3.ff();

        //    Debug.Log(service);
        //}
        //ContainerBuilder ioc = new ContainerBuilder();

        //private void Awake()
        //{
        //    ioc.Register<ITestInterface, TestClass>();
        //    ioc.Register<MainViewModel, MainViewModel>();

        //    var view = ioc.Resolve<MainViewModel>();
        //    Debug.Log(view);
        //}
    }
}

    public static class DynamicOrder
    {
        public static IEnumerable<T> OrderBuy<T>(this IEnumerable<T> source, string propertyName)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");
            Expression propertyExpression = Expression.Property(parameterExpression, propertyName);
            var resultExpression = Expression.Lambda(propertyExpression, parameterExpression);

            var lambda = resultExpression.Compile();

            Type enumerableType = typeof(Enumerable);
            var methods = enumerableType.GetMethods(BindingFlags.Public | BindingFlags.Static);
            var selectedMethod = methods.Where(m => m.Name == "OrderBy" && m.GetParameters().Count() == 2);

            var method = selectedMethod.First();

            method = method.MakeGenericMethod(typeof(T), propertyExpression.Type);

            var result = (IEnumerable<T>)method.Invoke(null, new object[] { source, lambda });
            return result;
        }
    }
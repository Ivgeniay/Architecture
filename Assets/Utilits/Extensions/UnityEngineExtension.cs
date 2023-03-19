using System.Collections;
using UnityEngine;

namespace Utilits.Extensions
{ 
    public static class UnityEngineExtension
    {

        /// <summary>
        /// Return true if object is not exist
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsNotExist(this UnityEngine.Object target) => 
            target == null;

        /// <summary>
        /// Return true if object is exist
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsExist(this UnityEngine.Object target) => 
            target != null;


        //public static IEnumerable<T> GetFromChilds<T> (this Transform transform, Depth depth = Depth.All)
        //{
        //    depth switch
        //    {
        //        Depth.Nested => GetFromNestedChild<T>(transform),
        //        Depth.All => GetFromAllChild<T>(transform)
        //    };

        //}

        //public static List<T> GetFromNestedChild<T>(Transform transform) => transform.GetComponentsInChildren<T>().ToList<T>();
        //public static List<T> GetFromAllChild<T>(Transform transform)
        //{
        //    var arr = new List<T>();

        //    var childList = transform.childCount;



        //    return null;
        //}

    }

    public enum Depth
    {
        All,
        Nested
    }
}

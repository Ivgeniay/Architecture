using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.MainProject.Scripts.Utilits.UnityEngineUtils
{
    internal static class Extensions
    {
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

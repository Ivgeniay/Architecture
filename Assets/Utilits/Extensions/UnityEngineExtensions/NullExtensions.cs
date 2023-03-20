namespace Utilits.Extensions
{
    public static partial class NullExtensions
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
    }
}

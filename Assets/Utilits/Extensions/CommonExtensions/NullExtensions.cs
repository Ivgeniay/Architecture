namespace Utilits.Extensions
{
    public static partial class NullExtensions
    {
        /// <summary>
        /// Return true if object is not exist
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsNotExist(this System.Object target) =>
            target == null;

        /// <summary>
        /// Return true if object is exist
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsExist(this System.Object target) =>
            target != null;
    }
}

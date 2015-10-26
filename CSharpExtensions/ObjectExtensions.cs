namespace CSharpExtensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// returns true if an object is not of the given type
        /// </summary>
        /// <typeparam name="T">the given type</typeparam>
        /// <param name="o">the given object</param>
        /// <returns>true if an object is not of the given type</returns>
        public static bool Isnt<T>(this object o)
        {
            return !(o is T);
        }
    }
}
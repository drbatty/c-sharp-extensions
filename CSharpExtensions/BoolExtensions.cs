using System;

namespace CSharpExtensions
{
    public static class BoolExtensions
    {
        /*While working with MVC and having lots of if statements 
         * where i only care about either true or false, and printing null, or string.Empty in the other case, I came up with:
        */

        public static T WhenTrue<T>(this bool value, Func<T> λ)
        {
            return value ? λ() : default(T);
        }

        public static T WhenTrue<T>(this bool value, T t)
        {
            return value ? t : default(T);
        }

        public static T WhenFalse<T>(this bool value, Func<T> λ)
        {
            return !value ? λ() : default(T);
        }

        public static T WhenFalse<T>(this bool value, T t)
        {
            return !value ? t : default(T);
        }

        /*It allows me to change <%= (someBool) ? "print y" : string.Empty %> into <%= someBool.WhenTrue("print y") %> .
        I only use it in my Views where I mix code and HTML, in code files writing the "longer" version is more clear IMHO.*/

        public static void If<T>(this bool value, T t, Action<T> τ, Action<T> φ)
        {
            if (value)
                τ(t);
            else
                φ(t);
        }

        public static void If(this bool value, Action λ)
        {
            if (value)
                λ();
        }

        public static void IfNot(this bool value, Action λ)
        {
            if (!value)
                λ();
        }

        public static string ToYesOrNo(this bool @bool)
        {
            return @bool ? "yes" : "no";
        }
    }
}

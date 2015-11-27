using System;

namespace CSharpExtensions.Delegate
{
    public static class FuncExtensions
    {
        public static TArg2 ExecuteIfNotNull<TArg1, TArg2>
            (this Func<TArg1, TArg2> λ, TArg1 arg1)
        {
            if (arg1.GetType().IsValueType)
                return λ(arg1);
            // ReSharper disable CompareNonConstrainedGenericWithNull
            return arg1 == null ? default(TArg2) : λ(arg1);
            // ReSharper restore CompareNonConstrainedGenericWithNull
        }

        public static Func<T1, R> Compose<T1, T2, R>(this Func<T1, T2> λ, Func<T2, R> μ)
        {
            return x => μ(λ(x));
        }
    }
}
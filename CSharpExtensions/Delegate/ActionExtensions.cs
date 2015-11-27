using System;

namespace CSharpExtensions.Delegate
{
    public static class ActionExtensions
    {
        /// <summary>
        /// returns an action which invokes the given action, suppressing all exceptions of the given type
        /// </summary>
        /// <typeparam name="T">the type of exception to suppress</typeparam>
        /// <param name="λ">the action to invoke</param>
        /// <returns>an action which invokes the given action, suppressing all exceptions of the given type</returns>
        public static Action Suppress<T>(this Action λ)
            where T : Exception
        {
            return (() =>
            {
                try
                {
                    λ();
                }
                catch (T) { }
            });
        }

        /// <summary>
        /// returns an action which invokes the given action, suppressing all exceptions
        /// </summary>
        /// <param name="λ">the action to invoke</param>
        /// <returns>an action which invokes the given action, suppressing all exceptions</returns>
        public static Action Suppress(this Action λ)
        {
            return λ.Suppress<Exception>();
        }

        /// <summary>
        /// invokes an action which invokes the given action, suppressing all exceptions of the given type
        /// </summary>
        /// <typeparam name="T">the type of exception to suppress</typeparam>
        /// <param name="λ">the action to invoke</param>
        public static void InvokeSuppressed<T>(this Action λ)
            where T : Exception
        {
            λ.Suppress<T>();
        }

        /// <summary>
        /// invokes an action which invokes the given action, suppressing all exceptions
        /// </summary>
        /// <param name="λ">the action to invoke</param>
        public static void InvokeSuppressed(this Action λ)
        {
            λ.InvokeSuppressed<Exception>();
        }

        /// <summary>
        /// Functional extension to project a two-parameter action into a one-parameter action
        /// by using the provided argument parameter as the first parameter for the two-parameter action. 
        /// </summary>
        /// <typeparam name="TArg1">The first parameter type for the action parameter</typeparam>
        /// <typeparam name="TArg2">The second parameter type for the action parameter</typeparam>
        /// <param name="λ">The two-parameter action to project</param>
        /// <param name="arg1"> The first parameter value for the two-parameter action</param>
        /// <returns> a one-parameter action projected from the two-parameter action supplied by using the 
        /// supplied object to the first parameter of the two-parameter action</returns>
        public static Action<TArg2> Project1<TArg1, TArg2>(this Action<TArg1, TArg2> λ, TArg1 arg1)
        {
            return arg2 => λ(arg1, arg2);
        }

        /// <summary>
        /// Functional extension to project a two-parameter action into a one-parameter action
        /// by using the provided argument parameter as the second parameter for the two-parameter action. 
        /// </summary>
        /// <typeparam name="TArg1">The first parameter type for the action parameter</typeparam>
        /// <typeparam name="TArg2">The second parameter type for the action parameter</typeparam>
        /// <param name="λ">The two-parameter action to project</param>
        /// <param name="arg2"> The second parameter value for the two-parameter action</param>
        /// <returns> a one-parameter action projected from the two-parameter action supplied by using the 
        /// supplied object to the second parameter of the two-parameter action</returns>
        public static Action<TArg1> Project2<TArg1, TArg2>(this Action<TArg1, TArg2> λ, TArg2 arg2)
        {
            return arg1 => λ(arg1, arg2);
        }

        public static Action<TArg1> Project23<TArg1, TArg2, TArg3>(this Action<TArg1, TArg2, TArg3> λ, TArg2 arg2,
            TArg3 arg3)
        {
            return arg1 => λ(arg1, arg2, arg3);
        }

        public static Action<TArg2> Project13<TArg1, TArg2, TArg3>(this Action<TArg1, TArg2, TArg3> λ, TArg1 arg1,
            TArg3 arg3)
        {
            return arg2 => λ(arg1, arg2, arg3);
        }

        public static Action<TArg3> Project12<TArg1, TArg2, TArg3>(this Action<TArg1, TArg2, TArg3> λ, TArg1 arg1,
            TArg2 arg2)
        {
            return arg3 => λ(arg1, arg2, arg3);
        }

        /// <summary>
        /// converts a two-parameter action (with both parameters of the same type) to a one-parameter action by passing the same parameter in both places
        /// </summary>
        /// <typeparam name="T">the type of the parameter</typeparam>
        /// <param name="λ">the given two-parameter action</param>
        /// <returns>a one-parameter action by passing the same parameter in both places of the given two-parameter action</returns>
        public static Action<T> Diagonal<T>(this Action<T, T> λ)
        {
            return t => λ(t, t);
        }

        /// <summary>
        /// Turns an action into a function which returns a dummy value (the
        /// default value for the return type). Useful for refactoring code which can
        /// take either an action or a function.
        /// </summary>
        /// <typeparam name="T">The type the action operates upon</typeparam>
        /// <typeparam name="TReturnType">The dummy return type for the function created</typeparam>
        /// <param name="λ">The action to turn into a function</param>
        /// <returns> a function which is the same as the action parameter except that it returns a dummy return value</returns>
        public static Func<T, TReturnType> ToFunc<T, TReturnType>(this Action<T> λ)
        {
            return arg =>
            {
                λ(arg);
                return default(TReturnType);
            };
        }
    }
}

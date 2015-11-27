using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CSharpExtensions.ContainerClasses;

namespace CSharpExtensions.DesignPattern.Structural.Composite
{
    public interface IComposite<T>
    {
        ICollection<IComposite<T>> Children { get; set; }
        T Content { get; set; }
        IComposite<T> CreateComposite(ICollection<IComposite<T>> children);
        IComposite<T> Parent { get; set; }
    }

    public static class CompositeExtensions
    {
        public static int NumberOfChildren<T>(this IComposite<T> composite)
        {
            return composite.Children.Count;
        }

        public static bool HasNoChildren<T>(this IComposite<T> composite)
        {
            return composite.Children.None();
        }

        public static IComposite<T> CreateComposite<T>(this IComposite<T> composite, IComposite<T> child)
        {
            return composite.CreateComposite(new Collection<IComposite<T>> { child });
        }

        public static bool IsTerminal<T>(this IComposite<T> composite)
        {
            return composite.Children.None();
        }

        public static IComposite<T> GetChild<T>(this IComposite<T> composite, int i)
        {
            return composite.Children.ElementAt(i);
        }

        public static IComposite<T> GetLastChild<T>(this IComposite<T> composite)
        {
            return composite.Children.ElementAt(composite.Children.Count - 1);
        }

        public static T GetLastContent<T>(this IComposite<T> composite)
        {
            return composite.GetLastChild().Content;
        }

        public static IComposite<T> Descendant<T>(this IComposite<T> composite, IEnumerable<int> coordinates)
        {
            coordinates = coordinates as List<int> ?? coordinates.ToList();
            return coordinates.None() ? composite :
                Descendant(composite.Children.ElementAt(coordinates.ElementAt(0)), coordinates.RemoveFirst());
        }

        public static void AddChild<T>(this IComposite<T> composite, IComposite<T> child)
        {
            composite.Children.Add(child);
            child.Parent = composite;
        }

        public static void AddDescendant<T>(this IComposite<T> composite, IComposite<T> descendant, IEnumerable<int> coordinates)
        {
            composite.Descendant(coordinates).AddChild(descendant);
        }

        /// <summary>
        /// produces an enumerable from the composite by traversing the composite's tree
        /// in a breadth-first fashion
        /// </summary>
        /// <typeparam name="T">the type contained in the composite</typeparam>
        /// <param name="composite">the given composite</param>
        /// <returns>an enumerable from the composite by traversing the composite's tree
        /// in a breadth-first fashion</returns>
        public static IEnumerable<T> BreadthFirst<T>(this IComposite<T> composite)
        {
            var queue = new Queue<IComposite<T>>();
            queue.Enqueue(composite);
            while (queue.Any())
            {
                var comp = queue.Dequeue();
                yield return comp.Content;

                comp.Children.Each(queue.Enqueue);
            }
        }

        /// <summary>
        /// produces an enumerable from the composite by traversing the composite's tree
        /// in a depth-first postorder fashion
        /// </summary>
        /// <typeparam name="T">the type contained in the composite</typeparam>
        /// <param name="composite">the given composite</param>
        /// <returns>an enumerable from the composite by traversing the composite's tree
        /// in a depth-first postorder fashion</returns>
        public static IEnumerable<T> PostOrdered<T>(this IComposite<T> composite)
        {
            foreach (var t in composite.Children.SelectMany(child => child.PostOrdered()))
                yield return t;

            yield return composite.Content;
        }

        /// <summary>
        /// produces an enumerable from the composite by traversing the composite's tree
        /// in a depth-first preorder fashion
        /// </summary>
        /// <typeparam name="T">the type contained in the composite</typeparam>
        /// <param name="composite">the given composite</param>
        /// <returns>an enumerable from the composite by traversing the composite's tree
        /// in a depth-first preorder fashion</returns>
        public static IEnumerable<T> PreOrdered<T>(this IComposite<T> composite)
        {
            yield return composite.Content;

            foreach (var t in composite.Children.SelectMany(child => child.PreOrdered()))
                yield return t;
        }

        /// <summary>
        /// applies an action to each content element of a composite, in the order given by a tree traversal strategy 
        /// </summary>
        /// <typeparam name="T">the type of content element contained in the composite</typeparam>
        /// <param name="composite">the composite to iterate over</param>
        /// <param name="action">the action to apply</param>
        /// <param name="str">the tree traversal strategy to use</param>
        public static void Each<T>(this IComposite<T> composite, Action<T> action, TreeTraversalStrategy str)
        {
            var func = str.Traversal<T>();
            func(composite).Each(action);
        }

        public static int Count<T>(this IComposite<T> composite)
        {
            var total = 0;
            composite.Each(t => total++, TreeTraversalStrategy.PostOrderDepthFirst); // strategy doesn't matter
            return total;
        }

        public static bool All<T>(this IComposite<T> composite, Func<T, bool> predicate)
        {
            return composite.PreOrdered().All(predicate);
        }

        public static bool Any<T>(this IComposite<T> composite, Func<T, bool> predicate)
        {
            return composite.PreOrdered().Any(predicate);
        }

        public static TAccumulator Inject<TAccumulator, T>(this IComposite<T> composite,
            TAccumulator accumulatorStartValue, Func<TAccumulator, T, TAccumulator> lambda,
            TreeTraversalStrategy str)
        {
            var func = str.Traversal<T>();
            return func(composite).Inject(accumulatorStartValue, lambda);
        }

        public static IEnumerable<T> Select<T>(this IComposite<T> composite, Func<T, T> selector, TreeTraversalStrategy str)
        {
            var func = str.Traversal<T>();
            return func(composite).Select(selector);
        }

        public static IEnumerable<T> Where<T>(this IComposite<T> composite, Func<T, bool> predicate, TreeTraversalStrategy str)
        {
            var func = str.Traversal<T>();
            return func(composite).Where(predicate);
        }

        public static bool None<T>(this IComposite<T> composite, Func<T, bool> predicate)
        {
            return !composite.Any(predicate);
        }

        public static IEnumerable<T> Exclude<T>(this IComposite<T> composite, T element, TreeTraversalStrategy str)
        {
            return composite.Where(t => !Equals(t, element), str);
        }

        public static string Inspect<T>(this IComposite<T> composite)
        {
            var result = composite.Content.Equals(default(T)) ? "" : composite.Content.ToString();
            composite.Children.Each(child => result += "(" + child.Inspect() + ")");
            return result;
        }
    }
}
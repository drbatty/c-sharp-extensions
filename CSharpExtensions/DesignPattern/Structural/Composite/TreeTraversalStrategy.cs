using System;
using System.Collections.Generic;

namespace CSharpExtensions.DesignPattern.Structural.Composite
{
    public enum TreeTraversalStrategy
    {
        PostOrderDepthFirst, PreOrderDepthFirst, BreadthFirst
    }

    public static class TreeTraversalStrategyExtensions
    {
        public static Func<IComposite<T>, IEnumerable<T>> Traversal<T>(this TreeTraversalStrategy str)
        {
            switch (str)
            {
                case TreeTraversalStrategy.PostOrderDepthFirst:
                    return CompositeExtensions.PostOrdered;
                case TreeTraversalStrategy.PreOrderDepthFirst:
                    return CompositeExtensions.PreOrdered;
                case TreeTraversalStrategy.BreadthFirst:
                    return CompositeExtensions.BreadthFirst;
            }
            return null;
        }
    }
}
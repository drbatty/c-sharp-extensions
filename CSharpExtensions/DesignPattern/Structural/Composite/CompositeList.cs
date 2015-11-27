using System.Collections.Generic;
using System.Linq;
using CSharpExtensions.ContainerClasses;

namespace CSharpExtensions.DesignPattern.Structural.Composite
{
    public class CompositeList<T> : IComposite<T>
    {
        #region equality members

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.GetType() == GetType() && Equals((CompositeList<T>)other);
        }

        protected bool Equals(CompositeList<T> other)
        {
            if (Content == null && other.Content != null)
                return false;
            if (Content != null && !Content.Equals(other.Content))
                return false;
            if (Children.Count == 0)
                return true;
            return Children.Count == other.Children.Count &&
                   Children.AllIndices(i => Children.ElementAt(i).Equals(other.Children.ElementAt(i)));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Children != null ? Children.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(Content);
                hashCode = (hashCode * 397) ^ (Parent != null ? Parent.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

        public ICollection<IComposite<T>> Children { get; set; }
        public T Content { get; set; }
        public IComposite<T> Parent { get; set; }

        public IComposite<T> CreateComposite(ICollection<IComposite<T>> children)
        {
            return new CompositeList<T> { Content = Content, Children = children };
        }

        public CompositeList()
        {
            Children = new List<IComposite<T>>();
        }
    }
}
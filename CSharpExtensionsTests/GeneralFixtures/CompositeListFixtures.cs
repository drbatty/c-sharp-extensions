using CSharpExtensions.DesignPattern.Structural.Composite;

namespace CSharpExtensionsTests.GeneralFixtures
{
    public class CompositeListFixtures
    {
        public static CompositeList<string> TestListString
        {
            get
            {
                var listA = new CompositeList<string> { Content = "a" };
                var listB = new CompositeList<string> { Content = "b" };
                var listC = new CompositeList<string> { Content = "c", Children = { listA, listB } };
                var listE = new CompositeList<string> { Content = "e" };
                var listF = new CompositeList<string> { Content = "f" };
                var listG = new CompositeList<string> { Content = "g", Children = { listE, listF } };
                return new CompositeList<string> { Content = "d", Children = { listC, listG } };
            }
        }

        public static CompositeList<string> TestListString2
        {
            get
            {
                var listA = new CompositeList<string> { Content = "a" };
                var listB = new CompositeList<string> { Content = "b" };
                var listC = new CompositeList<string> { Content = "c", Children = { listA, listB } };
                var listE = new CompositeList<string> { Content = "e" };
                var listF = new CompositeList<string> { Content = "ff" };
                var listG = new CompositeList<string> { Content = "g", Children = { listE, listF } };
                return new CompositeList<string> { Content = "d", Children = { listC, listG } };
            }
        }

        public static CompositeList<string> TestListString3
        {
            get
            {
                var listA = new CompositeList<string> { Content = "a" };
                var listB = new CompositeList<string> { Content = "b" };
                var listC = new CompositeList<string> { Content = "c", Children = { listA, listB } };
                var listE = new CompositeList<string> { Content = "e" };
                var listF = new CompositeList<string> { Content = null };
                var listG = new CompositeList<string> { Content = "g", Children = { listE, listF } };
                return new CompositeList<string> { Content = "d", Children = { listC, listG } };
            }
        }
    }
}
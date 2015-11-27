using System.Collections.Generic;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.DesignPattern.Structural.Composite;
using CSharpExtensionsTests.GeneralFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.DesignPattern.Structural.Composite
{
    [TestClass]
    public class CompositeListTests
    {
        [TestMethod]
        public void CompositeListBreadthFirstTest()
        {
            var result = "";
            var enumerable = CompositeListFixtures.TestListString.BreadthFirst();
            enumerable.Each(s => result += s);
            result.ShouldEqual("dcgabef");
        }

        [TestMethod]
        public void CompositeListPostorderedTest()
        {
            var result = "";
            var enumerable = CompositeListFixtures.TestListString.PostOrdered();
            enumerable.Each(s => result += s);
            result.ShouldEqual("abcefgd");
        }


        [TestMethod]
        public void CompositeListPreorderedTest()
        {
            var result = "";
            var enumerable = CompositeListFixtures.TestListString.PreOrdered();
            enumerable.Each(s => result += s);
            result.ShouldEqual("dcabgef");
        }

        [TestMethod]
        public void CompositeListEachPostOrderedTest()
        {
            var result = "";
            CompositeListFixtures.TestListString.Each(c => result += c, TreeTraversalStrategy.PostOrderDepthFirst);
            result.ShouldEqual("abcefgd");
        }

        [TestMethod]
        public void CompositeListEachPreOrderedTest()
        {
            var result = "";
            CompositeListFixtures.TestListString.Each(c => result += c, TreeTraversalStrategy.PreOrderDepthFirst);
            result.ShouldEqual("dcabgef");
        }

        [TestMethod]
        public void CompositeListEachBreadthFirstTest()
        {
            var result = "";
            CompositeListFixtures.TestListString.Each(c => result += c, TreeTraversalStrategy.BreadthFirst);
            result.ShouldEqual("dcgabef");
        }

        [TestMethod]
        public void CompositeListInjectPostOrderedTest()
        {
            CompositeListFixtures.TestListString.Inject("", (acc, c) => acc + c, TreeTraversalStrategy.PostOrderDepthFirst).ShouldEqual("abcefgd");
        }

        [TestMethod]
        public void CompositeListInjectPreOrderedTest()
        {
            CompositeListFixtures.TestListString.Inject("", (acc, c) => acc + c, TreeTraversalStrategy.PreOrderDepthFirst).ShouldEqual("dcabgef");
        }

        [TestMethod]
        public void CompositeListInjectBreadthFirstTest()
        {
            CompositeListFixtures.TestListString.Inject("", (acc, c) => acc + c, TreeTraversalStrategy.BreadthFirst).ShouldEqual("dcgabef");
        }

        [TestMethod]
        public void CompositeListCountTest()
        {
            CompositeListFixtures.TestListString.Count().ShouldEqual(7);
        }

        [TestMethod]
        public void CompositeListCountTestNull()
        {
            CompositeListFixtures.TestListString3.Count().ShouldEqual(7);
        }

        [TestMethod]
        public void CompositeListAllTestTrue()
        {
            CompositeListFixtures.TestListString.All(s => s.Length == 1).ShouldBeTrue();
        }

        [TestMethod]
        public void CompositeListAllTestFalse()
        {
            CompositeListFixtures.TestListString2.All(s => s.Length == 1).ShouldBeFalse();
        }

        [TestMethod]
        public void CompositeListAnyTestTrue()
        {
            CompositeListFixtures.TestListString2.Any(s => s.Length == 2).ShouldBeTrue();
        }

        [TestMethod]
        public void CompositeListAnyTestFalse()
        {
            CompositeListFixtures.TestListString.Any(s => s.Length == 2).ShouldBeFalse();
        }

        [TestMethod]
        public void TestDescendants()
        {
            var descendant = CompositeListFixtures.TestListString.Descendant(1.WrapInList());
            descendant.Count().ShouldEqual(3);
            descendant.PostOrdered().ShouldContain("g", "e", "f");

            descendant = CompositeListFixtures.TestListString.Descendant(0.WrapInList());
            descendant.Count().ShouldEqual(3);
            descendant.PostOrdered().ShouldContain("a", "b", "c");

            descendant = CompositeListFixtures.TestListString.Descendant(new List<int> { 0, 0 });
            descendant.Count().ShouldEqual(1);
            descendant.PostOrdered().ShouldContain("a");

            descendant = CompositeListFixtures.TestListString.Descendant(new List<int> { 0, 1 });
            descendant.Count().ShouldEqual(1);
            descendant.PostOrdered().ShouldContain("b");

            descendant = CompositeListFixtures.TestListString.Descendant(new List<int> { 1, 0 });
            descendant.Count().ShouldEqual(1);
            descendant.PostOrdered().ShouldContain("e");

            descendant = CompositeListFixtures.TestListString.Descendant(new List<int> { 1, 1 });
            descendant.Count().ShouldEqual(1);
            descendant.PostOrdered().ShouldContain("f");

            descendant = CompositeListFixtures.TestListString.Descendant(new List<int>());
            descendant.Count().ShouldEqual(7);
            descendant.PostOrdered().ShouldContain(CompositeListFixtures.TestListString.PostOrdered());
        }

        [TestMethod]
        public void TestEquality()
        {
            CompositeListFixtures.TestListString.ShouldEqual(CompositeListFixtures.TestListString);
            CompositeListFixtures.TestListString.ShouldNotEqual(new CompositeList<string>());
        }

        [TestMethod]
        public void TestAddChild_and_parent()
        {
            var listA = new CompositeList<string> { Content = "a" };
            var listB = new CompositeList<string> { Content = "b" };
            var listC = new CompositeList<string>();
            listC.AddChild(listA);
            listC.AddChild(listB);
            var listE = new CompositeList<string> { Content = "e" };
            var listF = new CompositeList<string> { Content = "f" };
            var listG = new CompositeList<string>();
            listG.AddChild(listE);
            listG.AddChild(listF);
            var listD = new CompositeList<string>();
            listD.AddChild(listC);
            listD.AddChild(listG);

            listD.Count().ShouldEqual(7);
            listA.Parent.ShouldEqual(listC);
            listB.Parent.ShouldEqual(listC);
            listE.Parent.ShouldEqual(listG);
            listF.Parent.ShouldEqual(listG);
            listC.Parent.ShouldEqual(listD);
            listG.Parent.ShouldEqual(listD);
            Assert.IsNull(listD.Parent);
        }
    }
}
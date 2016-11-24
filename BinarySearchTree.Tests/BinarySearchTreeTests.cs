using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task2;
using Task1;


namespace BinarySearchTree.Tests
{
    [TestFixture]
    public class BinarySearchTreeTests
    {
        [Test]
        public void Contains_returnedValues()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            for (int i = 0; i < 5; i++)
            {
                tree.Add(i);
            }

            Assert.AreEqual(tree.Contains(4), true);
        }

        [Test]
        public void Preorder_returnedValues()
        {
            int[] input = new[] {19, 33, 31, 35, 13, 17, 18, 15, 3};
            int[] output = new[] {19, 13, 3, 17, 15, 18, 33, 31, 35};

            BinarySearchTree<int> tree = new BinarySearchTree<int>(input);

            int index = 0;
            foreach (var value in tree.Preorder())
            {
                Assert.AreEqual(output[index], value);
                index++;
            }
        }

        [Test]
        public void Inorder_returnedValues()
        {
            int[] input = new[] { 19, 33, 31, 35, 13, 17, 18, 15, 3 };
            int[] output = new[] { 3, 13, 15, 17, 18, 19, 31, 33, 35 };

            BinarySearchTree<int> tree = new BinarySearchTree<int>(input);

            int index = 0;
            foreach (var value in tree.Inorder())
            {
                Assert.AreEqual(output[index], value);
                index++;
            }
        }

        [Test]
        public void Postorder_returnedValues()
        {
            int[] input = new[] { 19, 33, 31, 35, 13, 17, 18, 15, 3 };
            int[] output = new[] { 3, 15, 18, 17, 13, 31, 35, 33, 19 };

            BinarySearchTree<int> tree = new BinarySearchTree<int>(input);

            int index = 0;
            foreach (var value in tree.Postorder())
            {
                Assert.AreEqual(output[index], value);
                index++;
            }
        }

        public class ComparatorInt : Comparer<int>
        {
            public override int Compare(int x, int y)
            {
                return x.CompareTo(y);
            }
        }

        [Test]
        public void ComparerInt_returnedValues()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(new ComparatorInt());
            List<int> list = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                tree.Add(i);
                list.Add(i);
            }

            list.Sort(new ComparatorInt());
            CollectionAssert.AreEqual(list, tree.Inorder());
        }

        public class ComparatorString : Comparer<string>
        {
            public override int Compare(string x, string y) => String.Compare(x, y, StringComparison.Ordinal);
        }

        [Test]
        public void ComparerString_returnedValues()
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>(new ComparatorString());
            List<string> list = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                tree.Add(i.ToString());
                list.Add(i.ToString());
            }

            list.Sort(new ComparatorString());
            CollectionAssert.AreEqual(list, tree.Inorder());
        }

        public struct Point
        {
            public int x, y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        public class ComparatorPoint : Comparer<Point>
        {
            public override int Compare(Point x, Point y)
            {
                return (x.x + 1).CompareTo(y.x + 1);
            }
        }

        [Test]
        public void ComparerPoint_returnedValues()
        {
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(new ComparatorPoint());
            List<Point> list = new List<Point>();

            for (int i = 0; i < 10; i++)
            {
                tree.Add(new Point(i, i));
                list.Add(new Point(i, i));
            }

            list.Sort(new ComparatorPoint());
            CollectionAssert.AreEqual(list, tree.Inorder());
        }

        public class ComparatorBook : Comparer<Book>
        {
            public override int Compare(Book x, Book y)
            {
                return x.CompareTo(y);
            }
        }

        [Test]
        public void ComparerBook_returnedValues()
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(new ComparatorBook());
            List<Book> list = new List<Book>();

            for (int i = 1; i < 10; i++)
            {
                tree.Add(new Book("title", i, 2000, "Second name"));
                list.Add(new Book("title", i, 2000, "Second name"));
            }

            list.Sort(new ComparatorBook());
            CollectionAssert.AreEqual(list, tree.Inorder());
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class BinarySearchTree<T> : ICollection<T>
    {
        /// <summary>
        /// Node of tree
        /// </summary>
        /// <typeparam name="TData">Type of data</typeparam>
        private class Node<TData>
        {
            public TData Data { get; set; }
            public Node<TData> Left { get; set; }
            public Node<TData> Right { get; set; }

            public Node(TData data)
            {
                Data = data;
            } 
        }

        private Node<T> root;
        private IComparer<T> comparer;
        public int Count { get; private set; }
        public bool IsReadOnly {
            get { return false; }
        }

        public void Clear()
        {
            root = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            var current = root;

            while (current != null)
            {
                var result = comparer.Compare(item, current.Data);

                if (result == 0)
                    return true;
                if (result < 0)
                    current = current.Left;
                else 
                    current = current.Right;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (ReferenceEquals(array, null))
                throw new ArgumentException();

            foreach (var value in this)
                array[arrayIndex++] = value;
        }

        public BinarySearchTree(IComparer<T> comparer)
        {
            if (ReferenceEquals(comparer, null))
                comparer = Comparer<T>.Default;
            else
                this.comparer = comparer;
        }

        public BinarySearchTree() : this(Comparer<T>.Default)
        {
            
        }

        public BinarySearchTree(IEnumerable<T> collection) : this(collection, Comparer<T>.Default)
        {

        }

        public BinarySearchTree(IEnumerable<T> collection, IComparer<T> comparer) : this(comparer)
        {
            if (ReferenceEquals(collection, null))
                throw new ArgumentNullException();

            AddRange(collection);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (ReferenceEquals(collection, null))
                throw new ArgumentNullException();

            foreach (var value in collection)
                Add(value);
        }

        /// <summary>
        /// Adding a new element into the tree
        /// </summary>
        /// <param name="value">New element</param>
        public void Add(T value)
        {
            if (root == null)
                root = new Node<T>(value);
            else
            {
                AddElementRecursion(root, value);
                Count++;
            }
        }

        /// <summary>
        /// Straight tree traversal
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Preorder()
        {
            if (root == null)
                yield break;

            var stack = new Stack<Node<T>>();
            stack.Push(root);
            while (stack.Count != 0)
            {
                Node<T> node = stack.Pop();
                yield return node.Data;

                if (node.Right != null)
                    stack.Push(node.Right);
                if (node.Left != null)
                    stack.Push(node.Left);
            }
        }

        /// <summary>
        /// Reverse traversal of the tree
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Postorder()
        {
            if (root == null)
                yield break;

            var stack = new Stack<Node<T>>();
            Node<T> node = root;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    if (stack.Count > 0 && node.Right == stack.Peek())
                    {
                        stack.Pop();
                        stack.Push(node);
                        node = node.Right;
                    }
                    else
                    {
                        yield return node.Data;
                        node = null;
                    }
                }
                else
                {
                    if (node.Right != null)
                        stack.Push(node.Right);
                    stack.Push(node);
                    node = node.Left;
                }
            }
        }

        /// <summary>
        /// Symmetrical tree traversal
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Inorder()
        {
            if (root == null)
                yield break;

            var stack = new Stack<Node<T>>();
            Node<T> node = root;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    yield return node.Data;

                    if (node.Right != null)
                        node = stack.Pop();
                    else
                        node = null;
                }
                else
                {
                    if (node.Right != null)
                        stack.Push(node.Right);
                    stack.Push(node);
                    node = node.Left;
                }
            }
        }

        /// <summary>
        /// Remove from the wood element
        /// </summary>
        /// <param name="value">Deleted item</param>
        /// <returns>Removed or not</returns>
        public bool Remove(T value)
        {
            if (root == null)
                return false;

            Node<T> current = root, parent = null;
            int result;
            do
            {
                result = comparer.Compare(value, current.Data);

                if (result < 0)
                {
                    parent = current;
                    current = current.Left;
                }
                if (result > 0)
                {
                    parent = current;
                    current = current.Right;
                }
                if (current == null)
                    return false;
            } while (result != 0);

            if (current.Right == null)
            {
                if (current == root)
                    root = current.Left;
                else
                {
                    result = comparer.Compare(current.Data, parent.Data);

                    if (result < 0)
                        parent.Left = current.Left;
                    else
                        parent.Right = current.Left;
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (current == root)
                    root = current.Right;
                else
                {
                    result = comparer.Compare(current.Data, parent.Data);
                    if (result < 0)
                        parent.Left = current.Right;
                    else
                        parent.Right = current.Right;
                }
            }
            else
            {
                Node<T> min = current.Right.Left, prev = current.Right;

                while (min.Left != null)
                {
                    prev = min;
                    min = min.Left;
                }
                prev.Left = min.Right;
                min.Left = current.Left;
                min.Right = current.Right;

                if (current == root)
                    root = min;
                else
                {
                    result = comparer.Compare(current.Data, parent.Data);

                    if (result < 0)
                        parent.Left = min;
                    else
                        parent.Right = min;
                }
            }
            --Count;
            return true;
        }

        private void AddElementRecursion(Node<T> currentNode, T newElement)
        {
            if (comparer.Compare(currentNode.Data, newElement) < 0)
            {
                if (ReferenceEquals(currentNode.Right, null))
                    currentNode.Right = new Node<T>(newElement);
                else
                    AddElementRecursion(currentNode.Right, newElement);
            }
            else
            {
                if (ReferenceEquals(currentNode.Left, null))
                    currentNode.Left = new Node<T>(newElement);
                else
                    AddElementRecursion(currentNode.Left, newElement);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Inorder().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

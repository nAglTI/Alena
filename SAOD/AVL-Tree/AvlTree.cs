using System;

namespace AVL_Tree
{
    public class AvlTree<T> where T: IComparable<T>
    {
        public void Insert(T key) => _root = InsertHelper(_root, key);

        public bool Contains(T key)
        {
            var currentNode = _root;
            
            while (currentNode != null)
            {
                var compareResult = key.CompareTo(currentNode.Key);

                if (compareResult < 0)
                {
                    currentNode = currentNode.Left;
                }
                else if (compareResult > 0)
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void Remove(T key) => _root = RemoveHelper(_root, key);

        public void Clear() => _root = null;

        public int Size { get; private set; }

        private Node InsertHelper(Node node, T key)
        {
            if (node == null)
            {
                ++Size;
                
                return new Node {Key = key};
            }

            var compareResult = key.CompareTo(node.Key);
            
            if (compareResult < 0)
            {
                node.Left = InsertHelper(node.Left, key);
            }
            else if (compareResult > 0)
            {
                node.Right = InsertHelper(node.Right, key);
            }
            else
            {
                return node;
            }

            return Balance(node);
        }
        
        private Node RemoveHelper(Node node, T key)
        {
            if (node == null)
            {
                return null;
            }
            
            var compareResult = key.CompareTo(node.Key);
            
            if (compareResult < 0)
            {
                node.Left = RemoveHelper(node.Left, key);
            }
            else if (compareResult > 0)
            {
                node.Right = RemoveHelper(node.Right, key);
            }
            else
            {
                var left = node.Left;
                var right = node.Right;

                --Size;

                if (right == null)
                {
                    return left;
                }

                var min = FindMinNode(right);

                min.Right = RemoveMin(right);
                min.Left = left;

                return Balance(min);
            }
            
            return Balance(node);
        }

        private static Node Balance(Node node)
        {
            FixHeight(node);

            switch (BalanceFactor(node))
            {
                case 2:
                {
                    if (BalanceFactor(node.Right) < 0)
                    {
                        node.Right = RotateRight(node.Right);
                    }

                    return RotateLeft(node);
                }
                case -2:
                {
                    if (BalanceFactor(node.Left) > 0)
                    {
                        node.Left = RotateLeft(node.Left);
                    }

                    return RotateRight(node);
                }
                default:
                    return node;
            }
        }

        private static Node RotateLeft(Node node)
        {
            var right = node.Right;

            node.Right = right.Left;
            right.Left = node;
            
            FixHeight(node);
            FixHeight(right);

            return right;
        }

        private static Node RotateRight(Node node)
        {
            var left = node.Left;

            node.Left = left.Right;
            left.Right = node;
            
            FixHeight(node);
            FixHeight(left);

            return left;
        }

        private static Node FindMinNode(Node node)
        {
            while (node?.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        private static Node RemoveMin(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }

            node.Left = RemoveMin(node.Left);

            return Balance(node);
        }

        private Node _root;

        private static void FixHeight(Node node)
        {
            var left = Height(node.Left);
            var right = Height(node.Right);

            node.Height = Math.Max(left, right) + 1;
        }

        private static int BalanceFactor(Node node) => Height(node.Right) - Height(node.Left);
        
        private static int Height(Node node) => node?.Height ?? 0;

        internal class Node
        {
            public T Key;

            public int Height = 1;

            public Node Left;

            public Node Right;
        }
    }
}
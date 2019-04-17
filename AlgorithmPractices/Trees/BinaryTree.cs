using System.Collections.Generic;

namespace AlgorithmPractices.Trees {
    public class BinaryTree<T> {
        private IComparer<T> comparer;
        private Node<T> root;
        public BinaryTree() {
            comparer = Comparer<T>.Default;
        }
        /// <summary>
        /// 二叉查找树查询
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Node<T> Find(T value) {
            if (root == null) return default;
            Node<T> p = root;
            while (p != null) {
                if (comparer.Compare(value, p.Value) < 0) {
                    p = p.Left;
                } else if (comparer.Compare(value, p.Value) > 0) {
                    p = p.Right;
                } else {
                    return p;
                }
            }
            return null;
        }
        public void Insert(T value) {
            if (root == null) {
                root = new Node<T>(value);
                return;
            }
            var node = root;
            while (node != null) {
                if (comparer.Compare(value, node.Value) <= 0) {
                    if (node.Left == null) {
                        node.Left = new Node<T>(value);
                        break;
                    }
                    node = node.Left;
                } else if (comparer.Compare(value, node.Value) > 0) {
                    if (node.Right == null) {
                        node.Right = new Node<T>(value);
                        break;
                    }
                    node = node.Right;
                }
            }
        }
        public class Node<TData> {
            public Node<TData> Left;
            public Node<TData> Right;
            public TData Value;
            public Node(TData value) {
                Value = value;
            }
        }

    }
}
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
        public bool Delete(T value) {
            //删除某个节点，先找到这个节点
            Node<T> p = root;
            Node<T> pp = null;
            while (p != null && comparer.Compare(p.Value, value) != 0) {
                pp = p;
                if (comparer.Compare(value, p.Value) > 0)
                    p = p.Right;
                else p = p.Left;
            }
            if (p == null) return false;
            //如果找到的这个节点存在两个子节点
            if (p.Left != null && p.Right != null) {
                //找到最右节点的最小节点
                Node<T> minP = p.Left;
                Node<T> minPP = p;
                while (minP.Left != null) {
                    minPP = minP;
                    minP = minP.Left;
                }
                p.Value = minP.Value;
                p = minP;
                pp = minPP;
            }
            //删除节点是叶子节点或者仅有一个子节点
            Node<T> child; //p的子节点
            if (p.Left != null) child = p.Left;
            else if (p.Right != null) child = p.Right;
            else child = null;

            if (pp == null) root = child;
            else if (pp.Left == p) pp.Left = child;
            else pp.Right = child;
            return true;
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
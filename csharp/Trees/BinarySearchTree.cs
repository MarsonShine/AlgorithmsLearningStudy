using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Trees
{
    public class BinarySearchTree
    {
        public Node tree;
        //二叉查找树查找元素
        //因为二叉查找树左子树的元素都比根节点小，右子树的元素都比根节点的值大，且树种元素不存在重复值。
        public Node Find(int data)
        {
            Node p = tree;
            while (p != null)
            {
                if (data < p.Data) p = p.Left;
                else if (data > p.Data) p = p.Right;
                else return p;
            }
            return null;
        }

        public void Insert(int data)
        {
            if (tree == null)
            {
                tree = new Node(data);
                return;
            }

            Node p = tree;
            //查找要插入的位置，根据上面提到的特征
            while (p != null)
            {
                if (data > p.Data)
                {
                    //查找右子树要插入的位置
                    if (p.Right == null)
                    {
                        p.Right = new Node(data);
                        return;
                    }
                    p = p.Right;
                }
                else
                {
                    //查找左子树要插入的位置
                    if (p.Left == null)
                    {
                        p.Left = new Node(data);
                        return;
                    }
                    p = p.Left;
                }
            }
        }

        public void Delete(int data)
        {
            if (tree == null)
                return;
            Node p = tree; //p 指向要删除的节点，初始化指向根节点
            Node pp = null; // pp 记录的是 p 的父节点
            while (p != null && p.Data != data)
            {
                pp = p;
                if (data > p.Data) p = p.Right;
                else p = p.Left;
            }
            if (p == null) return; //没有子树

            //第一种情况：要删除的节点有两个节点
            if (p.Left != null && p.Right != null)
            { //查找右子树中最小值
                Node minP = p.Right;
                Node minPP = p; //minPP 表示 minP 父节点
                while (minP.Left != null)
                {
                    minPP = minP;
                    minP = minP.Left;
                }
                p.Data = minP.Data; //将 minP 的数据替换到 p 中
                p = minP; //下面就变成删除了 minP
                pp = minPP;
            }
            // 删除节点是叶子结点或者仅有一个子节点
            Node child; //p 的子节点
            if (p.Left != null) child = p.Left;
            else if (p.Right != null) child = p.Right;
            else child = null;

            if (pp == null) tree = child; //删除根节点
            else if (p.Left == p) pp.Left = child;
            else pp.Right = child;
        }

        public Node FindMin()
        {
            if (tree == null) return null;
            Node p = tree;
            while (p.Left != null)
            {
                p = p.Left;
            }
            return p;
        }

        public Node FindMax()
        {
            if (tree == null) return null;
            Node p = tree;
            while (p.Right != null)
            {
                p = p.Right;
            }
            return p;
        }
    }

    /// <summary>
    /// learning from https://github.com/TheAlgorithms/C-Sharp/blob/master/DataStructures/BinarySearchTree/BinarySearchTree.cs
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class BinarySearchTree<TKey>
    {
        private readonly Comparer<TKey> comparer;
        private BinarySearchTreeNode<TKey>? root;
        public BinarySearchTree()
        {
            comparer = Comparer<TKey>.Default;
            root = null;
            Count = 0;
        }

        public BinarySearchTree(Comparer<TKey> comparer)
        {
            this.comparer = comparer;
            root = null;
            Count = 0;
        }
        /// <summary>
        /// 添加一个元素至树中
        /// 二叉查找树不允许元素值重复，左子树比父节点小，右子树比父节点大
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="InvalidOperationException">key重复</exception>
        public void Add(TKey key)
        {
            if (root is null)
            {
                root = new BinarySearchTreeNode<TKey>(key);
            }
            else
            {
                InternalAdd(root, key);
            }
            Count++;
        }

        public bool TryRemove(TKey key)
        {
            if (root is null) return false;
            try
            {
                bool result = InternalRemove(root, root, key);
                if (result)
                {
                    Count--;
                }
                return result;
            }
            catch
            {
                return false;
            }
        }
        public void Remove(TKey key)
        {
            InternalRemove(root, root, key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="root">待删除结点的父节点</param>
        /// <param name="node">待查找要删除的结点</param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks>
        /// 从BST中移除一个结点，移除的场景有三个
        /// <br>
        /// 0.被删除的结点没有子结点，那么可以直接删除该结点
        /// </br>
        /// <br>
        /// 1.被删除的结点有一个结点，那么被删除结点的子结点代替删除节点为父节点，然后删除该结点
        /// </br>
        /// <br>
        /// 3.被删除的结点有两个结点，那么我们首先要找到右子结点下的最小子结点（也就是便利查询右子结点的左子结点，知道没有子结点），然后将要删除的结点与找出来的右子树下的最小左子结点替换，然后删除替换之后的最小子结点。
        /// </br>
        /// </remarks>
        private bool InternalRemove(BinarySearchTreeNode<TKey> parent, BinarySearchTreeNode<TKey> node, TKey key)
        {
            if (parent is null || node is null) return false;

            int comparerResult = comparer.Compare(node.Key, key);
            if (comparerResult > 0)
            {
                return InternalRemove(node, node.Left, key);
            }
            else if (comparerResult < 0)
            {
                return InternalRemove(node, node.Right, key);
            }
            else
            {
                BinarySearchTreeNode<TKey> replacementNode;
                if (node.Left == null || node.Right == null)
                {
                    replacementNode = node.Left ?? node.Right;
                }
                else
                {
                    // 找到要删除的结点 node
                    // case3:
                    // 找出该结点右子树中最小子结点（即不含叶子节点的子结点）
                    var minNodeOfRight = GetMinOfRight(node.Right, node);
                    // 替换
                    replacementNode = new BinarySearchTreeNode<TKey>(minNodeOfRight.Node.Key)
                    {
                        Left = node.Left,
                        Right = node.Right
                    };
                    parent.Left = replacementNode;
                    // 删除最小值
                    if (minNodeOfRight.Node.Right != null)
                    {
                        minNodeOfRight.Parent.Left = minNodeOfRight.Node.Right;
                    }
                    else
                    {
                        minNodeOfRight.Parent.Left = null;
                    }
                }
                if (root == node)
                {
                    root = replacementNode;
                }
                else if (parent.Left == node)
                {
                    parent.Left = replacementNode;
                }
                else
                {
                    parent.Right = replacementNode;
                }
                return true;
            }
        }

        private (BinarySearchTreeNode<TKey> Parent, BinarySearchTreeNode<TKey> Node) GetMinOfRight(BinarySearchTreeNode<TKey> node, BinarySearchTreeNode<TKey> parent)
        {
            var oNode = node;
            while (oNode.Left != null)
            {
                parent = oNode;
                oNode = oNode.Left;
            }
            return (parent, oNode);
        }

        private void InternalAdd(BinarySearchTreeNode<TKey> root, TKey key)
        {
            int comparerResult = comparer.Compare(root.Key, key);
            if (comparerResult > 0)  // root.key > key ，所以放根节点的左子树
            {
                if (root.Left != null)
                {
                    InternalAdd(root.Left, key);
                }
                else
                {
                    var newNode = new BinarySearchTreeNode<TKey>(key);
                    root.Left = newNode;
                }
            }
            else if (comparerResult < 0) // root.Key < key,所以放右子树，逻辑与左子树一样
            {
                if (root.Right != null)
                {
                    InternalAdd(root.Right, key);
                }
                else
                {
                    var newNode = new BinarySearchTreeNode<TKey>(key);
                    root.Right = newNode;
                }
            }
            else
            {
                // 说明值相等，在二叉查找树中，是不允许值有重复的。
                throw new InvalidOperationException($"{nameof(TKey)} 值重复");
            }
        }

        public int Count { get; private set; }
    }

    public class Node
    {
        public int Data;
        public Node Left;
        public Node Right;
        public Node(int data)
        {
            this.Data = data;
        }
    }

    public class BinarySearchTreeNode<TKey>
    {
        public BinarySearchTreeNode(TKey key) => Key = key;

        public BinarySearchTreeNode<TKey> Left { get; set; }
        public BinarySearchTreeNode<TKey> Right { get; set; }
        public TKey Key { get; }
    }
}
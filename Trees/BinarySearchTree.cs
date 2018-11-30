namespace Trees {
    public class BinarySearchTree {
        public Node tree;
        //二叉查找树查找元素
        //因为二叉查找树左子树的元素都比根节点小，右子树的元素都比根节点的值大，且树种元素不存在重复值。
        public Node Find(int data) {
            Node p = tree;
            while (p != null) {
                if (data < p.Data) p = p.Left;
                else if (data > p.Data) p = p.Right;
                else return p;
            }
            return null;
        }

        public void Insert(int data) {
            if (tree == null) {
                tree = new Node(data);
                return;
            }

            Node p = tree;
            //查找要插入的位置，根据上面提到的特征
            while (p != null) {
                if (data > p.Data) {
                    //查找右子树要插入的位置
                    if (p.Right == null) {
                        p.Right = new Node(data);
                        return;
                    }
                    p = p.Right;
                } else {
                    //查找左子树要插入的位置
                    if (p.Left == null) {
                        p.Left = new Node(data);
                        return;
                    }
                    p = p.Left;
                }
            }
        }

        public void Delete(int data) {
            if (tree == null)
                return;
            Node p = tree; //p 指向要删除的节点，初始化指向根节点
            Node pp = null; // pp 记录的是 p 的父节点
            while (p != null && p.Data != data) {
                pp = p;
                if (data > p.Data) p = p.Right;
                else p = p.Left;
            }
            if (p == null) return; //没有子树

            //第一种情况：要删除的节点有两个节点
            if (p.Left != null && p.Right != null) { //查找右子树中最小值
                Node minP = p.Right;
                Node minPP = p; //minPP 表示 minP 父节点
                while (minP.Left != null) {
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

        public Node FindMin() {
            if (tree == null) return null;
            Node p = tree;
            while (p.Left != null) {
                p = p.Left;
            }
            return p;
        }

        public Node FindMax() {
            if (tree == null) return null;
            Node p = tree;
            while (p.Right != null) {
                p = p.Right;
            }
            return p;
        }
    }

    public class Node {
        public int Data;
        public Node Left;
        public Node Right;
        public Node(int data) {
            this.Data = data;
        }
    }
}
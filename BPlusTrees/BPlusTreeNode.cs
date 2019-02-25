namespace BPlusTrees {
    /// <summary>
    /// B+ 树非叶子节点的定义
    /// 假设 keywords=[3,5,8,10]
    /// 4 个数分为 5 个区间：[-INF,3],[3,5],[5,8],[8,10],[10,INF]
    /// 5 个区间分别对应：children[0]...children[4]
    /// m 的值是事先算的
    /// page_size = (m-1)*4[keywords 大小]+m*8[children 大小]
    /// </summary>
    public class BPlusTreeNode {
        public static int m = 5; //5叉树
        public int[] keywords = new int[m - 1]; //键值，用来划分区间
        public BPlusTreeNode[] childrens = new BPlusTreeNode[m]; //保存子节点指针
    }
    /// <summary>
    /// B+ 树的叶子节点的定义
    /// 叶子节点存储的是值，而非区间
    /// 在这个定义里，每个叶子节点存储三个数据行信息
    /// </summary>
    public class BPlusTreeLeafNode {
        public static int k = 3;
        public readonly int[] Keywords; //数据的键值
        public readonly long[] DataAccess; //数据地址
        public BPlusTreeLeafNode(int k) {
            Keywords = new int[k];
            DataAccess = new long[k];
        }

        public BPlusTreeLeafNode Prev { get; set; } //这个节点在链表的前驱结点
        public BPlusTreeLeafNode Next { get; set; } //这个节点在链表的后置结点
    }
}
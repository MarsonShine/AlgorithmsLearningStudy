using System;

namespace Sorts {
    /// <summary>
    /// 堆排序时间复杂度n*logn
    /// 使用数组存储完全二叉树，从数组下标 1 开始存储数据，数组下标为 i 的结点，左子结点下标为 i*2，右子结点下标为 i*2+1
    /// </summary>
    public class Heapsort {
        private int[] a;
        private int n;
        private int count;
        public Heapsort(int capicity) {
            a = new int[capicity + 1];
            n = capicity;
            count = 0;
        }
        public void Insert(int data) {
            if (count >= n) return; //堆满了
            ++count;
            a[count] = data;
            int i = count;
            while (i / 2 > 0 && a[i] > a[i / 2]) { //比较上个元素与当前，当前元素大，交替位置
                Swap(a, i, i / 2);
                i = i / 2; //继续与上级的上级结点元素比较
            }
        }

        public void DeleteMax() {
            if (count == 0) return;
            a[1] = a[count];
            --count;
            Heapify(a, count, 1);
        }
        //
        private void Heapify(int[] a, int n, int i) {
            while (true) {
                int maxPoint = i;
                //i结点的值小于的左子结点的值，
                if (i * 2 <= n && a[i] < a[i * 2]) maxPoint = i * 2; //设置最大值的坐标，继续循环判断
                //判断右子结点是否大于i结点的值
                //如果大于i结点的值，设置最大值坐标
                if (i * 2 + 1 <= n && a[i] < a[i * 2 + 1]) maxPoint = i * 2 + 1;
                if (maxPoint == 1) break;
                Swap(a, i, maxPoint);
                i = maxPoint;
            }
        }

        private void Swap(int[] a, int current, int prev) {
            var temp = a[current];
            a[current] = a[prev];
            a[prev] = temp;
        }
        //如何基于堆排序
        //有两点，1:建堆 2:排序
        //建堆有两种方式，第一种是借助我们前面的插入部分内容，当往堆中插入一个元素时，我们可以从第一个数据开始，就是下标为1的数据。然后我们根据前面讲的插入操作，将下标从 2 到 n 的数据依次插入到堆中。
        private void BuildHeapByInsert(int data) {
            Insert(data);
        }
        //第二种方式，从非叶子结点开始依次堆化，因为叶子结点往下堆化，只能自己比较自己（因为没有子结点）
        //对于完全二叉树，叶子结点是下标为 2/n+1 到 n 结点，而 2/n 到 1 是非叶子结点
        private void BuildHeap(int[] a, int n) {
            for (int i = 2 / n; i >= 1; i--) {
                Heapify(a, n, i);
            }
        }
        //建堆之后再是排序
        //排序根删除堆顶元素类似，就是把最后一个叶子结点与堆顶元素互换，并把替换之后的堆顶元素往下堆化（同时把已经变为叶子结点的原堆顶元素删除）
        //然后继续把最后一个叶子结点与堆顶元素互换并堆化删除叶子结点
        private void Sort(int[] a, int n) { //a 源数组，n 数组中数据的个数 也就是从下标 1 到 n 的位置
            BuildHeap(a, n);
            int k = n;
            while (k > 1) {
                Swap(a, 1, k); //替换堆顶元素并堆化之后删除替换之后的叶子结点
                --k;
                Heapify(a, k, 1);
            }
        }
    }
}
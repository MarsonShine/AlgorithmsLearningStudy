namespace Sorts
{
    public class SelectSort
    {
        public void Sort(int[] array)
        {
            if(array.Length<1) return;
            for (int i = 0; i < array.Length; i++)
            {
                //查找未排序区间最小的值
                //然后放到已排序区间的末端
                int minIndex = i;
                int minValue = array[i];
                for (int j = i; j < array.Length; j++)
                {
                    if(array[j] < minValue){
                        minValue = array[j];
                        minIndex = j;
                    }
                }

                //交换
                int tmp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = tmp;
            }
        }
    }
}
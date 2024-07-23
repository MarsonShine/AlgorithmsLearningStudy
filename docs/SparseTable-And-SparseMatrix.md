在计算机科学和数学中，矩阵是一种重要的数据结构。然而，在许多实际应用中，矩阵中大部分元素都是零，这就是所谓的稀疏矩阵。本文将介绍什么是稀疏矩阵和稀疏表，为什么它们重要，以及如何高效地存储和操作它们。

## 什么是稀疏矩阵？

稀疏矩阵是一种特殊的矩阵，其中大多数元素的值都是零。相对于稠密矩阵（大多数元素都有值），稀疏矩阵的非零元素非常少。这种稀疏性使得我们可以使用特殊的存储格式来节省内存和提高计算效率。

## 为什么稀疏矩阵重要？

在许多实际应用中，如科学计算、图像处理、机器学习和信息检索中，稀疏矩阵非常常见。例如，有限元分析中的矩阵通常是稀疏的，因为每个节点只与少数相邻节点相互作用。通过使用稀疏矩阵，可以显著减少存储需求和计算时间。

## 稀疏矩阵的存储格式

由于稀疏矩阵大部分元素为零，直接使用二维数组存储会浪费大量空间。以下是几种常见的稀疏矩阵存储格式：

1. **压缩行存储（CSR）格式**

   CSR格式通过三个数组来表示稀疏矩阵：

   - **值数组（Values）**：存储所有非零元素。
   - **列索引数组（Column Indexes）**：存储对应非零元素的列索引。
   - **行指针数组（Row Pointers）**：存储每一行的非零元素在值数组中的起始位置，以及最后一个元素之后的位置。

   例子：

   ```
   稀疏矩阵：
   [ 0, 0, 3, 0 ]
   [ 22, 0, 0, 0 ]
   [ 0, 0, 0, 0 ]
   [ 0, 17, 0, 0 ]
   ```

   CSR表示：
   Values: [3, 22, 17]
   Column Indexes: [2, 0, 1]
   Row Pointers: [0, 1, 2, 2, 3]

2. **压缩列存储（CSC）格式**

   与CSR类似，只不过非零元素按列存储，并记录对应的行索引和列指针。

3. **三元组格式**

   每个非零元素用一个三元组（行索引，列索引，值）表示。例如：

   ```
   (0, 2, 3), (1, 0, 22), (3, 1, 17)
   ```

> ### 如何计算CSR格式的行指针数组？
>
> 行指针数组的目的是指示每一行的非零元素在值数组（Values）中的起始位置。行指针数组的最后一个元素表示值数组的长度（即所有非零元素的总数）。以上述提到的 4x4 矩阵为例，计算方法如下：
>
> 1. 初始化行指针数组，长度为行数加1：
>
>    ```
>    Row Pointers: [0, 0, 0, 0, 0]
>    ```
>
> 2. 逐行遍历矩阵，计算每行的非零元素在值数组中的起始位置：
>
>    - **第0行**：
>      - 第0行有1个非零元素（3）。
>      - 第0行的起始位置是0（值数组的索引从0开始）。
>      - 第0行非零元素计数为1，所以更新行指针数组的下一个位置为0+1=1: `[0, 1, 0, 0, 0]`
>    - **第1行**：
>      - 第1行有1个非零元素（22）。
>      - 第1行的起始位置是前一行的起始位置加上前一行的非零元素个数，即1。
>      - 第1行非零元素计数为1，所以更新行指针数组的下一个位置为1+1=2: `[0, 1, 2, 0, 0]`
>    - **第2行**：
>      - 第2行没有非零元素。
>      - 第2行的起始位置是前一行的起始位置，即2。
>      - 第2行没有非零元素，所以行指针数组的下一个位置保持不变为2：`[0, 1, 2, 2, 0]`
>    - **第3行**：
>      - 第3行有1个非零元素（17）。
>      - 第3行的起始位置是前一行的起始位置，即2。
>      - 第3行非零元素计数为1，所以更新行指针数组的下一个位置为2+1=3：`[0, 1, 2, 2, 3]`
>    - 最后一个元素表示值数组的长度：`[0, 1, 2, 2, 3]`

## 什么是稀疏表？

稀疏表与稀疏矩阵类似，通常用于表示大多数元素为空或未使用的表格结构。在某些应用中，如编译器设计中的语法分析表，稀疏表非常有用。通过只存储非零或重要元素，稀疏表可以显著减少内存使用。

## 稀疏表的存储方法

1. **散列表（哈希表）**

   使用键值对存储非空表项，键是行列索引，值是实际的表项值。 例子：

   ```
   稀疏表：
   
     | a | b | c |
   --|---|---|---|
   0 | S1|   |   |
   1 |   | S2|   |
   2 |   |   | S3|
   
   散列表表示：
   {(0, 'a'): 'S1', (1, 'b'): 'S2', (2, 'c'): 'S3'}
   ```

2. **链表**

   每行使用一个链表存储非空表项。 例子：

   ```
   稀疏表：
   
     | a | b | c |
   --|---|---|---|
   0 | S1|   |   |
   1 |   | S2|   |
   2 |   |   | S3|
   
   链表表示：
   行0: (a, S1) -> NULL
   行1: (b, S2) -> NULL
   行2: (c, S3) -> NULL
   ```
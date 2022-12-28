package main

// https://leetcode.cn/problems/binary-tree-right-side-view/
// 二叉树的右视图
func rightSideView(root *TreeNode) []int {
	queue := []*TreeNode{}
	if root != nil {
		queue = append(queue, root)
	}
	res := []int{}
	for len(queue) != 0 {
		length := len(queue)
		for i := 0; i < length; i++ {
			node := queue[0]
			queue = queue[1:]
			if i == (length - 1) {
				res = append(res, node.Val) // 每层的最后元素放入结果数组中
			}
			if node.Left != nil {
				queue = append(queue, node.Left)
			}
			if node.Right != nil {
				queue = append(queue, node.Right)
			}
		}
	}
	return res
}

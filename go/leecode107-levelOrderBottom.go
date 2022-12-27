package main

// https://leetcode.cn/problems/binary-tree-level-order-traversal-ii/
// 层序遍历
func levelOrderBottom(root *TreeNode) [][]int {
	res := levelOrder(root)
	return reverseLevelOrder(res)
}

func reverseLevelOrder(res [][]int) [][]int {
	reverse := [][]int{}
	for i := len(res) - 1; i > -1; i-- {
		reverse = append(reverse, res[i])
	}
	return reverse
}

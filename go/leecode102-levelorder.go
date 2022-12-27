package main

// https://leetcode.cn/problems/binary-tree-level-order-traversal/
// 层序遍历
func levelOrder(root *TreeNode) [][]int {
	res := [][]int{}
	if root == nil {
		return res
	}
	queue := []*TreeNode{}
	queue = append(queue, root)
	tmp := []int{}
	for len(queue) > 0 {
		length := len(queue)
		for i := 0; i < length; i++ {
			node := queue[0]
			queue = queue[1:]
			if node.Left != nil {
				queue = append(queue, node.Left)
			}
			if node.Right != nil {
				queue = append(queue, node.Right)
			}
			tmp = append(tmp, node.Val)
		}
		res = append(res, tmp)
		tmp = []int{}
	}
	return res
}

/* 递归 */
func levelOrderRecursive(root *TreeNode) [][]int {
	res := [][]int{}
	deep := 0
	var orderRecursive func(root *TreeNode, deep int)
	orderRecursive = func(root *TreeNode, deep int) {
		if root == nil {
			return
		}
		if len(res) == deep {
			res = append(res, []int{})
		}
		res[deep] = append(res[deep], root.Val)
		orderRecursive(root.Left, deep+1)
		orderRecursive(root.Right, deep+1)
		return
	}

	orderRecursive(root, deep)
	return res
}

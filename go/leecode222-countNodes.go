package main

// https://leetcode.cn/problems/count-complete-tree-nodes/
// 完全二叉树的节点个数

// bfs
func countNodes(root *TreeNode) int {
	n := 0
	queue := []*TreeNode{}
	if root != nil {
		queue = append(queue, root)
	}
	for len(queue) > 0 {
		for i := 0; i < len(queue); i++ {
			n++
			node := queue[0]
			queue = queue[1:]
			if node.Left != nil {
				queue = append(queue, node.Left)
			}
			if node.Right != nil {
				queue = append(queue, node.Right)
			}
		}

	}

	return n
}

// dfs + 前序遍历
func countNodes2(root *TreeNode) int {
	return dfs_countNodes(root)
}

func dfs_countNodes(node *TreeNode) int {
	if node == nil {
		return 0
	}
	left := dfs_countNodes(node.Left)
	right := dfs_countNodes(node.Right)
	return 1 + left + right
}

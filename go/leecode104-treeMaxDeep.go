package main

func maxDepth(root *TreeNode) int {
	return deep(root, 0)
}

func deep(node *TreeNode, depth int) int {
	if node == nil {
		return depth
	}
	depth++
	maxdepth := deep(node.Left, depth)
	rightHeight := deep(node.Right, depth)
	if maxdepth < rightHeight {
		maxdepth = rightHeight
	}
	return maxdepth
}

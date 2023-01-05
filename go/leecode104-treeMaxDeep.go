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

// 层序遍历
func maxDepth2(root *TreeNode) int {
	queue := []*TreeNode{}
	max := 0
	if root != nil {
		queue = append(queue, root)
	}
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
		}
		max += 1
	}
	return max
}

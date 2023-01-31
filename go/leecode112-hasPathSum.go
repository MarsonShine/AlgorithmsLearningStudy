package main

func hasPathSum(root *TreeNode, targetSum int) bool {
	return hasPathSumRecurision(root, targetSum)
}

func hasPathSumRecurision(root *TreeNode, targetSum int) bool {
	if root == nil {
		return false
	}
	if root.Left == nil && root.Right == nil {
		return targetSum-root.Val == 0
	}
	l := hasPathSumRecurision(root.Left, targetSum-root.Val)
	r := hasPathSumRecurision(root.Right, targetSum-root.Val)
	return l || r
}

func hasPathSum2(root *TreeNode, targetSum int) bool {
	if root == nil {
		return false
	}
	stack := make([]*TreeNode, 0)
	stack = append(stack, root)
	ret := 0
	// 因为要回溯，所以要记录每个节点的累加值
	for len(stack) > 0 {
		node := stack[len(stack)-1]
		stack = stack[:len(stack)-1]
		if node.Left == nil && node.Right == nil {
			ret += node.Val
			if targetSum == ret {
				return true
			}
		}
		if node.Left != nil {

		}
	}
	return false
}

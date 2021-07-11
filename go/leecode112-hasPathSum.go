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

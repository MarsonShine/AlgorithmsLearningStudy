package main

import "math"

// https://leetcode.cn/problems/balanced-binary-tree/
// 判断是否平衡二叉树：一个二叉树每个节点的左右两个节点的高度差的绝对值不能超过1
func isBalanced(root *TreeNode) bool {
	return getBinaryTreeHeight(root) != -1
}

func getBinaryTreeHeight(node *TreeNode) int {
	if node == nil {
		return 0
	}
	leftHeight := getBinaryTreeHeight(node.Left)
	if leftHeight == -1 {
		return -1
	}
	rightHeight := getBinaryTreeHeight(node.Right)
	if rightHeight == -1 {
		return -1
	}
	sub := math.Abs(float64(rightHeight) - float64(leftHeight))
	if sub > 1 {
		return -1
	} else {
		// 当前节点的高度
		return 1 + max(leftHeight, rightHeight)
	}
}

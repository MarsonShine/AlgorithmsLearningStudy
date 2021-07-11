package main

import (
	"fmt"
	"math"
)

// 验证二叉搜索树
func isValidBST(root *TreeNode) bool {
	return checkBST(root, math.MinInt64, math.MaxInt64)
}

func checkBST(node *TreeNode, minValue int, maxValue int) bool {
	if node == nil {
		return true
	}
	if node.Val <= minValue || node.Val >= maxValue {
		return false
	}
	return checkBST(node.Left, minValue, node.Val) && checkBST(node.Right, node.Val, maxValue)
}

// 用中序遍历
func checkBSTByMiddleOrder(root *TreeNode) bool {
	return inOrder(root)
}

var pre int = math.MinInt64

func inOrder(root *TreeNode) bool {
	if root == nil {
		return true
	}
	l := inOrder(root.Left)
	if root.Val <= pre {
		return false
	}
	pre = root.Val
	r := inOrder(root.Right)
	return l && r
}

func middleOrder(root *TreeNode) {
	if root == nil {
		return
	}
	middleOrder(root.Left)
	fmt.Printf("%d ", root.Val)
	middleOrder(root.Right)
}

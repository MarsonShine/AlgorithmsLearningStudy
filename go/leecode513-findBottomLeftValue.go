package main

import "math"

// https://leetcode.cn/problems/find-bottom-left-tree-value/
// 给定一个二叉树的 根节点 root，请找出该二叉树的 最底层 最左边 节点的值。
func findBottomLeftValue(root *TreeNode) int {
	ret := 0
	queue := make([]*TreeNode, 0)
	if root == nil {
		return ret
	}
	ret = root.Val
	queue = append(queue, root)
	for len(queue) > 0 {
		l := len(queue)
		for i := 0; i < l; i++ {
			node := queue[0]
			queue = queue[1:]
			if node.Left == nil && node.Right == nil {
				ret = node.Val
			}
			if node.Right != nil {
				queue = append(queue, node.Right)
			}
			if node.Left != nil {
				queue = append(queue, node.Left)
			}
		}
	}
	return ret
}

func findBottomLeftValue2(root *TreeNode) int {
	ret, maxdeep := 0, math.MinInt
	var findBottomLeftValueFunc func(node *TreeNode, deep int)

	findBottomLeftValueFunc = func(node *TreeNode, deep int) {
		if node.Left == nil && node.Right == nil {
			if deep > maxdeep {
				maxdeep = deep
				ret = node.Val
			}
			return
		}
		if node.Left != nil {
			findBottomLeftValueFunc(node.Left, deep+1)
		}
		if node.Right != nil {
			findBottomLeftValueFunc(node.Right, deep+1)
		}
	}
	if root != nil {
		findBottomLeftValueFunc(root, root.Val)
	}
	return ret
}

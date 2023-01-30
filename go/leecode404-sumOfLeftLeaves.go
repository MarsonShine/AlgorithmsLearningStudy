package main

// https://leetcode.cn/problems/sum-of-left-leaves/
// 给定二叉树的根节点 root ，返回所有左叶子之和。
func sumOfLeftLeaves(root *TreeNode) int {
	ret := 0
	queue := make([]*TreeNode, 0)
	leftQueue := make([]bool, 0)
	if root == nil {
		return ret
	}
	queue = append(queue, root)
	leftQueue = append(leftQueue, false)
	for len(queue) > 0 {
		for i := 0; i < len(queue); i++ {
			node := queue[0]
			queue = queue[1:]
			left := leftQueue[0]
			leftQueue = leftQueue[1:]
			if node.Left == nil && node.Right == nil && left {
				ret += node.Val
			}
			if node.Left != nil {
				queue = append(queue, node.Left)
				leftQueue = append(leftQueue, true)
			}
			if node.Right != nil {
				queue = append(queue, node.Right)
				leftQueue = append(leftQueue, false)
			}
		}
	}
	return ret
}

func sumOfLeftLeaves2(root *TreeNode) int {
	ret := 0
	stack := make([]*TreeNode, 0)
	if root == nil {
		return ret
	}
	stack = append(stack, root)
	for len(stack) > 0 {
		node := stack[len(stack)-1]
		stack = stack[:len(stack)-1]
		if node.Left != nil && node.Left.Left == nil && node.Left.Right == nil {
			ret += node.Left.Val
		}
		if node.Right != nil {
			stack = append(stack, node.Right)
		}
		if node.Left != nil {
			stack = append(stack, node.Left)
		}
	}
	return ret
}

package main

import "math"

// https://leetcode.cn/problems/find-largest-value-in-each-tree-row/
// 在每个树行中找最大值
func largestValues(root *TreeNode) []int {
	r := []int{}
	queue := []*TreeNode{}
	if root != nil {
		queue = append(queue, root)
	}
	for len(queue) > 0 {
		length := len(queue)
		var max int = math.MinInt32
		for i := 0; i < length; i++ {
			node := queue[0]
			queue = queue[1:]
			if node.Val > max {
				max = node.Val
			}
			if node.Left != nil {
				queue = append(queue, node.Left)
			}
			if node.Right != nil {
				queue = append(queue, node.Right)
			}
		}
		r = append(r, max)
	}
	return r
}

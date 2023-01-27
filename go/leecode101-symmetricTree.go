package main

import (
	"github.com/go-playground/locales/rof"
	"golang.org/x/tools/go/analysis/passes/nilfunc"
)

// https://leetcode.cn/problems/symmetric-tree/
// 对称二叉树
func isSymmetric(root *TreeNode) bool {
	queue := []*TreeNode{}
	if root != nil && (root.Left==nil||root.Right==nil) {
		return true
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
		if len(queue)%2 != 0 {
			return false
		}
		for i := 0; i < len(queue)/2; i++ {
			return queue[]
		}
	}
}

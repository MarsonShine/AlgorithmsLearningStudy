package main

// https://leetcode.cn/problems/insert-into-a-binary-search-tree/
// 二叉搜索树中的插入操作
func insertIntoBST(root *TreeNode, val int) *TreeNode {
	if root == nil {
		return nil
	}
	var insertIntoBSTRecursion func(root, val)
	if root.Val < val {
		if root.Left == nil && root.Right == nil {
			root.Right = &TreeNode{Val: val}
			return
		}
	}
	return root
}

package main

// https://leetcode.cn/problems/insert-into-a-binary-search-tree/
// 二叉搜索树中的插入操作
func insertIntoBST(root *TreeNode, val int) *TreeNode {
	if root == nil {
		return &TreeNode{Val: val}
	}
	var insertIntoBSTRecursion func(*TreeNode)
	insertIntoBSTRecursion = func(node *TreeNode) {
		if val < node.Val {
			if node.Left == nil {
				node.Left = &TreeNode{Val: val}
				return
			} else {
				insertIntoBSTRecursion(node.Left)
			}
		}
		if val > node.Val {
			if node.Right == nil {
				node.Right = &TreeNode{Val: val}
				return
			} else {
				insertIntoBSTRecursion(node.Right)
			}
		}
	}
	insertIntoBSTRecursion(root)
	return root
}

func insertIntoBST2(root *TreeNode, val int) *TreeNode {
	if root == nil {
		return &TreeNode{Val: val}
	}
	if val < root.Val {
		root.Left = insertIntoBST2(root.Left, val)
	} else {
		root.Right = insertIntoBST2(root.Right, val)
	}
	return root
}

func insertIntoBST3(root *TreeNode, val int) *TreeNode {
	if root == nil {
		return &TreeNode{Val: val}
	}
	node := root
	var prev *TreeNode
	for node != nil {
		prev = node
		if val < node.Val {
			node = node.Left
		} else {
			node = node.Right
		}
	}
	if prev != nil {
		if val < prev.Val {
			prev.Left = &TreeNode{Val: val}
		} else {
			prev.Right = &TreeNode{Val: val}
		}
	}
	return root
}

package main

// https://leetcode.cn/problems/trim-a-binary-search-tree/
// 裁剪二叉搜索树
func trimBST(root *TreeNode, low int, high int) *TreeNode {
	if root == nil {
		return root
	}
	if root.Val < low {
		// 左边直接去掉，右边需要裁剪
		root.Right = trimBST(root.Right, low, high)
		return root.Right
	}
	if root.Val > high {
		// 右边直接删除，左边需要裁剪
		root.Left = trimBST(root.Left, low, high)
		return root.Left
	}
	root.Left = trimBST(root.Left, low, high)
	root.Right = trimBST(root.Right, low, high)
	return root
}

func trimBST2(root *TreeNode, low int, high int) *TreeNode {
	if root == nil {
		return root
	}
	for root != nil && (root.Val < low || root.Val > high) {
		if root.Val < low {
			root = root.Right
		} else {
			root = root.Left
		}
	}
	var cur *TreeNode = root
	for cur != nil {
		if cur.Left != nil && cur.Left.Val < low {
			cur.Left = cur.Left.Right
		}
		cur = cur.Left
	}
	cur = root
	for cur != nil {
		if cur.Right != nil && cur.Right.Val > high {
			cur.Right = cur.Right.Left
		}
		cur = cur.Right
	}
	return root
}

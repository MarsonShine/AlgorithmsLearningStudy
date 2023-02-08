package main

// https://leetcode.cn/problems/search-in-a-binary-search-tree/
// 二叉搜索树中的搜索指定值节点
func searchBST(root *TreeNode, val int) *TreeNode {
	return searchBST(root, val)
}

func searchBSTRecursion(node *TreeNode, val int) *TreeNode {
	if node == nil {
		return nil
	}
	if node.Val == val {
		return node
	}
	if node.Val <= val {
		return searchBSTRecursion(node.Right, val)
	} else {
		return searchBSTRecursion(node.Left, val)
	}
}

func searchBST2(root *TreeNode, val int) *TreeNode {
	if root == nil {
		return nil
	}
	node := root
	for node != nil {
		if node.Val == val {
			return node
		}
		if node.Val <= val {
			node = node.Right
			continue
		} else {
			node = node.Left
		}
	}
	return nil
}

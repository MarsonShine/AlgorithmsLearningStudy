package main

// https://leetcode.cn/problems/convert-bst-to-greater-tree
// 把二叉搜索树转换为累加树
func convertBST(root *TreeNode) *TreeNode {
	prev := 0 // 前一个节点的值
	var buildBst func(node *TreeNode)
	buildBst = func(node *TreeNode) {
		if node == nil {
			return
		}
		// 右中左
		buildBst(node.Right)
		node.Val += prev
		prev = node.Val
		buildBst(node.Left)
	}
	buildBst(root)
	return root
}

// iterator
func convertBST2(root *TreeNode) *TreeNode {
	stack := []*TreeNode{}
	var prev int = 0
	cur := root
	for cur != nil || len(stack) > 0 {
		for cur != nil { // right
			stack = append(stack, cur)
			cur = cur.Right
		}
		// middle
		cur = stack[len(stack)-1]
		stack = stack[:len(stack)-1]
		cur.Val += prev
		prev = cur.Val
		cur = cur.Left // 左
	}
	return root
}

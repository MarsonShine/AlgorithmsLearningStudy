package main

type TreeNode struct {
	Val   int
	Left  *TreeNode
	Right *TreeNode
}

func invertTree(root *TreeNode) *TreeNode {
	tmp := root
	invertTreeRecurison(tmp)
	return tmp
}

func invertTreeRecurison(node *TreeNode) {
	if node == nil {
		return
	}
	if node.Left != nil || node.Right != nil {
		left := node.Left
		right := node.Right
		node.Right = left
		node.Left = right
	}
	invertTreeRecurison(node.Left)
	invertTreeRecurison(node.Right)
}

package main

func lowestCommonAncestor(root, p, q *TreeNode) *TreeNode {
	return commonAncestor(root, p, q)
}

func commonAncestor(node, p, q *TreeNode) *TreeNode {
	if node == nil || node == q || node == p {
		return node
	}
	left := commonAncestor(node.Left, p, q)
	right := commonAncestor(node.Right, p, q)
	if left != nil && right != nil {
		return node
	} else if left != nil && right == nil {
		return left
	} else if left == nil && right != nil {
		return right
	}
	return nil
}

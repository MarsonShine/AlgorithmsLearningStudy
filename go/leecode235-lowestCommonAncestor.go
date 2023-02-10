package main

func lowestCommonAncestorBST(root, p, q *TreeNode) *TreeNode {
	if root == nil || root == q || root == p {
		return root
	}
	if p.Val > root.Val && q.Val > root.Val {
		return lowestCommonAncestorBST(root.Right, p, q)
	}
	if p.Val < root.Val && q.Val < root.Val {
		return lowestCommonAncestorBST(root.Left, p, q)
	}
	left := lowestCommonAncestorBST(root.Left, p, q)
	right := lowestCommonAncestorBST(root.Right, p, q)
	if left != nil && right != nil {
		return root
	} else if left == nil && right != nil {
		return right
	} else if left != nil && right == nil {
		return left
	}
	return nil
}

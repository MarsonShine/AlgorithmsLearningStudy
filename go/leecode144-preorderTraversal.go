package main

func preorderTraversal(root *TreeNode) []int {
	return preorder(root)
}

func preorder(root *TreeNode) (res []int) {
	if root == nil {
		return
	}
	// 中
	res = append(res, root.Val)
	// 左
	res = append(res, preorder(root.Left)...)
	// 右
	res = append(res, preorder(root.Right)...)
	return res
}

/*
前序遍历，中左右
*/

package main

func inorderTraversal(root *TreeNode) []int {
	return inorder(root)
}

func inorder(root *TreeNode) (res []int) {
	if root == nil {
		return
	}
	// 左
	res = append(res, inorder(root.Left)...)
	// 中
	res = append(res, root.Val)
	// 右
	res = append(res, inorder(root.Right)...)

	return res
}

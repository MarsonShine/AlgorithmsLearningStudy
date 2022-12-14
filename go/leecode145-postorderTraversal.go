package main

func postorderTraversal(root *TreeNode) []int {
	return postorder(root)
}

func postorder(root *TreeNode) (res []int) {
	if root == nil {
		return
	}
	// 左
	res = append(res, postorder(root.Left)...)
	// 右
	res = append(res, postorder(root.Right)...)
	// 中
	res = append(res, root.Val)
	return res
}

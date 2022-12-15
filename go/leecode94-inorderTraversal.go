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

/* 迭代 */
func inorderTraversal2(root *TreeNode) (res []int) {
	stack := []*TreeNode{}
	for root != nil || len(stack) > 0 {
		for root != nil {
			stack = append(stack, root)
			root = root.Left
		}
		root := stack[len(stack)-1]
		stack = stack[:len(stack)-1]
		res = append(res, root.Val)
		root = root.Right
	}
	return
}

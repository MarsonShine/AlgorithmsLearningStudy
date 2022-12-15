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

/* 迭代 */
func postorderTraversal2(root *TreeNode) (res []int) {
	stack := []*TreeNode{}
	var node *TreeNode
	for root != nil || len(stack) > 0 {
		for root != nil {
			stack = append(stack, root)
			root = root.Left
		}
		root = stack[len(stack)-1]
		stack = stack[:len(stack)-1]
		if root.Right == nil || node == root.Right {
			res = append(res, root.Val)
			node = root
			root = nil
		} else {
			stack = append(stack, root)
			root = root.Right
		}
	}
	return
}

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

/* 迭代 */
func preorderTraversal2(root *TreeNode) []int {
	res := []int{}
	stack := []*TreeNode{}
	for root != nil || len(stack) > 0 {
		for root != nil {
			res = append(res, root.Val)
			stack = append(stack, root)
			root = root.Left
		}
		root = stack[len(stack)-1].Right
		stack = stack[:len(stack)-1]
	}
	return res
}

/*
前序遍历，中左右
*/

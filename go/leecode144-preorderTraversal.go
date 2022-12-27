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
1 创建一个空栈 nodeStack，将 root push 进去
2 循环如下执行体，如果 nodeStack 不为空的
	2.1 弹出 nodeStack 元素项并打印该项
	2.2 弹出项的右孩子节点推送至 nodeStack
	2.3 弹出项的左孩子节点推送至 nodeStack
*/

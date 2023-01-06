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
		node.Left, node.Right = node.Right, node.Left
	}
	invertTreeRecurison(node.Left)
	invertTreeRecurison(node.Right)
}

// 迭代，深度遍历，前序遍历
func invertTree2(root *TreeNode) *TreeNode {
	stack := []*TreeNode{}
	node := root
	for node != nil || len(stack) > 0 {
		for node != nil {
			node.Left, node.Right = node.Right, node.Left
			stack = append(stack, node)
			node = node.Left
		}
		node = stack[len(stack)-1]
		stack = stack[:len(stack)-1]
		node = node.Right
	}
	return root
}

// 广度优先算法
// 层序遍历
func invertTree3(root *TreeNode) *TreeNode {
	queue := []*TreeNode{}
	if root != nil {
		queue = append(queue, root)
	}
	for len(queue) > 0 {
		length := len(queue)
		for i := 0; i < length; i++ {
			node := queue[0]
			queue = queue[1:]
			if node.Left != nil {
				queue = append(queue, node.Left)
			}
			if node.Right != nil {
				queue = append(queue, node.Right)
			}
			node.Left, node.Right = node.Right, node.Left
		}
	}
	return root
}

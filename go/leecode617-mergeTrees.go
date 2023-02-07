package main

// https://leetcode.cn/problems/merge-two-binary-trees/
// 合并二叉树，相同节点的值相加
func mergeTrees(root1 *TreeNode, root2 *TreeNode) *TreeNode {
	return mergeBinaryTree(root1, root2)
}

func mergeBinaryTree(root1 *TreeNode, root2 *TreeNode) *TreeNode {
	var root *TreeNode
	if root1 == nil && root2 == nil {
		return nil
	}
	if root1 != nil && root2 != nil {
		root = &TreeNode{
			Val: root1.Val + root2.Val,
		}
		root.Left = mergeBinaryTree(root1.Left, root2.Left)
		root.Right = mergeBinaryTree(root1.Right, root2.Right)
		return root
	}
	if root1 != nil {
		return root1
	} else {
		return root2
	}
}

func mergeTrees2(root1 *TreeNode, root2 *TreeNode) *TreeNode {
	if root1 == nil && root2 == nil {
		return nil
	}
	if root1 != nil && root2 != nil {
		queue := []*TreeNode{}
		queue = append(queue, root1, root2)
		for len(queue) > 0 {
			node1 := queue[0]
			node2 := queue[1]
			queue = queue[2:]
			node1.Val += node2.Val
			// left
			if node1.Left != nil && node2.Left != nil {
				queue = append(queue, node1.Left, node2.Left)
			}
			if node1.Left == nil && node2.Left != nil {
				node1.Left = node2.Left
			}
			// right
			if node1.Right != nil && node2.Right != nil {
				queue = append(queue, node1.Right, node2.Right)
			}
			if node1.Right == nil && node2.Right != nil {
				node1.Right = node2.Right
			}
		}
		return root1
	}
	if root1 == nil {
		return root2
	}
	return root1
}

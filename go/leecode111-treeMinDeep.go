package main

// https://leetcode.cn/problems/minimum-depth-of-binary-tree/
// 给定一个二叉树，找出其最小深度
// 层序遍历
func minDepth(root *TreeNode) int {
	queue := []*TreeNode{}
	minDepth := 0
	if root != nil {
		queue = append(queue, root)
	}
	for len(queue) > 0 {
		length := len(queue)
		minDepth += 1
		for i := 0; i < length; i++ {
			node := queue[0]
			queue = queue[1:]
			if node.Left == nil && node.Right == nil {
				return minDepth
			} else {
				if node.Left != nil {
					queue = append(queue, node.Left)
				}
				if node.Right != nil {
					queue = append(queue, node.Right)
				}
			}
		}
	}
	return minDepth
}

// 递归
func minDepth2(root *TreeNode) int {
	if root == nil {
		return 0
	}
	if root.Left == nil && root.Right == nil {
		return 1
	}
	ml := minDepth2(root.Left)
	mr := minDepth2(root.Right)
	if root.Left == nil || root.Right == nil {
		return ml + mr + 1
	}
	return min(ml, mr) + 1
}

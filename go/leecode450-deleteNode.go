package main

// https://leetcode.cn/problems/delete-node-in-a-bst/
// 删除二叉搜索树中的节点

func deleteNode(root *TreeNode, key int) *TreeNode {
	if root == nil {
		return root
	}
	if key < root.Val {
		root.Left = deleteNode(root.Left, key)
		return root
	}
	if key > root.Val {
		root.Right = deleteNode(root.Right, key)
		return root
	}
	if key == root.Val {
		if root.Left == nil {
			return root.Right
		}
		if root.Right == nil {
			return root.Left
		}
		if root.Left != nil && root.Right != nil {
			// 找右边最小的节点
			rightMinNode := root.Right
			for rightMinNode.Left != nil {
				rightMinNode = rightMinNode.Left
			}
			root.Val = rightMinNode.Val
			// 删除最小的节点
			root.Right = deleteMinNode(root.Right)
			return root
		}
	}
	return root
}

func deleteMinNode(node *TreeNode) *TreeNode {
	if node.Left == nil {
		right := node.Right
		node.Right = nil
		return right
	}
	node.Left = deleteMinNode(node.Left)
	return node
}

func deleteNode2(root *TreeNode, key int) *TreeNode {
	if root == nil {
		return root
	}
	cur := root
	var prev *TreeNode
	for cur != nil {
		if key == cur.Val {
			break
		}
		prev = cur
		if key < cur.Val {
			cur = cur.Left
		} else {
			cur = cur.Right
		}
	}
	if prev == nil { // 删除的是跟节点
		return deleteNodeInterator(cur)
	}
	// 匹配到父节点的左边节点相等，则删除左节点
	if prev.Left != nil && prev.Left.Val == key {
		prev.Left = deleteNodeInterator(cur)
	}
	// 删除右边节点
	if prev.Right != nil && prev.Right.Val == key {
		prev.Right = deleteNodeInterator(cur)
	}
	return root
}

func deleteNodeInterator(node *TreeNode) *TreeNode {
	if node == nil {
		return node
	}
	if node.Right == nil {
		return node.Left
	}
	// 右边最小节点
	cur := node.Right
	for cur.Left != nil {
		cur = cur.Left
	}
	cur.Left = node.Left
	return node.Right
}

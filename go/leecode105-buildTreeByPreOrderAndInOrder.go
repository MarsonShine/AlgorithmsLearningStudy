package main

// https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/
// 根据先序遍历、中序遍历的结果求导出二叉树
func buildTreeByPreOrderAndInOrder(preorder []int, inorder []int) *TreeNode {
	m := make(map[int]int)
	for i, v := range inorder {
		m[v] = i
	}

	var build func(leftIndex, rightIndex int) *TreeNode
	build = func(leftIndex, rightIndex int) *TreeNode {
		if leftIndex > rightIndex {
			return nil
		}
		rootValue := preorder[0]
		preorder = preorder[1:]
		root := &TreeNode{Val: rootValue}
		rootIndex := m[rootValue]
		root.Left = build(leftIndex, rootIndex-1)
		root.Right = build(rootIndex+1, rightIndex)
		return root
	}

	return build(0, len(inorder)-1)
}

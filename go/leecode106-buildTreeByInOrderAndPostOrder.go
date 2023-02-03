package main

// https://leetcode.cn/problems/construct-binary-tree-from-inorder-and-postorder-traversal/solutions/
// 从中序与后序遍历序列构造二叉树
func buildTreeByInOrderAndPostOrder(inorder []int, postorder []int) *TreeNode {
	m := make(map[int]int)
	for i, v := range inorder {
		m[v] = i
	}
	return buildTree(inorder, &postorder, m, 0, len(inorder)-1)
}

func buildTree(inorder []int, postorder *[]int, m map[int]int, l, r int) *TreeNode {
	if l > r {
		return nil
	}
	ps := *postorder
	nodeValue := ps[len(ps)-1] // 后序遍历的最后一个节点就是根节点
	aps := ps[:len(ps)-1]
	postorder = &aps
	nodeIndex := m[nodeValue]
	root := &TreeNode{Val: nodeValue}
	// 构建左右节点
	// 注意，必须是先构建右节点，再构建左节点。这样就不会存在构建树的顺序问题
	// 因为在遍历后续数组时，是从最后的元素开始的，也就是，也就是先存储的右节点，再是左节点
	root.Right = buildTree(inorder, postorder, m, nodeIndex+1, r)
	root.Left = buildTree(inorder, postorder, m, l, nodeIndex-1)
	return root
}

func buildTree2(inorder []int, postorder []int) *TreeNode {
	m := make(map[int]int)
	for i, v := range inorder {
		m[v] = i
	}
	var build func(leftIndex, rightIndex int) *TreeNode
	build = func(leftIndex, rightIndex int) *TreeNode {
		if leftIndex > rightIndex {
			return nil
		}
		nodeValue := postorder[len(postorder)-1] // 后序遍历的最后一个节点就是根节点
		postorder = postorder[:len(postorder)-1]
		nodeIndex := m[nodeValue]
		root := &TreeNode{Val: nodeValue}
		root.Right = build(nodeIndex+1, rightIndex)
		root.Left = build(leftIndex, nodeIndex-1)
		return root
	}

	return build(0, len(inorder)-1)
}

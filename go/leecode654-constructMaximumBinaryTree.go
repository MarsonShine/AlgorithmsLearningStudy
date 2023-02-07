package main

// https://leetcode.cn/problems/maximum-binary-tree/
// 最大二叉树
func constructMaximumBinaryTree(nums []int) *TreeNode {
	m := make(map[int]int)
	for i, v := range nums {
		m[v] = i
	}
	var buildMaxinumBinaryTree func(l, r int) *TreeNode
	buildMaxinumBinaryTree = func(l, r int) *TreeNode {
		if l > r {
			return nil
		}
		max := getmax(nums[l : r+1])
		root := &TreeNode{Val: max}
		rootIndex := m[max]
		root.Left = buildMaxinumBinaryTree(l, rootIndex-1)
		root.Right = buildMaxinumBinaryTree(rootIndex+1, r)
		return root
	}
	return buildMaxinumBinaryTree(0, len(nums)-1)
}

func getmax(nums []int) int {
	max := nums[0]
	for i := 1; i < len(nums); i++ {
		if max < nums[i] {
			max = nums[i]
		}
	}
	return max
}

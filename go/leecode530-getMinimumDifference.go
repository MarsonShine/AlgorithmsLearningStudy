package main

import "math"

// https://leetcode.cn/problems/minimum-absolute-difference-in-bst
// 二叉搜索树的最小绝对差
func getMinimumDifference_error(root *TreeNode) int {
	// 读题错误，以为是相邻的两个节点的最小值
	minValue := math.MaxInt
	var getMinimumDifference func(node *TreeNode)
	getMinimumDifference = func(node *TreeNode) {
		if node.Left != nil {
			minValue = min(int(math.Abs(float64(node.Val)-float64(node.Left.Val))), minValue)
			getMinimumDifference(node.Left)
		}
		if node.Right != nil {
			minValue = min(int(math.Abs(float64(node.Val)-float64(node.Right.Val))), minValue)
			getMinimumDifference(node.Right)
		}
	}
	getMinimumDifference(root)
	return minValue
}

func getMinimumDifference(root *TreeNode) int {
	// 构建一个有序数组，这样最小值就一定是相邻的两个数之差
	arr := []int{}
	minValue := math.MaxInt
	var buildOrderedArray func(node *TreeNode)
	buildOrderedArray = func(node *TreeNode) {
		if node == nil {
			return
		}
		buildOrderedArray(node.Left)
		arr = append(arr, node.Val)
		buildOrderedArray(node.Right)
	}
	buildOrderedArray(root)
	for i := 1; i < len(arr); i++ {
		minValue = min(minValue, arr[i]-arr[i-1])
	}
	return minValue
}

func getMinimumDifference2(root *TreeNode) int {
	minValue := math.MaxInt
	var pre *TreeNode
	var getMinimumDifferenceRecursion func(node *TreeNode)
	getMinimumDifferenceRecursion = func(node *TreeNode) {
		if node == nil {
			return
		}
		getMinimumDifferenceRecursion(node.Left)
		if pre != nil {
			minValue = min(minValue, node.Val-pre.Val)
		}
		pre = node
		getMinimumDifferenceRecursion(node.Right)
	}

	getMinimumDifferenceRecursion(root)
	return minValue
}

func getMinimumDifference3(root *TreeNode) int {
	minValue := math.MaxInt
	var pre *TreeNode
	var cur *TreeNode = root
	stack := []*TreeNode{}
	for cur != nil || len(stack) > 0 {
		if cur != nil {
			stack = append(stack, cur)
			cur = cur.Left
		} else {
			cur = stack[len(stack)-1]
			stack = stack[:len(stack)-1]
			if pre != nil {
				minValue = min(minValue, cur.Val-pre.Val)
			}
			pre = cur
			cur = cur.Right
		}
	}
	return minValue
}

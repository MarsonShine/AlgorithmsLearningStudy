package main

// https://leetcode.cn/problems/convert-sorted-array-to-binary-search-tree/
// 将有序数组转换为二叉搜索树

func sortedArrayToBST(nums []int) *TreeNode {
	count := len(nums)
	if count == 0 {
		return nil
	}
	if count == 1 {
		return &TreeNode{Val: nums[0]}
	}
	rootIndex := count / 2
	rootValue := nums[rootIndex]
	root := &TreeNode{Val: rootValue}
	root.Left = sortedArrayToBST(nums[:rootIndex])
	root.Right = sortedArrayToBST(nums[rootIndex+1:])
	return root
}

func sortedArrayToBST2(nums []int) *TreeNode {
	if len(nums) == 0 {
		return nil
	}
	root := &TreeNode{Val: 0}
	nodeQueue := []*TreeNode{}
	leftIndexQueue := []int{0}
	rightIndexQueue := []int{len(nums) - 1}
	nodeQueue = append(nodeQueue, root)
	for len(nodeQueue) > 0 {
		cur := nodeQueue[0]
		nodeQueue = nodeQueue[1:]
		leftIndex := leftIndexQueue[0]
		rightIndex := rightIndexQueue[0]
		leftIndexQueue = leftIndexQueue[1:]
		rightIndexQueue = rightIndexQueue[1:]
		midIndex := leftIndex + ((rightIndex - leftIndex) / 2)
		cur.Val = nums[midIndex]
		if leftIndex <= midIndex-1 {
			cur.Left = &TreeNode{Val: 0}
			nodeQueue = append(nodeQueue, cur.Left)
			leftIndexQueue = append(leftIndexQueue, leftIndex)
			rightIndexQueue = append(rightIndexQueue, midIndex-1)
		}
		if rightIndex >= midIndex+1 {
			cur.Right = &TreeNode{Val: 0}
			nodeQueue = append(nodeQueue, cur.Right)
			leftIndexQueue = append(leftIndexQueue, midIndex+1)
			rightIndexQueue = append(rightIndexQueue, rightIndex)
		}
	}
	return root
}

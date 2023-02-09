package main

import "math"

// https://leetcode.cn/problems/find-mode-in-binary-search-tree
// 在二叉搜索树中找出并返回 BST 中的所有 众数（即，出现频率最高的元素）
func findMode(root *TreeNode) []int {
	m := make(map[int]int)
	max := math.MinInt
	result := []int{}
	var findModRecursion func(root *TreeNode)
	findModRecursion = func(node *TreeNode) {
		if node == nil {
			return
		}
		m[node.Val]++
		count := m[node.Val]
		if count > max {
			max = count
		}
		findModRecursion(node.Left)
		findModRecursion(node.Right)
	}
	findModRecursion(root)
	for k, v := range m {
		if v == max {
			result = append(result, k)
		}
	}
	return result
}

func findMode2(root *TreeNode) []int {
	max := 0
	count := 0
	var prev *TreeNode
	result := []int{}
	var findModRecursion func(root *TreeNode)
	findModRecursion = func(node *TreeNode) {
		if node == nil {
			return
		}
		findModRecursion(node.Left)
		if prev == nil { // 最后一个左节点的叶子节点
			count = 1
		} else if prev.Val == node.Val { // 连续相等 +1
			count += 1
		} else {
			count = 1 // 不相同，则重新统计
		}
		prev = node
		if count == max {
			result = append(result, node.Val)
		}
		if count > max {
			max = count
			result = []int{}
			result = append(result, node.Val)
		}
		findModRecursion(node.Right)
	}
	findModRecursion(root)
	return result
}

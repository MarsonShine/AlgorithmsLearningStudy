package main

// https://leetcode.cn/problems/average-of-levels-in-binary-tree/

func averageOfLevels(root *TreeNode) []float64 {
	queue := []*TreeNode{}
	if root != nil {
		queue = append(queue, root)
	}
	r := []float64{}
	for len(queue) > 0 {
		length := len(queue)
		vals := []int{}
		for i := 0; i < length; i++ {
			node := queue[0]
			queue = queue[1:]
			if node.Left != nil {
				queue = append(queue, node.Left)
			}
			if node.Right != nil {
				queue = append(queue, node.Right)
			}
			vals = append(vals, node.Val)
		}
		// 计算平均值
		r = append(r, avg(vals))
	}
	return r
}

func averageOfLevels2(root *TreeNode) []float64 {
	queue := []*TreeNode{}
	if root != nil {
		queue = append(queue, root)
	}
	r := []float64{}
	for len(queue) > 0 {
		length := len(queue)
		var sum float64
		for i := 0; i < length; i++ {
			node := queue[0]
			queue = queue[1:]
			if node.Left != nil {
				queue = append(queue, node.Left)
			}
			if node.Right != nil {
				queue = append(queue, node.Right)
			}
			sum += float64(node.Val)
		}
		// 计算平均值
		r = append(r, sum/float64(length))
	}
	return r
}

type Number interface {
	int | int32 | int64 | float32 | float64
}

func avg[T Number](source []T) float64 {
	var sum float64
	l := len(source)
	for i := 0; i < l; i++ {
		sum += float64(source[i])
	}
	return sum / float64(l)
}

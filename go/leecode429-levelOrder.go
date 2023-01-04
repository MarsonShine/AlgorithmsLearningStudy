package main

// https://leetcode.cn/problems/n-ary-tree-level-order-traversal/
// N 叉树的层序遍历
func nlevelOrder(root *Node) [][]int {
	r := [][]int{}
	queue := []*Node{}
	if root != nil {
		queue = append(queue, root)
	}
	for len(queue) > 0 {
		length := len(queue)
		nodeValues := make([]int, 0, length)
		for i := 0; i < length; i++ {
			node := queue[0]
			queue = queue[1:]
			nodeValues = append(nodeValues, node.Val)
			if len(node.Children) > 0 {
				for _, cn := range node.Children {
					queue = append(queue, cn)
				}
			}
		}
		r = append(r, nodeValues)
	}
	return r
}

type Node struct {
	Val      int
	Children []*Node
}

package main

// https://leetcode.cn/problems/populating-next-right-pointers-in-each-node/
// 填充每个节点的下一个右侧节点指针
func connect(root *PerfectNode) *PerfectNode {
	queue := []*PerfectNode{}
	if root != nil {
		queue = append(queue, root)
	}
	for len(queue) > 0 {
		length := len(queue)
		nodes := []*PerfectNode{}
		for i := 0; i < length; i++ {
			node := queue[0]
			queue = queue[1:]
			nodes = append(nodes, node)
			if node.Left != nil {
				queue = append(queue, node.Left)
			}
			if node.Right != nil {
				queue = append(queue, node.Right)
			}
		}
		if len(nodes) == 1 {
			continue
		}
		for i := 1; i < len(nodes); i++ {
			nodes[i-1].Next = nodes[i]
		}
	}
	return root
}

type PerfectNode struct {
	Val   int
	Left  *PerfectNode
	Right *PerfectNode
	Next  *PerfectNode
}

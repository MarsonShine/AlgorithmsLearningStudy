package main

import "strconv"

// https://leetcode.cn/problems/binary-tree-paths/
// 给你一个二叉树的根节点 root ，按 任意顺序 ，返回所有从根节点到叶子节点的路径。
var res []string

func binaryTreePaths(root *TreeNode) []string {
	res = make([]string, 0)
	dfs_binaryTreePaths(root, "")
	return res
}

func dfs_binaryTreePaths(node *TreeNode, acc string) {
	if node == nil {
		return
	}
	if node.Left == nil && node.Right == nil {
		res = append(res, acc+strconv.Itoa(node.Val))
		return
	}
	acc += strconv.Itoa(node.Val) + "->"
	if node.Left != nil {
		dfs_binaryTreePaths(node.Left, acc)
	}
	if node.Right != nil {
		dfs_binaryTreePaths(node.Right, acc)
	}
}

func binaryTreePaths2(root *TreeNode) []string {
	results := []string{}
	if root == nil {
		return results
	}
	nodes := make([]*TreeNode, 0)
	paths := make([]string, 0) // 存储当前的节点
	nodes = append(nodes, root)
	paths = append(paths, "")
	for len(nodes) > 0 {
		l := len(nodes)
		node := nodes[l-1]
		path := paths[l-1]
		nodes = nodes[:l-1]
		paths = paths[:l-1]
		if node.Left == nil && node.Right == nil {
			results = append(results, path+strconv.Itoa(node.Val))
			continue
		}
		if node.Left != nil {
			nodes = append(nodes, node.Left)
			paths = append(paths, path+strconv.Itoa(node.Val)+"->")
		}
		if node.Right != nil {
			nodes = append(nodes, node.Right)
			paths = append(paths, path+strconv.Itoa(node.Val)+"->")
		}
	}
	return results
}

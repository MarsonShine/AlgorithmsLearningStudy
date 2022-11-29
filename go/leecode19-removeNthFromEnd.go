package main

import "math"

func getLength(head *ListNode) int {
	// 计算长度
	size := 0
	for ; head != nil; head = head.Next {
		size++
	}
	return size
}

// https://leetcode.cn/problems/remove-nth-node-from-end-of-list/
func removeNthFromEnd(head *ListNode, n int) *ListNode {
	size := getLength(head)
	newNode := &ListNode{math.MaxInt, head}
	cur := newNode
	for i := 0; i < size-n; i++ {
		cur = cur.Next
	}
	cur.Next = cur.Next.Next
	return newNode.Next
}

// 利用快慢双指针，只用遍历一次
func removeNthFromEnd2(head *ListNode, n int) *ListNode {
	newNode := &ListNode{math.MaxInt, head}
	cur := newNode
	slow, fast := cur, cur
	for i := 0; i < n+1; i++ {
		fast = cur.Next
		cur = cur.Next
	}
	for fast != nil {
		slow = slow.Next
		fast = fast.Next
	}
	slow.Next = slow.Next.Next
	return newNode.Next
}

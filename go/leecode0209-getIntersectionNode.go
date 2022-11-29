package main

import "math"

// 链表相交 https://leetcode.cn/problems/intersection-of-two-linked-lists-lcci/
func getIntersectionNode(headA, headB *ListNode) *ListNode {
	// 链表相交 说明相交点以后的节点都是相同的
	// 所以我们只需要把两个节点的元素按尾对齐，从元素少的链表的头节点开始比较对应位置的元素较多的链表节点判断是否相等
	sizeA := getLength(headA)
	sizeB := getLength(headB)
	sub := math.Abs(float64(sizeA - sizeB))
	var slow, fast *ListNode
	if sizeA >= sizeB {
		slow = headB
		fast = headA
	} else {
		slow = headA
		fast = headB
	}
	// 到达同一相对位置
	for i := 0; i < int(sub); i++ {
		fast = fast.Next
	}
	for slow != fast {
		slow = slow.Next
		fast = fast.Next
	}
	return slow
}

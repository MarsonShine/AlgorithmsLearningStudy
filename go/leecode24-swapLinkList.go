package main

import "math"

func swapPairs(head *ListNode) *ListNode {
	var newNode *ListNode = &ListNode{
		Val:  math.MaxInt,
		Next: nil,
	}
	newNode.Next = head
	cur := newNode
	for cur.Next != nil && cur.Next.Next != nil {
		temp := cur.Next
		temp2 := cur.Next.Next.Next
		cur.Next = cur.Next.Next
		cur.Next.Next = temp
		cur.Next.Next.Next = temp2

		cur = cur.Next.Next
	}
	return newNode.Next
}

/*
[1,2,3,4]
[2,1,4,3]
*/

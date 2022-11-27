package main

type ListNode struct {
	Val  int
	Next *ListNode
}

func removeElements(head *ListNode, val int) *ListNode {
	if head == nil {
		return nil
	}
	// 如果是头节点
	for head != nil && head.Val == val {
		head = head.Next
	}
	// 如果是非头节点
	current := head
	for current != nil && current.Next != nil {
		if current.Next.Val == val {
			current.Next = current.Next.Next
		} else {
			current = current.Next
		}
	}
	return head
}

/*
[1,2,6,3,4,5,6] 6

*/

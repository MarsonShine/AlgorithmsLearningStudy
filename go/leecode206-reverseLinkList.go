package main

// https://leetcode.cn/problems/reverse-linked-list/
// 反转链表
func reverseList(head *ListNode) *ListNode {
	var newListNode *ListNode
	cur := head
	if cur != nil {
		//剩余部分
		for cur != nil {
			node := ListNode{
				Val:  cur.Val,
				Next: nil,
			}
			node.Next = newListNode
			newListNode = &node
			cur = cur.Next
		}
	}
	return newListNode
}

/*
[1,2,3,4,5]
1
2,1
3,2,1
*/

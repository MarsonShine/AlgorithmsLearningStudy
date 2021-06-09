package main

type MyCircularDeque struct {
	items    []int
	capacity int
	size     int
	front    int
	rear     int
}

func Constructor(k int) MyCircularDeque {
	return MyCircularDeque{
		capacity: k,
		items:    make([]int, k),
		front:    1,
	}
}

// 数组插入性能不如链表,可以用首尾指针优化
func (q *MyCircularDeque) InsertFront(val int) bool {
	if q.IsFull() {
		return false
	}
	q.size++
	q.front--
	if q.front == -1 {
		q.front = q.capacity - 1
	}
	q.items[q.front] = val
	return true
}

func (q *MyCircularDeque) InsertLast(value int) bool {
	if q.IsFull() {
		return false
	}
	q.size++
	q.rear++
	if q.rear == q.capacity {
		q.rear = 0
	}
	q.items[q.rear] = value
	return true
}

func (q *MyCircularDeque) DeleteFront() bool {
	if q.IsEmpty() {
		return false
	}
	q.front++
	q.size--
	if q.front == q.capacity {
		q.front = 0
	}
	return true
}

func (q *MyCircularDeque) DeleteLast() bool {
	if q.IsEmpty() {
		return false
	}
	q.size--
	q.rear--
	if q.rear == -1 {
		q.rear = q.capacity - 1
	}
	return true
}

func (q *MyCircularDeque) GetFront() int {
	if q.IsEmpty() {
		return -1
	}
	return q.items[q.front]
}

func (q *MyCircularDeque) GetRear() int {
	if q.IsEmpty() {
		return -1
	}
	return q.items[q.rear]
}

func (q *MyCircularDeque) IsFull() bool {
	return q.capacity == q.size
}

func (q *MyCircularDeque) IsEmpty() bool {
	return q.size == 0
}

func (q *MyCircularDeque) Length() int {
	return q.size
}

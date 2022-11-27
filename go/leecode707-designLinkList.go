package main

type MyInt interface {
	int | int8 | int16 | int32
}

type MyLinkedNode[T any] struct {
	Val  T
	Next *MyLinkedNode[T]
}

func NewMyLinkedNode[T any](val T) *MyLinkedNode[T] {
	return &MyLinkedNode[T]{
		Val:  val,
		Next: nil,
	}
}

type MyLinkedList[T any] struct {
	length int
	Head   *MyLinkedNode[T]
}

func NewMyLinkedList[T any]() MyLinkedList[T] {
	return MyLinkedList[T]{
		Head:   nil,
		length: 0,
	}
}

func (this *MyLinkedList[T]) Get(index int) T {
	var t T
	if this.length == 0 || index < 0 {
		return t
	}
	head := this.Head
	i := 0
	for head != nil {
		if i == index {
			return head.Val
		}
		head = head.Next
		i++
	}
	return t
}

func (this *MyLinkedList[T]) AddAtHead(val T) {
	newNode := NewMyLinkedNode(val)
	temp := this.Head
	newNode.Next = temp
	this.Head = newNode
	this.length++
}

func (this *MyLinkedList[T]) AddAtTail(val T) {
	if this.Head == nil {
		this.AddAtHead(val)
		return
	}
	cur := this.Head
	for cur.Next != nil {
		cur = cur.Next
	}
	cur.Next = NewMyLinkedNode(val)
	this.length++
}

func (this *MyLinkedList[T]) AddAtIndex(index int, val T) {
	size := this.length
	if index < 0 || index > size {
		return
	}
	if index == 0 {
		this.AddAtHead(val)
		return
	}
	if index == size {
		this.AddAtTail(val)
		return
	}
	cur := this.Head
	for i := 0; i < index-1; i++ {
		cur = cur.Next
	}
	newNode := NewMyLinkedNode(val)
	newNode.Next = cur.Next
	cur.Next = newNode
	this.length++
}

func (this *MyLinkedList[T]) DeleteAtIndex(index int) {
	size := this.length
	if index < 0 || index > size-1 {
		return
	}
	if index == 0 {
		// 删除头部
		this.Head = this.Head.Next
		this.length--
		return
	}

	cur := this.Head
	for i := 0; i < index-1; i++ {
		cur = cur.Next
	}
	cur.Next = cur.Next.Next
	this.length--
}

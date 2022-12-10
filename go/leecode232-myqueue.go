package main

// 用栈实现队列
// https://leetcode.cn/problems/implement-queue-using-stacks/
type MyQueue struct {
	items  []int
	head   int // 头指针
	tail   int // 尾指针
	length int // 长度
}

func NewMyQueueByStack() MyQueue {
	return MyQueue{
		items:  make([]int, 0),
		head:   0,
		tail:   0,
		length: 0,
	}
}

func (this *MyQueue) Push(x int) {
	this.items = append(this.items, x)
	this.tail++
	this.length++
}

func (this *MyQueue) Pop() int {
	if this.Empty() {
		this.head = 0
		this.tail = 0
		return 0
	}
	tmp := this.items[this.head]
	this.head++
	this.length--
	return tmp
}

func (this *MyQueue) Peek() int {
	if this.Empty() {
		this.head = 0
		this.tail = 0
		return 0
	}
	return this.items[this.head]
}

func (this *MyQueue) Empty() bool {
	return this.length == 0
}

/**
 * Your MyQueue object will be instantiated and called as such:
 * obj := Constructor();
 * obj.Push(x);
 * param_2 := obj.Pop();
 * param_3 := obj.Peek();
 * param_4 := obj.Empty();
 */

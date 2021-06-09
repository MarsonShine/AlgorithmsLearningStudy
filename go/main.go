package main

import "fmt"

func main() {
	fmt.Println(isValid("()"))
	fmt.Println(isValid("()[]"))
	fmt.Println(isValid("([])"))
	fmt.Println(isValid("()[[{}]]()"))
	fmt.Println(isValid("(]"))

	fmt.Println(longestValidParentheses("(()"))
	fmt.Println(longestValidParentheses("(())"))
	fmt.Println(longestValidParentheses("(()())"))
	fmt.Println(longestValidParentheses("(()())((()))"))

	fmt.Println(longestValidParenthesesByDynamicProgram("())((())"))

	// fmt.Println(evalRPN([]string{"2", "1", "+", "3", "*"}))
	fmt.Println(evalRPN([]string{"4", "13", "5", "/", "+"}))
	fmt.Println(evalRPN([]string{"10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+"}))

	queue := Constructor(3)
	retArr := make([]interface{}, 10)
	retArr[1] = queue.InsertLast(1)
	retArr[2] = queue.InsertLast(2)
	retArr[3] = queue.InsertFront(3)
	retArr[4] = queue.InsertFront(4)
	retArr[5] = queue.GetRear()
	retArr[6] = queue.IsFull()
	retArr[7] = queue.DeleteLast()
	retArr[8] = queue.InsertFront(4)
	retArr[9] = queue.GetFront()
	fmt.Println(retArr...)
}

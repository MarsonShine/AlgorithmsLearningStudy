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
}

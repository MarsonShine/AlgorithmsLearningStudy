package main

import "strconv"

func evalRPN(tokens []string) int {
	operates := map[string]bool{
		"+": true,
		"-": true,
		"/": true,
		"*": true,
	}

	acc := []int{}
	for _, v := range tokens {
		if operates[v] {
			if len(acc) > 1 {
				r := calculate(acc[len(acc)-2], acc[len(acc)-1], v)
				acc = acc[:len(acc)-2]
				acc = append(acc, r)
			}
		} else {
			n, _ := strconv.Atoi(v)
			acc = append(acc, n)
		}
	}
	return acc[len(acc)-1]
}

func calculate(a, b int, op string) int {
	switch op {
	case "+":
		return a + b
	case "-":
		return a - b
	case "*":
		return a * b
	case "/":
		return a / b
	default:
		return 0
	}
}

package main

import (
	"fmt"
	"main/dp"
	"net/url"
)

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

	// fmt.Println(maxSlidingWindow([]int{1, 3, -1, -3, 5, 3, 6, 7}, 3))
	fmt.Println(maxSlidingWindow2([]int{1, 3, -1, -3, 5, 3, 6, 7}, 3))
	fmt.Println(maxSlidingWindow2([]int{1, 3, -1, -3, 5, 3, 6, 7}, 2))
	fmt.Println(maxSlidingWindow2([]int{1, 3, -1, -3, 5, 3, 6, 7}, 4))
	fmt.Println(maxSlidingWindow2([]int{7, 2, 4}, 2))
	fmt.Println(maxSlidingWindow2([]int{7, 2, 4}, 3))
	fmt.Println(maxSlidingWindow3([]int{1, 3, -1, -3, 5, 3, 6, 7}, 2))

	fmt.Println(climbStairs(5))
	fmt.Println(climbStairs2(5))
	// fmt.Println(climbStairs(44))
	// fmt.Println(climbStairs2(44))

	fmt.Println(mySqrt(0))
	fmt.Println(mySqrt(1))
	fmt.Println(mySqrt(2))
	fmt.Println(mySqrt(3))
	// fmt.Println(mySqrt(4))
	// fmt.Println(mySqrt(8))
	// fmt.Println(mySqrt(10))
	// fmt.Println(mySqrt(17))

	fmt.Println(reverseWords("hello world marson shine"))
	fmt.Println(reverseWords("a good   example"))

	// fmt.Println(MyAtoi("  -0012a42"))
	// fmt.Println(MyAtoi(""))
	// fmt.Println(MyAtoi("+1"))
	// fmt.Println(MyAtoi("42"))
	// fmt.Println(MyAtoi("   -42"))
	// fmt.Println(MyAtoi("4193 with words"))
	// fmt.Println(MyAtoi("words and 987"))
	fmt.Println(MyAtoi("-91283472332"))
	fmt.Println(MyAtoi2("-91283472332"))

	root := TreeNode{
		Val: 4,
		Left: &TreeNode{
			Val: 2,
			Left: &TreeNode{
				Val: 1,
			},
			Right: &TreeNode{
				Val: 3,
			},
		},
		Right: &TreeNode{
			Val: 7,
			Left: &TreeNode{
				Val: 6,
			},
			Right: &TreeNode{
				Val: 9,
			},
		},
	}
	newRoot := invertTree(&root)
	fmt.Printf("%v", &newRoot)

	root2 := TreeNode{
		Val: 4,
		Left: &TreeNode{
			Val: 2,
		},
	}
	newRoot = invertTree(&root2)
	fmt.Printf("%v", &newRoot)

	root = TreeNode{
		Val: 3,
		Left: &TreeNode{
			Val: 9,
		},
		Right: &TreeNode{
			Val: 20,
			Left: &TreeNode{
				Val: 15,
			},
			Right: &TreeNode{
				Val: 7,
			},
		},
	}

	maxDeep := maxDepth(&root)
	fmt.Printf("max depth = %d", maxDeep)

	root = TreeNode{
		Val: 2,
		Left: &TreeNode{
			Val: 1,
		},
		Right: &TreeNode{
			Val: 3,
		},
	}
	fmt.Printf("check bst = %v", isValidBST(&root))

	root = TreeNode{
		Val: 4,
		Left: &TreeNode{
			Val: 2,
			Left: &TreeNode{
				Val: 1,
			},
			Right: &TreeNode{
				Val: 3,
			},
		},
		Right: &TreeNode{
			Val: 5,
			Right: &TreeNode{
				Val: 6,
			},
		},
	}
	middleOrder(&root)

	root = TreeNode{
		Val: 2,
		Left: &TreeNode{
			Val: 1,
		},
		Right: &TreeNode{
			Val: 3,
		},
	}
	fmt.Printf("%v", checkBSTByMiddleOrder(&root))

	root = TreeNode{
		Val: 1,
		Left: &TreeNode{
			Val: 2,
		},
		Right: &TreeNode{
			Val: 3,
		},
	}
	fmt.Printf("%v", hasPathSum(&root, 5))

	metrics := make([][]byte, 4)
	metrics = [][]byte{{'1', '1', '1', '1', '0'}, {'1', '1', '0', '1', '0'}, {'1', '1', '0', '0', '0'}, {'0', '0', '0', '0', '0'}}
	fmt.Printf("%d", numIslands(metrics))
	metrics = [][]byte{{'1', '1', '1', '1', '0'}, {'1', '1', '0', '1', '0'}, {'1', '1', '0', '0', '0'}, {'0', '0', '0', '0', '0'}}
	fmt.Printf("%d", bfsSearchLands(metrics))

	metrics = [][]byte{{'1', '1', '0', '0', '0'}, {'1', '1', '0', '0', '0'}, {'0', '0', '1', '0', '0'}, {'0', '0', '0', '1', '1'}}
	fmt.Printf("%d", bfsSearchLands(metrics))
	fmt.Println()
	fmt.Printf("最大回撤值为：%d", maxDrawDown([]int{4, 5, 6, 8, 4, 3, 2, 6, 9, 1}))
	fmt.Println("动态规划...")
	fmt.Printf("%d台阶总有%d跳法", 10, dp.DP1(10))

	url.Parse("http://localhost/?a=1&b=2&c=3#path")

	removeElement3([]int{3, 2, 2, 3}, 3)
}

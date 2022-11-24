package main

func search(nums []int, target int) int {
	for i, v := range nums {
		if v == target {
			return i
		}
	}
	return -1
}

func search2(nums []int, target int) int {
	left := 0
	right := len(nums) - 1
	for {
		if left <= right {
			mid := left + (right-left)/2 // 防止溢出，等同于 (left + right)/2
			if nums[mid] == target {
				return mid
			} else if nums[mid] < target {
				left = mid + 1
				continue
			} else {
				right = mid - 1
				continue
			}
		}
		return -1
	}
}

/*
[0,1,2,3,4,5,6,7,8,9] 10
10/2=5 s[5]=5<10  [0,1,2,3,4] [5,6,7,8,9]
(6+9)/2=7 => s[7]=7<10
(8+9)/2=8 => s[8]=8<10
(9+9)/2=9 => s[9]=9<10

*/

func main() {
	search2([]int{2, 5}, 5)
}

package main

// 删除数组元素
func removeElement(nums []int, val int) int {
	l := len(nums)
	for i := 0; i < l; i++ {
		if nums[i] == val {
			for j := i + 1; j < l; j++ {
				nums[j-1] = nums[j]
			}
			i--
			l--
		}
	}
	return l
}

// 快慢指针
func removeElement2(nums []int, val int) int {
	l := len(nums)
	slowIndex := 0
	for quickIndex := slowIndex; quickIndex < l; quickIndex++ {
		if nums[quickIndex] != val {
			nums[slowIndex] = nums[quickIndex]
			slowIndex += 1
		}
	}
	return slowIndex
}

func removeElement3(nums []int, val int) int {
	var leftIndex = 0
	var rightIndex = len(nums) - 1
	for rightIndex >= 0 && nums[rightIndex] == val {
		rightIndex--
	}
	for leftIndex <= rightIndex {
		for nums[leftIndex] == val {
			nums[leftIndex] = nums[rightIndex]
			rightIndex--
		}
		leftIndex++
		for rightIndex >= 0 && nums[rightIndex] == val {
			rightIndex--
		}
	}
	return leftIndex
}

/*
j				     j                    j
0 1 2 2 3 3 0 4 2  0 1 2 2 3 3 0 4 2  0 1 2 2 3 3 0 4 2
i                    i                    i
*/

package main

// https://leetcode.cn/problems/jump-game-ii/
func jumpii(nums []int) int {
	length := len(nums)
	step := 0 // 步数
	if length <= 1 {
		return step
	}
	curDistance := 0  // 当前位置元素跳的最远范围
	nextDistance := 0 // 下一个位置元素跳的最远范围
	for i := 0; i <= length; i++ {
		// 第i个元素能跳的最远步数，从当前位置跳nums[i]的步数
		maxStep := i + nums[i]
		// 更新最远的步数
		nextDistance = max(maxStep, nextDistance)
		// 如果当前到达的位置为最远范围位置
		if i == curDistance {
			// 如果最远位置不是终点，步数加1，更新当前（下一步）的最远位置范围
			if curDistance < len(nums)-1 {
				step++
				if nextDistance >= len(nums)-1 {
					break
				}
				curDistance = nextDistance
			} else {
				break
			}
		}
	}
	return step
}

// https://leetcode.cn/problems/jump-game-ii/
func jumpii2(nums []int) int {
	length := len(nums)
	step := 0         // 步数
	curDistance := 0  // 当前位置元素跳的最远范围
	nextDistance := 0 // 下一个位置元素跳的最远范围
	for i := 0; i < length-1; i++ {
		nextDistance = max(i+nums[i], nextDistance) // 下一步最远位置
		if i == curDistance {
			curDistance = nextDistance
			step++
		}
	}
	return step
}

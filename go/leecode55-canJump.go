package main

// https://leetcode.cn/problems/jump-game/
func canJump(nums []int) bool {
	length := len(nums)
	if length <= 1 {
		return true
	}
	skip := 0 // 范围
	for i := 0; i <= skip; i++ {
		// 第i个元素能跳的最远步数，从当前位置跳nums[i]的步数
		step := i + nums[i]
		// 更新最远的步数
		skip = max(step, skip)
		if skip >= length-1 {
			return true
		}
	}
	return false
}

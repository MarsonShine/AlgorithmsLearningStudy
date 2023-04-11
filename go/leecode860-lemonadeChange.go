package main

// https://leetcode.cn/problems/lemonade-change/
func lemonadeChange(bills []int) bool {
	length := len(bills)
	if length == 0 {
		return false
	}
	if bills[0] != 5 {
		return false
	}
	var five, ten int = 1, 0
	for i := 1; i < length; i++ {
		targetBill := bills[i]
		if targetBill == 5 {
			five++
		} else if targetBill == 10 {
			if five == 0 {
				return false
			}
			five--
			ten++
		} else {
			// 20: 1. 一张10，1一张5; 2. 3个5
			if ten > 0 && five > 0 {
				five--
				ten--
			} else if five >= 3 {
				five -= 3
			} else {
				return false
			}
		}
	}
	return true
}

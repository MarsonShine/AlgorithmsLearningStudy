package main

import "math"

// https://leetcode.cn/problems/gas-station/
func canCompleteCircuit(gas []int, cost []int) int {
	length := len(gas)
	for i := 0; i < length; i++ {
		if gas[i] >= cost[i] {
			// 循环length次
			j := i
			count := length - 1
			sum := 0
			for count >= 0 {
				if j > length-1 {
					j = 0
				}
				sum += gas[j] - cost[j]
				if sum < 0 {
					break
				}
				if count == 0 {
					return i
				}
				j++
				count--
			}
		}

	}
	return -1
}

/*
计算出从起点到每个加油站剩余的油量，同时记录下所有剩余油量的最小值min。如果sum小于0，则说明从起点到当前加油站中的任何一个加油站都无法到达，而min则记录了从起点到任何一个加油站时的最低油量，也就是最容易油量不足的位置。

如果总的油量sum小于0，说明无法从任何一个加油站出发成功绕一圈，直接返回-1。如果min大于等于0，说明整个数组的剩余油量都大于等于0，可以从起点出发成功绕一圈，直接返回0。

如果min小于0，则需要从后向前遍历数组，计算从当前加油站出发到达起点剩余的油量。如果剩余油量不足，说明从当前加油站无法出发成功绕一圈，将min更新为当前剩余油量，并继续向前遍历。如果在遍历过程中出现了min大于等于0的情况，说明找到了起点，返回当前位置即可。如果遍历完成后仍未找到起点，则返回-1。
*/
func canCompleteCircuit2(gas []int, cost []int) int {
	sum := 0
	min := math.MaxInt
	for i := 0; i < len(gas); i++ {
		sum += gas[i] - cost[i]
		if sum < min {
			min = sum
		}
	}
	if sum < 0 {
		return -1
	}
	// 有多余的油
	if min >= 0 {
		return 0
	}
	for i := len(gas) - 1; i >= 0; i-- {
		rest := gas[i] - cost[i]
		min += rest
		if min >= 0 {
			return i
		}
	}
	return -1
}

/*
从阅读题意可以得知：如果从起点开始走到某个加油站i时，发现油量不足以到达下一个加油站j（i < j），那么从任意一个i和j之间的加油站出发，都无法到达加油站j。因为从这些加油站出发，油量一定更少。

所以我们可以采用贪心的策略：从起点开始，依次遍历每个加油站，记录从起点开始到当前加油站的油量和sum。如果在遍历过程中，发现sum小于0，那么就说明从起点到当前加油站中的任何一个加油站都无法到达，所以我们就选择从当前加油站的下一个加油站出发。因为如果从当前加油站出发，到达下一个加油站也无法完成一圈，那么从当前加油站之前的任意一个加油站出发，都一定无法到达下一个加油站。因此，我们可以将起始位置设为下一个加油站，从下一个加油站开始重新计算sum。如果遍历完成后，sum仍然大于等于0，那么说明我们可以从起点出发成功绕完一圈。如果sum小于0，那么说明无法从任何一个加油站出发成功绕一圈。

其核心思路为：如果当前位置的油量无法到达下一个加油站，那么就从下一个加油站开始重新计算油量。因为从当前加油站之前的任何一个加油站出发，都无法到达下一个加油站。
*/
func canCompleteCircuit3(gas []int, cost []int) int {
	/*
			1.定义变量sum和totalSum，分别表示从起点到当前加油站的油量和总油量。变量startIndex记录成功绕一圈的起始位置，初始化为0。
		    2.从第一个加油站开始遍历，依次计算sum和totalSum，如果sum小于0，说明从起点到当前加油站中的任何一个加油站都无法到达，将startIndex设置为当前加油站的下一个加油站，并将sum重置为0。
		    3。遍历完成后，如果totalSum小于0，说明无法从任何一个加油站出发成功绕一圈，返回-1。否则返回startIndex，即成功绕一圈的起始位置。
	*/
	sum, totalSum := 0, 0
	startIndex := 0
	for i := 0; i < len(gas); i++ {
		sum += gas[i] - cost[i]
		totalSum += gas[i] - cost[i]
		if sum < 0 {
			startIndex = i + 1
			sum = 0
		}
	}
	if totalSum < 0 {
		return -1
	}
	return startIndex
}

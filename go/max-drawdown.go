package main

func maxDrawDown(array []int) int {
	var maxValue int = 0
	var minValue int = 0
	var maxDrawDownValue int = 0
	for i := 0; i < len(array); i++ {
		if maxValue < array[i] {
			maxValue = array[i]
			minValue = array[i]
		}
		if minValue > array[i] {
			minValue = array[i]
		}
		if maxValue-minValue > maxDrawDownValue {
			maxDrawDownValue = maxValue - minValue
		}
	}
	return maxDrawDownValue
}

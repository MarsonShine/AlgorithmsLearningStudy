package main

// https://leetcode.cn/problems/letter-combinations-of-a-phone-number/
var (
	result        = []string{}
	pathBytes     = []byte{}
	numberLatters = []string{"", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz"}
)

func letterCombinations(digits string) []string {
	pathBytes = []byte{}
	result = []string{}
	if len(digits) != 0 {
		letterCombinationsBackTracking(digits, 0)
	}
	return result
}

func letterCombinationsBackTracking(digits string, index int) {
	if len(digits) == index {
		result = append(result, string(pathBytes))
		return
	}
	digit := int(digits[index] - '0')
	letters := numberLatters[digit]
	for i := 0; i < len(letters); i++ {
		pathBytes = append(pathBytes, letters[i])
		letterCombinationsBackTracking(digits, index+1)
		pathBytes = pathBytes[:len(pathBytes)-1]
	}
}

package main

import (
	"math"
	"unicode"
)

func MyAtoi2(s string) int {
	ac := NewAutomatonWithStatus()
	for _, r := range s {
		ac.get(r)
	}
	return ac.getValue()
}

func MyAtoi(s string) int {
	var sign bool = true
	var index int = 0
	for _, v := range s {
		// 去前置空格
		if v == 32 {
			index++
			continue
		} else {
			break
		}
	}
	// 当index=len
	if index == len(s) {
		return 0
	}
	// 判断符号
	// +
	if s[index] == 43 {
		sign = true
		index++
	} else if s[index] == 45 {
		sign = false
		index++
	}
	var ans int = 0
	for i := index; i < len(s); i++ {
		// 判断是否为数字
		if !unicode.IsDigit(rune(s[i])) {
			return getValue(sign, ans)
		}
		if unicode.IsDigit(rune(s[i])) {
			digit := s[i] - '0'
			// 判断是否越界
			if ans > (math.MaxInt32-int(digit))/10 {
				if sign {
					return math.MaxInt32
				} else {
					return math.MinInt32
				}
			}
			ans = ans*10 + int(digit)
		}
	}
	return getValue(sign, ans)
}

func getValue(sign bool, v int) int {
	if sign {
		return v
	} else {
		return -v
	}
}

// 有限状态自动机
// 根据题目规则构建状态机集合
type Automaton struct {
	statusMap map[string][]string
	sign      int
	value     int
	status    string
}

func NewAutomatonWithStatus() *Automaton {
	return &Automaton{
		statusMap: map[string][]string{
			"start":  {"start", "signed", "number", "end"},
			"signed": {"end", "end", "number", "end"},
			"number": {"end", "end", "number", "end"},
			"end":    {"end", "end", "end", "end"},
		},
		sign:   1,
		value:  0,
		status: "start",
	}
}

func (ac *Automaton) getValue() int {
	return ac.sign * ac.value
}

func (ac *Automaton) get(r rune) {
	ac.status = ac.statusMap[ac.status][ac.getKey(r)]
	if ac.status == "number" {
		digit := int(r - '0')
		// 判断是否越界
		if ac.value > (math.MaxInt32-int(digit))/10 {
			if ac.sign == 1 {
				ac.value = math.MaxInt32
			} else {
				ac.value = -math.MinInt32
			}
		} else {
			ac.value = ac.value*10 + digit
		}
	} else if ac.status == "signed" {
		if r == '-' {
			ac.sign = -1
		}
	}
}

func (ac *Automaton) getKey(r rune) int {
	if r == ' ' {
		return 0
	}
	if r == '+' || r == '-' {
		return 1
	}
	if unicode.IsDigit(r) {
		return 2
	}
	return 3
}

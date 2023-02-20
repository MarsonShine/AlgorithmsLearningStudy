package main

import (
	"strconv"
	"strings"
)

// https://leetcode.cn/problems/restore-ip-addresses/
// 复原 IP 地址
func restoreIpAddresses(s string) []string {
	results := []string{}
	paths := []string{}
	var getRestoreIpAddress func(ss string, index int)
	getRestoreIpAddress = func(ss string, index int) {
		if len(paths) == 4 {
			if index == len(ss) {
				ip := strings.Join(paths, ".")
				results = append(results, ip)
			}
			return
		}
		for i := index; i < len(ss); i++ {
			if i != index && ss[index] == '0' {
				break
			}
			str := ss[index : i+1]
			num, _ := strconv.Atoi(str)
			if num >= 0 && num <= 255 {
				paths = append(paths, str)
				getRestoreIpAddress(ss, i+1)
				paths = paths[:len(paths)-1]
			} else {
				break
			}
		}
	}
	getRestoreIpAddress(s, 0)
	return results
}

func restoreIpAddresses2(s string) []string {
	results := []string{}
	var getRestoreIpAddress func(ss string, index, pointNumber int)
	getRestoreIpAddress = func(ss string, index, pointNumber int) {
		if pointNumber == 3 {
			if isValidIP(ss, index, len(ss)-1) {
				results = append(results, ss)
			}
			return
		}
		for i := index; i < len(ss); i++ {
			if isValidIP(ss, index, i) { // 判断IP的子段是否有效
				ss = ss[:i+1] + "." + ss[i+1:]
				pointNumber++
				getRestoreIpAddress(ss, i+2, pointNumber)
				pointNumber--
				ss = ss[:i+1] + ss[i+2:]
			} else {
				break
			}
		}
	}
	getRestoreIpAddress(s, 0, 0)
	return results
}

func isValidIP(s string, index, end int) bool {
	if index > end {
		return false
	}
	if s[index] == '0' && index != end {
		return false
	}
	num := 0
	for i := index; i <= end; i++ {
		if s[i] > '9' || s[i] < '0' { // 字符集对应的数字大于9或小于0
			return false
		}
		num = num*10 + int(s[i]-'0')
		if num > 255 {
			return false
		}
	}
	return true
}

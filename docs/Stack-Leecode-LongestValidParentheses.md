# 栈相关操作算法——求最长有效括号

Q：https://leetcode-cn.com/problems/longest-valid-parentheses/

给你一个只包含 `'('` 和 `')'` 的字符串，找出最长有效（格式正确且连续）括号子串的长度。

解决思路：首先分析题干，只有 `(` 和 `)`，并且要找到**连续的**能匹配的最长括号字符串长度。例如：`(()` 就是最长括号数为 2；`)()())` 值为 4；`)())()()` 值为 4（注意，不是 6，中间断了）。

因为要找出匹配的括号，所以这个思路我们继续沿用[leecode20-有效括号](https://leetcode-cn.com/problems/valid-parentheses/)的思路：用栈的特性，入栈 `(`，循环遍历字符串 s，遇到 `)` 就出栈表示匹配。那么剩下的就是如何找到连续这个特征。很容易想到的时候**当栈为空时肯定表示所有括号都刚好匹配**。当循环结束之后不为空时，就代表肯定不是连续的。那么就得想办法将**中断前后能匹配的最长符号是多少得存下来**。

咱们以上面举的例子中最后一个为例：求字符串 `)())()()` 的最长连续有效符号。为了方便的描述各个符号，我们用数值来表示这些括号：`0 1 2 3 4 5 6 7`

从这个字符串我们可以看出中断（不能匹配）的符号分别是 `0,3`。那么对应到匹配的最长字符为 `0,2,4`。能看出来什么来了么？

我们再来看一个更长一点的例子：`)(()))())()`，对应的数值：`0 1 2 3 4 5 6 7 8 9 10`；中断符号为 `0,5,8 `。那么匹配的最长字符为 `0,4,2,2`。其实我们借助这些数组下标和中断的位置以及匹配的最长值，我们发现**连续的有效括号就是中断符号所处位置的前后的差值**。即：

- 中断符号位置 0，这个时候没有匹配任何符号就是 0
- 等下一个不匹配的位置 5，那么就是说前面已经连续匹配到了下标为 4 的括号。这个时候我们就能知道此时最长有效括号数就为 `4-0`。
- 接着下一个不匹配的位置是 8，那么从上一个不匹配的 5 开始匹配到了下标为 7，此时的最长有小括号数为 `7-5`。
- 往后没有了，说明剩下的符号都匹配了，则最长匹配符号数为 `10-8`
- 每次遇到不匹配的字符时，就能知道此时的最长有效符号数以及最大的连续符号数是多少

```go
func longestValidParentheses(s string) int {
	// 遍历
	l := len(s)
	notmatch := []int{}
	notmatch = append(notmatch, -1)
	maxLength := 0

	arr := []byte{}
	left := byte('(')  // (
	right := byte(')') // )
	for i := 0; i < l; i++ {
		arrl := len(arr)
		if s[i] == right {
			if arrl != 0 && arr[arrl-1] == left {
				arr = arr[:arrl-1]
				notmatch = notmatch[:len(notmatch)-1]
				longLength := i - notmatch[len(notmatch)-1]
				if maxLength < longLength {
					maxLength = longLength
				}
			} else {
				arr = append(arr, s[i])
				notmatch = append(notmatch, i)
			}
		} else {
			arr = append(arr, s[i])
			notmatch = append(notmatch, i)
		}
	}
	return maxLength
}
```

当然还可以有其它解法，leecode 上已经有详细说明了。因为跟栈关系不大，这里就不讲了。
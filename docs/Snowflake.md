# 雪花算法生成ID

雪花 ID 使用在分布式计算中生成唯一 id 的一套算法。

## 雪花 ID 格式

雪花 ID 总共有 64 位。第一部分为 41 位，表示时间戳，表示从纪元时间到现在的毫秒数。下一部份是 10 位，表示一个 机器 ID，用来分区防止数据冲突的。另外 12 位表示每台机器的序列号，以允许在同一毫秒内创建多个雪花。

由于雪花 ID 是 64 位，这超过了 Javascript 所能支持的最大整数范围，所以必须要以字符串形式处理。

雪花 ID 是通过时间增量排序的，因为它们是基于时间创建的。另外，雪花 ID 的创建时间可以从雪花 ID 中计算出来。这可以用于获取在特定日期之前或之后创建的雪花 ID(及其关联的对象)

> 纪元时间：是一种计算时间，是计算机用来测量系统时间的日期和时间。大部分系统纪元时间都是从 1970 00:00:00 计算的时间戳

## 参考资料

- https://en.wikipedia.org/wiki/Snowflake_ID
- https://en.wikipedia.org/wiki/Epoch_(computing)
- https://www.cnblogs.com/sunyuliang/archive/2020/01/07/12161416.html

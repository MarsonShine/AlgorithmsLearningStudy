using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmPractices.SnowFlakes
{
    /// <summary>
    /// 雪花ID算法
    /// https://www.cnblogs.com/sunyuliang/archive/2020/01/07/12161416.html
    /// </summary>
    public class SnowflakeIdGenerator
    {
        private static readonly DateTime JAN_1ST_1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>
        /// (new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc) - JAN_1ST_1970).TotalMilliseconds
        /// </summary>
        private const long TW_EPOCH = 1577836800000L;
        /// <summary>
        /// 机器id所占的位数
        /// </summary>
        private const int WORKER_ID_BITS = 5;

        /// <summary>
        /// 数据标识id所占的位数
        /// </summary>
        private const int DATACENTER_ID_BITS = 5;

        /// <summary>
        /// 支持的最大机器id，结果是31 (这个移位算法可以很快的计算出几位二进制数所能表示的最大十进制数) 
        /// </summary>
        private const long MAX_WORKER_ID = -1L ^ (-1L << WORKER_ID_BITS);

        /// <summary>
        /// 支持的最大数据标识id，结果是31 
        /// </summary>
        private const long MAX_DATACENTER_ID = -1L ^ (-1L << DATACENTER_ID_BITS);

        /// <summary>
        /// 序列在id中占的位数 
        /// </summary>
        private const int SEQUENCE_BITS = 12;

        /// <summary>
        /// 数据标识id向左移17位(12+5) 
        /// </summary>
        private const int DATACENTER_ID_SHIFT = SEQUENCE_BITS + WORKER_ID_BITS;

        /// <summary>
        /// 机器ID向左移12位 
        /// </summary>
        private const int WORKER_ID_SHIFT = SEQUENCE_BITS;


        /// <summary>
        /// 时间截向左移22位(5+5+12) 
        /// </summary>
        private const int TIMESTAMP_LEFT_SHIFT = SEQUENCE_BITS + WORKER_ID_BITS + DATACENTER_ID_BITS;

        /// <summary>
        /// 生成序列的掩码，这里为4095 (0b111111111111=0xfff=4095) 
        /// </summary>
        private const long SEQUENCE_MASK = -1L ^ (-1L << SEQUENCE_BITS);

        /// <summary>
        /// 数据中心ID(0~31)
        /// </summary>
        public long DatacenterId { get; private set; }

        /// <summary>
        /// 工作机器ID(0~31) 
        /// </summary>
        public long WorkerId { get; private set; }

        /// <summary>
        /// 毫秒内序列(0~4095) 
        /// </summary>
        public long Sequence { get; private set; }

        /// <summary>
        /// 上次生成ID的时间截 
        /// </summary>
        public long LastTimestamp { get; private set; }

        public SnowflakeIdGenerator(long datacenterId,long workerId)
        {
            if (datacenterId > MAX_DATACENTER_ID || datacenterId < 0)
                throw new InvalidOperationException($"datacenter Id can't be greater than {MAX_DATACENTER_ID} or less than 0");
            if (workerId > MAX_WORKER_ID || workerId < 0)
                throw new InvalidOperationException($"worker Id can't be greater than {MAX_WORKER_ID} or less than 0");

            WorkerId = workerId;
            DatacenterId = datacenterId;
            Sequence = 0;
            LastTimestamp = -1L;
        }

        public long NextId()
        {
            lock (this)
            {
                long timestamp = GetCurrentTimestamp();
                if(timestamp > LastTimestamp)   // 时间戳改变，毫秒内序列重置
                {
                    Sequence = 0L;
                }
                else if(timestamp == LastTimestamp) // 时间戳相等，则再相同的毫秒内序号+1
                {
                    Sequence = (Sequence + 1) & SEQUENCE_MASK;
                    if(Sequence == 0)
                    {
                        timestamp = GetNextTimestamp(LastTimestamp); //阻塞到下一个毫秒，获得新的时间戳
                    }
                }
                else // 当前时间小于最后生成ID的时间戳，说明系统时间被回拨，需要做额外的处理
                {
                    Sequence = (Sequence + 1) & SEQUENCE_MASK;
                    if(Sequence > 0)
                    {
                        timestamp = LastTimestamp;  // 停留在最后一次时间戳,等待系统追上即完全度过了时钟回拨问题
                    }
                    else
                    {
                        timestamp = LastTimestamp + 1; 
                    }
                    // 原算法中遇到始终回拨问题直接抛出异常
                    // throw new Exception("始终回拨异常,无法生成id");
                }
                LastTimestamp = timestamp;

                // 移位并通过或运算拼接成64位的ID
                var id = ((timestamp - TW_EPOCH) << TIMESTAMP_LEFT_SHIFT)
                | (DatacenterId << DATACENTER_ID_SHIFT)
                    | (WorkerId << WORKER_ID_SHIFT)
                    | Sequence;

                return id;
            }
        }

        /// <summary>
        /// 解析雪花ID
        /// </summary>
        /// <returns></returns>
        public static string AnalyzeId(long id)
        {
            StringBuilder sb = new StringBuilder();

            var timestamp = (id >> TIMESTAMP_LEFT_SHIFT);
            var time = JAN_1ST_1970.AddMilliseconds(timestamp + TW_EPOCH);
            sb.Append(time.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff"));

            var datacenterId = (id ^ (timestamp << TIMESTAMP_LEFT_SHIFT)) >> DATACENTER_ID_SHIFT;
            sb.Append("_" + datacenterId);

            var workerId = (id ^ ((timestamp << TIMESTAMP_LEFT_SHIFT) | (datacenterId << DATACENTER_ID_SHIFT))) >> WORKER_ID_SHIFT;
            sb.Append("_" + workerId);

            var sequence = id & SEQUENCE_MASK;
            sb.Append("_" + sequence);

            return sb.ToString();
        }

        private long GetNextTimestamp(long lastTimestamp)
        {
            long timestamp = GetCurrentTimestamp();
            while (timestamp <= LastTimestamp)
            {
                timestamp = GetCurrentTimestamp();
            }
            return timestamp;
        }

        private long GetCurrentTimestamp()
        {
            return (long)(DateTime.UtcNow - JAN_1ST_1970).TotalMilliseconds;
        }
    }
}

namespace Essensoft.AspNetCore.Snowflake
{
    /// <summary>
    /// Snowflake 选项
    /// </summary>
    public class SnowflakeOptions
    {
        /// <summary>
        /// 时代。默认为：2018-12-1 00:00:00 +00:00
        /// </summary>
        public long Epoch { get; set; } = 1543622400000L;

        /// <summary>
        /// 工作机器Id位数
        /// </summary>
        public int WorkerIdBits { get; set; } = 5;

        /// <summary>
        /// 数据中心Id位数
        /// </summary>
        public int DatacenterIdBits { get; set; } = 5;

        /// <summary>
        /// 序列位数
        /// </summary>
        public int SequenceBits { get; set; } = 12;

        /// <summary>
        /// 工作机器Id
        /// </summary>
        public long WorkerId { get; set; } = 0;

        /// <summary>
        /// 数据中心Id
        /// </summary>
        public long DatacenterId { get; set; } = 0;

        /// <summary>
        /// 起始序列号
        /// </summary>
        public long Sequence { get; set; } = 0;

        /// <summary>
        /// 最大工作机器Id
        /// </summary>
        internal long MaxWorkerId => -1L ^ (-1L << WorkerIdBits);

        /// <summary>
        /// 最大数据中心Id
        /// </summary>
        internal long MaxDatacenterId => -1L ^ (-1L << DatacenterIdBits);

        /// <summary>
        /// 工作机器Id位移
        /// </summary>
        internal int WorkerIdShift => SequenceBits;

        /// <summary>
        /// 数据中心Id位移
        /// </summary>
        internal int DatacenterIdShift => SequenceBits + WorkerIdBits;

        /// <summary>
        /// 时间戳位移
        /// </summary>
        internal int TimestampLeftShift => SequenceBits + WorkerIdBits + DatacenterIdBits;

        /// <summary>
        /// 序列掩码
        /// </summary>
        internal long SequenceMask => -1L ^ (-1L << SequenceBits);
    }
}

namespace Discus.SDK.IdGenerater.IdGeneraterFactory
{
    public static class IdGenerater
    {
        private static bool _isSet = false;
        private static readonly object _locker = new();

        public static byte WorkerIdBitLength => 6;
        public static byte SeqBitLength => 6;
        public static short MaxWorkerId => (short)(Math.Pow(2.0, WorkerIdBitLength) - 1);
        public static short CurrentWorkerId { get; private set; } = -1;

        /// <summary>
        /// 初始化Id生成器
        /// </summary>
        /// <param name="workerId"></param>
        public static void SetWorkerId(ushort workerId)
        {
            if (_isSet)
                throw new InvalidOperationException("只能初始化一次！");

            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException($"允许最大的机器Id{MaxWorkerId}，机器Id不能超过设置的最大Id或者小于0！");
            }
               

            lock (_locker)
            {
                if (_isSet)
                    throw new InvalidOperationException("只能初始化一次！");

                YitIdHelper.SetIdGenerator(new IdGeneratorOptions(workerId)
                {
                    WorkerIdBitLength = WorkerIdBitLength,
                    SeqBitLength = SeqBitLength
                });

                CurrentWorkerId = (short)workerId;
                _isSet = true;
            }
        }

        /// <summary>
        /// 获取唯一Id，默认支持64个节点，每毫秒下的序列数6位。
        /// 在默认配置下，ID可用 71000 年不重复
        /// 在默认配置下，70年才到 js Number Max 值
        /// 默认情况下，500毫秒可以生成10W个号码。
        /// 如果需要提高速度，可以修改SeqBitLength长度。当SeqBitLength =10 ,100W个id约800毫秒。
        /// </summary>
        /// <returns>Id</returns>
        public static long GetNextId()
        {
            if (!_isSet)
                throw new InvalidOperationException("请初始化Id生成器！");

            return YitIdHelper.NextId();
        }
    }
}

using Discus.SDK.Core.System.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;

namespace Discus.SDK.IdGenerater.IdGeneraterFactory
{
    public sealed class WorkerNode
    {
        private readonly ILogger<WorkerNode> _logger;
        private readonly IRedisClient _redisClient;
        private readonly IDistributedLocker _distributedLocker;

        public WorkerNode(ILogger<WorkerNode> logger, IRedisClient redisClient, IDistributedLocker distributedLocker)
        {
            _logger = logger;
            _redisClient = redisClient;
            _distributedLocker = distributedLocker;
        }

        internal async Task InitWorkerNodesAsync(string serviceName)
        {
            var workerIdSortedSetCacheKey = GetWorkerIdCacheKey(serviceName);
            if (!_redisClient.KeyExists(workerIdSortedSetCacheKey))
            {
                _logger.LogInformation("开始获取Id值，RedisKey值:{0}", workerIdSortedSetCacheKey);
                var flag = await _distributedLocker.LockAsync(workerIdSortedSetCacheKey);
                if (!flag.Success)
                {
                    await Task.Delay(300);
                    await InitWorkerNodesAsync(serviceName);
                }

                long count = 0;  
                try
                {
                    (decimal, object)[] cacheValues = new (decimal, object)[IdGenerater.MaxWorkerId];
                    for (long index = 0; index <= cacheValues.Length-1; index++)
                    {
                        cacheValues[index] = ((decimal)DateTime.Now.GetTotalMilliseconds(), index + 1);
                    }
                    //await Task.Delay(300);
                    //count = await _redisClient.ZAddAsync(workerIdSortedSetCacheKey, cacheValues);
                    count =  _redisClient.ZAdd(workerIdSortedSetCacheKey, cacheValues);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    await _distributedLocker.SafedUnLockAsync(workerIdSortedSetCacheKey, flag.LockValue);
                }
                _logger.LogInformation("最后生成Id的Key值:{0}，生成数量为:{1}", workerIdSortedSetCacheKey, count);
            }
            else
            { 
                _logger.LogInformation("退出生成Id，RedisKey值:{0}", workerIdSortedSetCacheKey);
            }
        }

        internal async Task<long> GetWorkerIdAsync(string serviceName)
        {
            var workerIdSortedSetCacheKey = GetWorkerIdCacheKey(serviceName);

            var scirpt = @"local workerids = redis.call('ZRANGE', KEYS[1], 0,0) redis.call('ZADD', KEYS[1],ARGV[2],workerids[1]) return workerids[1]";
            var parameters = new object[2] { workerIdSortedSetCacheKey, DateTime.Now.GetTotalMilliseconds() };
            var luaResult = await _redisClient.EvalAsync(scirpt, workerIdSortedSetCacheKey, parameters);
            var workerId = (long)Convert.ToDouble(luaResult);
            _logger.LogInformation("Get WorkerNodes:{0}", workerId);
            return workerId;
        }

        internal async Task RefreshWorkerIdScoreAsync(string serviceName, long workerId, double? workerIdScore = null)
        {
            if (workerId < 0 || workerId > IdGenerater.MaxWorkerId)
                throw new Exception(string.Format("worker Id can't be greater than {0} or less than 0", IdGenerater.MaxWorkerId));

            var workerIdSortedSetCacheKey = GetWorkerIdCacheKey(serviceName);

            workerIdScore = workerIdScore == null ? DateTime.Now.GetTotalMilliseconds() : workerIdScore.Value;
            decimal value = (decimal)workerIdScore;
            (decimal, object)[] cacheValues = new (decimal, object)[1] { (value, workerId) };
            await _redisClient.ZAddAsync(workerIdSortedSetCacheKey, cacheValues);
            _logger.LogDebug("Refresh WorkerNodes:{0}:{1}", workerId, workerIdScore);
        }


        internal static string GetWorkerIdCacheKey(string serviceName) => $"{serviceName}:workids";
    }
}

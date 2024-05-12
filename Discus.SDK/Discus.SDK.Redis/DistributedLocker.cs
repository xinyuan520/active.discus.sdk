using Discus.SDK.Redis.Core;
using Newtonsoft.Json.Linq;

namespace Discus.SDK.Redis
{
    public class DistributedLocker : IDistributedLocker
    {
        public IRedisClient RedisClient { get; set; }

        public DistributedLocker(IRedisClient redisClient)
        {
            RedisClient = redisClient;
        }

        public async Task<(bool Success, string LockValue)> LockAsync(string cacheKey, int timeoutSeconds = 5, bool autoDelay = false)
        {
            var lockKey = GetLockKey(cacheKey);
            var lockValue = Guid.NewGuid().ToString();
            var timeoutMilliseconds = timeoutSeconds * 1000;
            var flag = RedisClient.StringSet(lockKey, lockValue, timeoutMilliseconds, RedisExistence.Nx);
            if (flag && autoDelay)
            {
                var refreshMilliseconds = (int)(timeoutMilliseconds / 2.0);
                var autoDelayTimer = new Timer(timerState => Delay(lockKey, lockValue, timeoutMilliseconds), null, refreshMilliseconds, refreshMilliseconds);
                var addResult = AutoDelayTimers.Instance.TryAdd(lockKey, autoDelayTimer);
                if (!addResult)
                {
                    autoDelayTimer?.Dispose();
                    SafedUnLock(cacheKey, lockValue);
                    return (false, null);
                }
            }
            return (flag, flag ? lockValue : null);
        }

        public bool SafedUnLock(string cacheKey, string lockValue)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));
            ArgumentCheck.NotNullOrWhiteSpace(lockValue, nameof(lockValue));

            var lockKey = GetLockKey(cacheKey);
            AutoDelayTimers.Instance.CloseTimer(lockKey);

            var script = @"local invalue = ARGV[2]
                                    local currvalue = redis.call('get', ARGV[1])
                                    if(invalue==currvalue) then redis.call('del', ARGV[1])
                                        return 1
                                    else
                                        return 0
                                    end";
            object[] parameters = { lockKey,lockValue };
            var result = (int)RedisClient.EvalAsync(script, lockKey, parameters).GetAwaiter().GetResult();
            return result == 1;
        }

        public (bool Success, string LockValue) Lock(string cacheKey, int timeoutSeconds = 5, bool autoDelay = false)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));
            ArgumentCheck.NotLessThanOrEqualZero(timeoutSeconds, nameof(timeoutSeconds));

            var lockKey = GetLockKey(cacheKey);
            var lockValue = Guid.NewGuid().ToString();
            var timeoutMilliseconds = timeoutSeconds * 1000;
            var expiration = TimeSpan.FromMilliseconds(timeoutMilliseconds);
            var flag = RedisClient.StringSet(lockKey, lockValue, timeoutMilliseconds, RedisExistence.Nx);
            if (flag && autoDelay)
            {
                var refreshMilliseconds = (int)(timeoutMilliseconds / 2.0);
                var autoDelayTimer = new Timer(timerState => Delay(lockKey, lockValue, timeoutMilliseconds), null, refreshMilliseconds, refreshMilliseconds);
                var addResult = AutoDelayTimers.Instance.TryAdd(lockKey, autoDelayTimer);
                if (!addResult)
                {
                    autoDelayTimer?.Dispose();
                    SafedUnLock(cacheKey, lockValue);
                    return (false, null);
                }
            }
            return (flag, flag ? lockValue : null);
        }

        public async Task<bool> SafedUnLockAsync(string cacheKey, string cacheValue)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));

            var lockKey = GetLockKey(cacheKey);

            AutoDelayTimers.Instance.CloseTimer(lockKey);

            var script = @"local invalue = ARGV[2]
                                    local currvalue = redis.call('get',ARGV[1])
                                    if(invalue==currvalue) then redis.call('del',ARGV[1])
                                        return 1
                                    else
                                        return 0
                                    end";
            object[] parameters = { lockKey, cacheValue };
            var result = await RedisClient.EvalAsync(script, lockKey, parameters);
            return result == 1;
        }

        #region 私有方法
        private void Delay(string key, string value, int milliseconds)
        {
            if (!AutoDelayTimers.Instance.ContainsKey(key))
                return;
            var script = @"local val = redis.call('GET', ARGV[1])
                                    if val==ARGV[2] then
                                        redis.call('PEXPIRE', ARGV[1], ARGV[3])
                                        return 1
                                    end
                                    return 0";
            object[] parameters = { key, value, milliseconds };
            var result = RedisClient.EvalAsync(script, key, parameters).GetAwaiter().GetResult();
            if ((int)result == 0)
            {
                AutoDelayTimers.Instance.CloseTimer(key);
            }
            return;
        }

        private string GetLockKey(string cacheKey)
        {
            return $"locker:{cacheKey.Replace(":", "-")}";
        }
        #endregion
    }
}

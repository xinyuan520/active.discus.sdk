using CSRedis;
using Discus.SDK.Redis.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.Redis
{
    public class RedisClient : IRedisClient
    {
        private readonly IOptions<RedisConfiguration> _options;

        private readonly ILogger<RedisClient> _logger;

        public RedisClient(IOptions<RedisConfiguration> options, ILogger<RedisClient> logger)
        {
            _options = options;
            _logger = logger;
            var csredis = new CSRedisClient(_options.Value.Dbconfig.ConnectionString);
            RedisHelper.Initialization(csredis);
            logger.LogInformation("redis Initialization");
        }

        #region Keys
        public long KeyDel(string cacheKey)
        {
            return RedisHelper.Del(cacheKey);
        }

        public async Task<long> KeyDelAsync(string cacheKey)
        {
            return await RedisHelper.DelAsync(cacheKey);
        }

        public bool KeyExists(string cacheKey)
        {
            return RedisHelper.Exists(cacheKey);
        }

        public async Task<bool> KeyExistsAsync(string cacheKey)
        {
            return await RedisHelper.ExistsAsync(cacheKey);
        }

        public bool KeyExpire(string cacheKey, int second)
        {
            return RedisHelper.Expire(cacheKey, second);
        }

        public async Task<bool> KeyExpireAsync(string cacheKey, int second)
        {
            return await RedisHelper.ExpireAsync(cacheKey, second);
        }

        public long TTL(string cacheKey)
        {
            return RedisHelper.Ttl(cacheKey);
        }

        public async Task<long> TTLAsync(string cacheKey)
        {
            return await RedisHelper.TtlAsync(cacheKey);
        }
        #endregion

        #region String
        public long IncrBy(string cacheKey, long value = 1)
        {
            return RedisHelper.IncrBy(cacheKey, value);
        }

        public async Task<long> IncrByAsync(string cacheKey, long value = 1)
        {
            return await RedisHelper.IncrByAsync(cacheKey, value);
        }

        public decimal IncrByFloat(string cacheKey, decimal value = 1)
        {
            return RedisHelper.IncrByFloat(cacheKey, value);
        }

        public async Task<decimal> IncrByFloatAsync(string cacheKey, decimal value = 1)
        {
            return await RedisHelper.IncrByFloatAsync(cacheKey, value);
        }

        public bool StringSet(string cacheKey, object cacheValue, int expireSeconds = -1, RedisExistence? exists = null)
        {
            return RedisHelper.Set(cacheKey, cacheValue, expireSeconds, exists);
        }

        public async Task<bool> StringSetAsync(string cacheKey, object cacheValue, int expireSeconds = -1, RedisExistence? exists = null)
        {
            return await RedisHelper.SetAsync(cacheKey, cacheValue, expireSeconds, exists);
        }

        public string StringGet(string cacheKey)
        {
            return RedisHelper.Get(cacheKey);

        }

        public async Task<string> StringGetAsync(string cacheKey)
        {
            return await RedisHelper.GetAsync(cacheKey);
        }

        public long StringLen(string cacheKey)
        {
            return RedisHelper.StrLen(cacheKey);
        }

        public async Task<long> StringLenAsync(string cacheKey)
        {
            return await RedisHelper.StrLenAsync(cacheKey);
        }

        public long StringSetRange(string cacheKey, uint offest, string value)
        {
            return RedisHelper.SetRange(cacheKey, offest, value);
        }

        public async Task<long> StringSetRangeAsync(string cacheKey, uint offest, string value)
        {
            return await RedisHelper.SetRangeAsync(cacheKey, offest, value);
        }

        public string StringGetRange(string cacheKey, long start, long end)
        {
            return RedisHelper.GetRange(cacheKey, start, end);
        }

        public async Task<string> StringGetRangeAsync(string cacheKey, long start, long end)
        {
            return await RedisHelper.GetRangeAsync(cacheKey, start, end);
        }
        #endregion

        #region Hashes
        public bool HMSet(string cacheKey, Dictionary<string, string> vals, TimeSpan? expiration = null)
        {
            return RedisHelper.HMSet(cacheKey, vals, expiration);
        }

        public bool HSet(string cacheKey, string field, string cacheValue)
        {
            return RedisHelper.HSet(cacheKey, field, cacheValue);
        }

        public bool HExists(string cacheKey, string field)
        {
            return RedisHelper.HExists(cacheKey, field);
        }

        public long HDel(string cacheKey, string[] fields = null)
        {
            return RedisHelper.HDel(cacheKey, fields);

        }

        public string HGet(string cacheKey, string field)
        {
            return RedisHelper.HGet(cacheKey, field);
        }

        public Dictionary<string, string> HGetAll(string cacheKey)
        {
            return RedisHelper.HGetAll(cacheKey);
        }

        public long HIncrBy(string cacheKey, string field, long val = 1)
        {
            return RedisHelper.HIncrBy(cacheKey, field, val);
        }

        public string[] HKeys(string cacheKey)
        {
            return RedisHelper.HKeys(cacheKey);
        }

        public long HLen(string cacheKey)
        {
            return RedisHelper.HLen(cacheKey);
        }

        public string[] HVals(string cacheKey)
        {
            return RedisHelper.HVals(cacheKey);
        }

        public string[] HMGet(string cacheKey, string[] fields)
        {
            return RedisHelper.HMGet(cacheKey, fields);
        }

        public async Task<bool> HMSetAsync(string cacheKey, object[] keyValues)
        {
            return await RedisHelper.HMSetAsync(cacheKey, keyValues);
        }

        public async Task<bool> HSetAsync(string cacheKey, string field, string cacheValue)
        {
            return await RedisHelper.HSetAsync(cacheKey, field, cacheValue);
        }

        public Task<bool> HExistsAsync(string cacheKey, string field)
        {
            return RedisHelper.HExistsAsync(cacheKey, field);
        }

        public async Task<long> HDelAsync(string cacheKey, string[] fields)
        {
            return await RedisHelper.HDelAsync(cacheKey, fields);
        }

        public async Task<string> HGetAsync(string cacheKey, string field)
        {
            return await RedisHelper.HGetAsync(cacheKey, field);
        }

        public async Task<Dictionary<string, string>> HGetAllAsync(string cacheKey)
        {
            return await RedisHelper.HGetAllAsync(cacheKey);
        }

        public async Task<long> HIncrByAsync(string cacheKey, string field, long val = 1)
        {
            return await RedisHelper.HIncrByAsync(cacheKey, field, val);
        }

        public async Task<string[]> HKeysAsync(string cacheKey)
        {
            return await RedisHelper.HKeysAsync(cacheKey);
        }

        public async Task<long> HLenAsync(string cacheKey)
        {
            return await RedisHelper.HLenAsync(cacheKey);
        }

        public async Task<string[]> HValsAsync(string cacheKey)
        {
            return await RedisHelper.HValsAsync(cacheKey);
        }

        public async Task<string[]> HMGetAsync(string cacheKey, string[] fields)
        {
            return await RedisHelper.HMGetAsync(cacheKey, fields);
        }
        #endregion

        #region List
        public T LIndex<T>(string cacheKey, long index)
        {
            return RedisHelper.LIndex<T>(cacheKey, index);
        }

        public long LLen(string cacheKey)
        {
            return RedisHelper.LLen(cacheKey);
        }

        public T LPop<T>(string cacheKey)
        {
            return RedisHelper.LPop<T>(cacheKey);
        }

        public long LPush<T>(string cacheKey, T[] cacheValues)
        {
            return RedisHelper.LPush<T>(cacheKey, cacheValues);
        }

        public T[] LRange<T>(string cacheKey, long start, long stop)
        {
            return RedisHelper.LRange<T>(cacheKey, start, stop);
        }

        public long LRem(string cacheKey, long count, object cacheValue)
        {
            return RedisHelper.LRem(cacheKey, count, cacheValue);
        }

        public bool LSet(string cacheKey, long index, object cacheValue)
        {
            return RedisHelper.LSet(cacheKey, index, cacheValue);
        }

        public bool LTrim(string cacheKey, long start, long stop)
        {
            return RedisHelper.LTrim(cacheKey, start, stop);
        }

        public long LPushX(string cacheKey, object cacheValue)
        {
            return RedisHelper.LPushX(cacheKey, cacheValue);
        }

        public long LInsertBefore(string cacheKey, object pivot, object cacheValue)
        {
            return RedisHelper.LInsertBefore(cacheKey, pivot, cacheValue);
        }

        public long LInsertAfter(string cacheKey, object pivot, object cacheValue)
        {
            return RedisHelper.LInsertAfter(cacheKey, pivot, cacheValue);
        }

        public long RPushX(string cacheKey, object cacheValue)
        {
            return RedisHelper.RPushX(cacheKey, cacheValue);
        }

        public long RPush<T>(string cacheKey, T[] cacheValues)
        {
            return RedisHelper.RPush<T>(cacheKey, cacheValues);
        }

        public T RPop<T>(string cacheKey)
        {
            return RedisHelper.RPop<T>(cacheKey);
        }

        public async Task<T> LIndexAsync<T>(string cacheKey, long index)
        {
            return await RedisHelper.LIndexAsync<T>(cacheKey, index);
        }

        public async Task<long> LLenAsync(string cacheKey)
        {
            return await RedisHelper.LLenAsync(cacheKey);
        }

        public async Task<T> LPopAsync<T>(string cacheKey)
        {
            return await RedisHelper.LPopAsync<T>(cacheKey);
        }

        public async Task<long> LPushAsync<T>(string cacheKey, T[] cacheValues)
        {
            return await RedisHelper.LPushAsync<T>(cacheKey, cacheValues);
        }

        public async Task<T[]> LRangeAsync<T>(string cacheKey, long start, long stop)
        {
            return await RedisHelper.LRangeAsync<T>(cacheKey, start, stop);
        }

        public async Task<long> LRemAsync(string cacheKey, long count, object cacheValue)
        {
            return await RedisHelper.LRemAsync(cacheKey, count, cacheValue);
        }

        public async Task<bool> LSetAsync(string cacheKey, long index, object cacheValue)
        {
            return await RedisHelper.LSetAsync(cacheKey, index, cacheValue);
        }

        public async Task<bool> LTrimAsync(string cacheKey, long start, long stop)
        {
            return await RedisHelper.LTrimAsync(cacheKey, start, stop);
        }

        public async Task<long> LPushXAsync(string cacheKey, object cacheValue)
        {
            return await RedisHelper.LPushXAsync(cacheKey, cacheValue);
        }

        public async Task<long> LInsertBeforeAsync(string cacheKey, object pivot, object cacheValue)
        {
            return await RedisHelper.LInsertBeforeAsync(cacheKey, pivot, cacheValue);
        }

        public async Task<long> LInsertAfterAsync(string cacheKey, object pivot, object cacheValue)
        {
            return await RedisHelper.LInsertAfterAsync(cacheKey, pivot, cacheValue);
        }

        public async Task<long> RPushXAsync(string cacheKey, object cacheValue)
        {
            return await RedisHelper.RPushXAsync(cacheKey, cacheValue);
        }

        public async Task<long> RPushAsync<T>(string cacheKey, T[] cacheValues)
        {
            return await RedisHelper.RPushAsync<T>(cacheKey, cacheValues);
        }

        public async Task<T> RPopAsync<T>(string cacheKey)
        {
            return await RedisHelper.RPopAsync<T>(cacheKey);
        }
        #endregion

        #region Set

        public long SAdd<T>(string cacheKey, T[] cacheValues)
        {
            return RedisHelper.SAdd<T>(cacheKey, cacheValues);
        }

        public long SCard(string cacheKey)
        {
            return RedisHelper.SCard(cacheKey);
        }

        public bool SIsMember(string cacheKey, object cacheValue)
        {
            return RedisHelper.SIsMember(cacheKey, cacheValue);
        }

        public string[] SMembers(string cacheKey)
        {
            return RedisHelper.SMembers(cacheKey);
        }

        public T SPop<T>(string cacheKey)
        {
            return RedisHelper.SPop<T>(cacheKey);
        }

        public T SRandMember<T>(string cacheKey)
        {
            return RedisHelper.SRandMember<T>(cacheKey);
        }

        public long SRem<T>(string cacheKey, T[] cacheValues)
        {
            return RedisHelper.SRem<T>(cacheKey, cacheValues);
        }

        public async Task<long> SAddAsync<T>(string cacheKey,T[] cacheValues)
        {
            return await RedisHelper.SAddAsync<T>(cacheKey, cacheValues);
        }

        public async Task<long> SCardAsync(string cacheKey)
        {
            return await RedisHelper.SCardAsync(cacheKey);
        }

        public async Task<bool> SIsMemberAsync(string cacheKey, object cacheValue)
        {
            return await RedisHelper.SIsMemberAsync(cacheKey, cacheValue);
        }

        public async Task<string[]> SMembersAsync(string cacheKey)
        {
            return await RedisHelper.SMembersAsync(cacheKey);
        }

        public async Task<T> SPopAsync<T>(string cacheKey)
        {
            return await RedisHelper.SPopAsync<T>(cacheKey);
        }

        public async Task<T> SRandMemberAsync<T>(string cacheKey)
        {
            return await RedisHelper.SRandMemberAsync<T>(cacheKey);
        }

        public async Task<long> SRemAsync<T>(string cacheKey, T[] cacheValues)
        {
            return await RedisHelper.SRemAsync<T>(cacheKey, cacheValues);
        }
        #endregion

        #region Sorted Set
        public long ZAdd(string cacheKey, (decimal, object)[] cacheValues)
        {
            return RedisHelper.ZAdd(cacheKey, cacheValues);  
        }

        public long ZCard(string cacheKey)
        {
            return RedisHelper.ZCard(cacheKey);
        }

        public long ZCount(string cacheKey, decimal min, decimal max)
        {
            return RedisHelper.ZCount(cacheKey, min, max);
        }

        public decimal ZIncrBy(string cacheKey, string field, decimal increment = 1m)
        {
            return RedisHelper.ZIncrBy(cacheKey, field, increment);
        }

        public long ZLexCount(string cacheKey, string min, string max)
        {
            return RedisHelper.ZLexCount(cacheKey, min, max);
        }

        public T[] ZRange<T>(string cacheKey, long start, long stop)
        {
            return RedisHelper.ZRange<T>(cacheKey, start, stop);
        }

        public long? ZRank(string cacheKey, object cacheValue)
        {
            return RedisHelper.ZRank(cacheKey, cacheValue);
        }

        public long ZRem<T>(string cacheKey, T[] cacheValues)
        {
            return RedisHelper.ZRem(cacheKey, cacheValues);
        }

        public decimal? ZScore(string cacheKey, object cacheValue)
        {
            return RedisHelper.ZScore(cacheKey, cacheValue);
        }

        public async Task<long> ZAddAsync(string cacheKey, params (decimal, object)[] cacheValues)
        {
            return await RedisHelper.ZAddAsync(cacheKey, cacheValues);
        }

        public async Task<long> ZCardAsync(string cacheKey)
        {
            return await RedisHelper.ZCardAsync(cacheKey);
        }

        public async Task<long> ZCountAsync(string cacheKey, decimal min, decimal max)
        {
            return await RedisHelper.ZCountAsync(cacheKey, min, max);
        }

        public async Task<decimal> ZIncrByAsync(string cacheKey, string field, decimal val = 1m)
        {
            return await RedisHelper.ZIncrByAsync(cacheKey, field, val);
        }

        public async Task<long> ZLexCountAsync(string cacheKey, string min, string max)
        {
            return await RedisHelper.ZLexCountAsync(cacheKey, min, max);
        }

        public async Task<T[]> ZRangeAsync<T>(string cacheKey, long start, long stop)
        {
            return await RedisHelper.ZRangeAsync<T>(cacheKey, start, stop);
        }

        public async Task<long?> ZRankAsync(string cacheKey, object cacheValue)
        {
            return await RedisHelper.ZRankAsync(cacheKey, cacheValue);
        }

        public async Task<long> ZRemAsync<T>(string cacheKey, T[] cacheValues)
        {
            return await RedisHelper.ZRemAsync<T>(cacheKey, cacheValues);
        }

        public async Task<decimal?> ZScoreAsync<T>(string cacheKey, object cacheValue)
        {
            return await RedisHelper.ZScoreAsync(cacheKey, cacheValue);

        }
        #endregion

        public async Task<dynamic> EvalAsync(string script, string keys, object[] args)
        {
            return await RedisHelper.EvalAsync(script, keys, args);
        }

    }
}

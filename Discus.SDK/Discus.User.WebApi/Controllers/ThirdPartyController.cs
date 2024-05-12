using Discus.SDK.IdGenerater.IdGeneraterFactory;

namespace Discus.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThirdPartyController : ControllerBase
    {
        private readonly IRedisClient _redisClient;

        private readonly IThirdPartyService _thirdPartyService;

       public ThirdPartyController(IRedisClient redisClient, IThirdPartyService thirdPartyService)
        {
            _redisClient = redisClient;
            _thirdPartyService = thirdPartyService;
        }

        /// <summary>
        /// RedisStringSet
        /// </summary>
        /// <returns></returns>
        [Route("RedisStringSet"), HttpGet]
        public bool RedisStringSet()
        {
            var result = _redisClient.StringSet("xusc", "159272409542");
            return result;
        }

        /// <summary>
        /// 测试Id生成器
        /// </summary>
        /// <returns></returns>
        [Route("GetIdGenerater"), HttpGet]
        public long GetIdGenerater()
        {
            return IdGenerater.GetNextId();
        }
    }
}

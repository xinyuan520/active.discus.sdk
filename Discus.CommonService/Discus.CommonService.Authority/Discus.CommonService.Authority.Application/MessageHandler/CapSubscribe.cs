using Discus.CommonService.Authority.Repository.Entities;
using DotNetCore.CAP;
using Discus.SDK.RabbitMQ.Consts;

namespace Discus.CommonService.Authority.Application.MessageHandler
{
    public class CapSubscribe : ICapSubscribe
    {
        private readonly IUserInfoService _userInfoService;

        public CapSubscribe(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [CapSubscribe(EventNameConsts.DiscusUser)]

        public async Task Test(UserInfo userInfo)
        {
            var userinfo = await _userInfoService.GetById(userInfo.Id);
            Console.WriteLine(userInfo.Nickname);
        }
    }
}

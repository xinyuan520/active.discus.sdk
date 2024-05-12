using Discus.SDK.RabbitMQ.Consts;
using Discus.User.Repository.Entities;
using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.MessageHandler
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

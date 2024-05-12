using Discus.User.Application.Contracts.Dtos;
using Discus.User.Repository.Entities;
using Discus.User.Repository.SqliteEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.AutoMapper
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserInfo, UserInfoDto>();
            CreateMap<UserInfoDto, UserInfo>();

            CreateMap<UserInfo, UserInfoRequestDto>();
            CreateMap<UserInfoRequestDto, UserInfo>();

            CreateMap<SettingModelDto, SettingModel>();
        }
    }
}

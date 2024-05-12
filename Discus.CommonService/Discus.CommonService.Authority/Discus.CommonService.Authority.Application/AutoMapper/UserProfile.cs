using AutoMapper;
using Discus.CommonService.Authority.Application.Contracts.Dtos;
using Discus.CommonService.Authority.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.CommonService.Authority.Application.AutoMapper
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserInfo, UserInfoDto>();
            CreateMap<UserInfoDto, UserInfo>();

            //CreateMap<UserInfo, UserInfoRequestDto>();
            //CreateMap<UserInfoRequestDto, UserInfo>();

            //CreateMap<SettingModelDto, SettingModel>();
        }
    }
}

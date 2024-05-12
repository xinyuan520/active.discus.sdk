using AutoMapper;
using Discus.CommonService.Authority.Application.Contracts.Dtos;
using Discus.CommonService.Authority.Repository.Entities;
using Discus.SDK.Repository.SqlSugar.Repository;

namespace Discus.CommonService.Authority.Application.Services
{
    public class UserInfoService : BasicService, IUserInfoService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<UserInfo> _userinfoRepository;

        public UserInfoService(IMapper mapper, IBaseRepository<UserInfo> userinfoRepository)
        {
            _mapper = mapper;
            _userinfoRepository = userinfoRepository;
        }

        public string ShowUser()
        {
            return "ShowUser";
        }

        public async Task<List<UserInfoDto>> GetAll()
        {
            var userInfoList = await _userinfoRepository.GetAllAsync();
            return _mapper.Map<List<UserInfoDto>>(userInfoList);
        }

        public async Task<UserInfoDto> GetById(long Id)
        {
            var userInfo = await _userinfoRepository.GetByIdAsync(Id);
            return _mapper.Map<UserInfoDto>(userInfo);
        }
    }
}

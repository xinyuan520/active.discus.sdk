using Castle.Core.Resource;
using Discus.SDK.IdGenerater.IdGeneraterFactory;
using Discus.SDK.Repository.SqlSugar.Repository;
using Discus.SDK.Tools.HashSecurity;
using Discus.SDK.Tools.HashSecurity.Extension;
using Discus.User.Application.Contracts.Dtos;
using Discus.User.Repository.Entities;
using SqlSugar;
using System.Diagnostics;

namespace Discus.User.Application.Services
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

        public async Task<ApiResult> Create(UserInfoRequestDto request)
        {
            var userinfo = _mapper.Map<UserInfo>(request);
            userinfo.Id = IdGenerater.GetNextId();
            int count = await _userinfoRepository.AddAsync(userinfo);
            return count > 0 ? ApiResult.IsSuccess("新增成功！") : ApiResult.IsFailed("新增失败！");
        }

        public async Task<ApiResult> Update(UserInfoRequestDto request)
        {
            var userInfo = _mapper.Map<UserInfo>(request);
            int count = await _userinfoRepository.UpdateIgnoreAsync(userInfo, UpdatingProps<UserInfo>(u => u.Password));
            return count > 0 ? ApiResult.IsSuccess("更新成功！") : ApiResult.IsFailed("更新失败！");
        }

        public async Task<ApiResult> Delete(long id)
        {
            var userInfo = await _userinfoRepository.GetByIdAsync(id);
            if (userInfo == null)
                return ApiResult.IsFailed("无效用户信息，删除失败！");
            userInfo.IsDeleted = true;
            int count = await _userinfoRepository.UpdateContainAsync(userInfo, UpdatingProps<UserInfo>(u => u.IsDeleted));
            return count > 0 ? ApiResult.IsSuccess("删除成功！") : ApiResult.IsFailed("删除失败！");
        }

        public async Task<UserInfoDto> GetById(long Id)
        {
            var userInfo = await _userinfoRepository.GetByIdAsync(Id);
            return _mapper.Map<UserInfoDto>(userInfo);
        }
        public async Task<List<UserInfoDto>> GetAll()
        {
            var userInfoList = await _userinfoRepository.GetAllAsync();
            return _mapper.Map<List<UserInfoDto>>(userInfoList);
        }
    }
}

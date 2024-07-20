using GIS_VETERINARY.Abstractions.IApplication;
using GIS_VETERINARY.Abstractions.IServices;
using GIS_VETERINARY.DTOs.Auth;
using GIS_VETERINARY.DTOs.Common;
using GIS_VETERINARY.DTOs.User;

namespace GIS_VETERINARY.Application.User
{
    public class UserApplication : IUserApplication
    {
        private IUserService _userService;
        public UserApplication(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<int>> Create(UserCreateRequestDto request)
        {
            return await _userService.Create(request);
        }

        public async Task<ResultDto<int>> Delete(DeleteDto request)
        {
           return await _userService.Delete(request);
        }

       

        public async Task<ResultDto<UserListResponseDTO>> GetAll()
        {
            return await _userService.GetAll();
        }

        public async Task<AuthResponseDto> Login(LoginRequestDto request)
        {
           return await _userService.Login(request);
        }
    }
}

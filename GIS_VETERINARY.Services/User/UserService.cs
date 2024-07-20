using GIS_VETERINARY.Abstractions.IRepository;
using GIS_VETERINARY.Abstractions.IServices;
using GIS_VETERINARY.DTOs.Auth;
using GIS_VETERINARY.DTOs.Common;
using GIS_VETERINARY.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_VETERINARY.Services.User
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ResultDto<int>> Create(UserCreateRequestDto request)
        {
            return await userRepository.Create(request);
        }

        public async Task<ResultDto<int>> Delete(DeleteDto request)
        {
            return await userRepository.Delete(request);
        }

        public async Task<ResultDto<UserListResponseDTO>> GetAll()
        {
            return await userRepository.GetAll();
        }

        public async Task<AuthResponseDto> Login(LoginRequestDto request)
        {
           return await userRepository.Login(request);
        }
    }
}

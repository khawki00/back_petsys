
using GIS_VETERINARY.DTOs.Auth;
using GIS_VETERINARY.DTOs.Common;
using GIS_VETERINARY.DTOs.User;

namespace GIS_VETERINARY.Abstractions.IRepository
{
    public interface IUserRepository
    {
        public Task<ResultDto<UserListResponseDTO>> GetAll();

        public Task<ResultDto<int>> Create(UserCreateRequestDto request);

        public Task<ResultDto<int>> Delete(DeleteDto request);

        public Task<TokenResponseDto> GenerateToken(UserDetailResponseDto request);
        public Task<UserDetailResponseDto> GetUserByUsername(string username);
        public Task<UserDetailResponseDto> ValidateUser(LoginRequestDto request);

        public Task<AuthResponseDto> Login(LoginRequestDto request);
        

        
       
    }
}

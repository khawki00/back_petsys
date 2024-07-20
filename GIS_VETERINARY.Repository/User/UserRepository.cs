using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using GIS_VETERINARY.Abstractions.IRepository;
using GIS_VETERINARY.DTOs.Common;
using GIS_VETERINARY.DTOs.User;
using Dapper;
using Azure.Core;
using GIS_VETERINARY.DTOs.Auth;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace GIS_VETERINARY.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;
        private string _connectionString = "";
        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("Connection");
        }


        public async Task<ResultDto<int>> Create(UserCreateRequestDto request)
        {
            ResultDto<int> res = new ResultDto<int>();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_id", request.id);
                    parameters.Add("@p_username", request.username);
                    parameters.Add("@p_password", request.password);
                    parameters.Add("@p_role_id", request.role_id);

                    using (var reader = await cn.ExecuteReaderAsync("SP_CREATE_USER", parameters, commandType: System.Data.CommandType.StoredProcedure))
                    {
                        while(reader.Read()) 
                        {
                            res.Item = Convert.ToInt32(reader["id"].ToString());
                            res.IsSuccess = Convert.ToInt32(reader["id"].ToString()) > 0 ? true: false;
                            res.Message = Convert.ToInt32(reader["id"].ToString()) > 0 ? "Information saved successfully" : "Infomration was not saved";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess=false;
                res.MessageException = ex.Message;
            }
            return res;
        }

        public async Task<ResultDto<int>> Delete(DeleteDto request)
        {
            ResultDto<int> res = new ResultDto<int>();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_id", request.id);
                    using (var reader = await cn.ExecuteReaderAsync("SP_DELETE_USER", parameters, commandType: System.Data.CommandType.StoredProcedure))
                    {
                        while (reader.Read())
                        {
                            res.Item = Convert.ToInt32(reader["id"].ToString());
                            res.IsSuccess = Convert.ToInt32(reader["id"].ToString()) > 0 ? true : false;
                            res.Message = Convert.ToInt32(reader["id"].ToString()) > 0 ? "User removed successfully" : "User could not be removed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                
            }
            return res;
        }

        public async Task<TokenResponseDto> GenerateToken(UserDetailResponseDto request)
        {
            var key = configuration.GetSection("JWTSettings:Key").Value;
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier,request.username));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier,request.role_id.ToString()));

            var credentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = credentials,
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();   
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(tokenConfig);
            return new TokenResponseDto {  Token = token }; 
        }

        public async Task<ResultDto<UserListResponseDTO>> GetAll()
        {
           ResultDto<UserListResponseDTO> res = new ResultDto<UserListResponseDTO>();
           List<UserListResponseDTO> list = new List<UserListResponseDTO>();
            try
            {
                using(var cn = new SqlConnection(_connectionString))
                {
                    list = (List<UserListResponseDTO>) await cn.QueryAsync<UserListResponseDTO>("SP_LIST_USERS", null, commandType: System.Data.CommandType.StoredProcedure);
                }
                res.IsSuccess = list.Count > 0 ? true : false;
                res.Message = list.Count > 0 ? "Information Found" : "No Information Found";
                res.Data = list.ToList();
            }
            catch (Exception ex)
            {

               res.IsSuccess = false;
               res.MessageException= ex.Message;
            }
           return res;

        }

        public async Task<UserDetailResponseDto> GetUserByUsername(string username)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@p_username", username);
            using(var cn = new SqlConnection(_connectionString))
            {
                var query = await cn.QueryAsync<UserDetailResponseDto>("SP_GET_USER_BY_USERNAME", parameters, commandType: System.Data.CommandType.StoredProcedure);
                if (query.Any())
                {
                    return query.First();
                }
                throw new Exception("Invalid Username or Password");
            }
        }

        public async Task<UserDetailResponseDto> ValidateUser(LoginRequestDto request)
        {
          UserDetailResponseDto user = await GetUserByUsername(request.username);
            if (user.password == request.password)
            {
                return user;
            }
            throw new Exception("Invalid Username or Password");
        }

        public async Task<AuthResponseDto> Login(LoginRequestDto request)
        {
            UserDetailResponseDto user = await ValidateUser(request);
            var token = await GenerateToken(user);
            return new AuthResponseDto {IsSuccess = true, User = user, Token = token.Token };
        }
    }
}

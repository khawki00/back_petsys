using GIS_VETERINARY.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_VETERINARY.DTOs.Auth
{
    public class AuthResponseDto
    {
        public Boolean IsSuccess { get; set; }  
        public UserDetailResponseDto User { get; set; }
        public string Token { get; set; }
    }

    public class TokenResponseDto
    {
        public string Token { get; set; }
    }
}

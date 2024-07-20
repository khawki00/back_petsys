using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GIS_VETERINARY.DTOs.User
{
    public class UserCreateRequestDto
    {
        public int id {get;set;}
        public string username { get;set;}
        public string password { get; set; }
        public int role_id { get; set; }

    }
}

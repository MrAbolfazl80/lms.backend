using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Users
{
    public class GetUsersRequestDto
    {
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}

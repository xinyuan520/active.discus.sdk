using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.CommonService.Authority.Application.Contracts.Dtos
{
    public class UserInfoDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Nickname { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int sex { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
    }
}

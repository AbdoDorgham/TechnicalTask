using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTask.BusinessLogic.DTOs.AccountDtos
{
    public class LoginUserDto 
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }

    }
}

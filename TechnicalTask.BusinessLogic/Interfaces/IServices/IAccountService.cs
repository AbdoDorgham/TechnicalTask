using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.DTOs.AccountDtos;
using TechnicalTask.BusinessLogic.DTOs.CustomerDtos;
using TechnicalTask.BusinessLogic.Entities.General;
using TechnicalTask.BusinessLogic.Utils;

namespace TechnicalTask.BusinessLogic.Interfaces.IServices
{
    public interface IAccountService
    {
        public Task<Result<string>> Login(LoginUserDto loginUserDTO);

        public Task<ApplicationUser> RegisterUser(RegisterUserDto registerUserDTO);
        public Task<Result<string>> RegisterCustomer(RegisterCustomerDto registerCustomerDTO);

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnicalTask.BusinessLogic.DTOs.AccountDtos;
using TechnicalTask.BusinessLogic.DTOs.CustomerDtos;
using TechnicalTask.BusinessLogic.Interfaces.IServices;

namespace TechnicalTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUser)
        {
            var result = await accountService.Login(loginUser);
            return result.IsFail ? Unauthorized(result) : Ok(result);

        }

        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerDto registerCustomerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var result = await accountService.RegisterCustomer(registerCustomerDto);
            return result.IsFail ? BadRequest(result) : Ok(result);

        }



    }
}

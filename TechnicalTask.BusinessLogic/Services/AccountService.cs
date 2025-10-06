using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.DTOs.AccountDtos;
using TechnicalTask.BusinessLogic.DTOs.CustomerDtos;
using TechnicalTask.BusinessLogic.Entities.Buiseness;
using TechnicalTask.BusinessLogic.Entities.General;
using TechnicalTask.BusinessLogic.Interfaces.IServices;
using TechnicalTask.BusinessLogic.Interfaces.IUnitOfWorks;
using TechnicalTask.BusinessLogic.Utils;

namespace TechnicalTask.BusinessLogic.Services
{
    public class AccountService :IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;


        public AccountService(
            UserManager<ApplicationUser> userManager,
            IMapper mapper , IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }



        public async Task<Result<string>> Login(LoginUserDto loginUserDTO)
        {
            var result = new Result<string>(isFail:false);
            try
            {
                ApplicationUser user = await userManager.FindByEmailAsync(loginUserDTO.UserNameOrEmail);
                user ??= await userManager.FindByNameAsync(loginUserDTO.UserNameOrEmail);
                if (user is null)
                    return result.Fail("Invalid User.");
                bool isValidUser = await userManager.CheckPasswordAsync(user, loginUserDTO.Password);
                if (!isValidUser)
                    return result.Fail("Wrong Password.");
               
                return result.Success("Success Login", MyTokenHandler.GenerateToken(user));
            }
            catch (Exception ex )
            {
                return result.Fail(ex.Message);
            }

          

        }

        public async Task<Result<ApplicationUser>> RegisterUser(RegisterUserDto registerUserDTO)
        {
            var result = new Result<ApplicationUser>(isFail: false);
            try
            {

                if (registerUserDTO.UserName == null)
                {
                    registerUserDTO.UserName = registerUserDTO.Email.Split('@').FirstOrDefault();
                }
                IdentityResult registerResult = await userManager.CreateAsync(mapper.Map<ApplicationUser>(registerUserDTO), registerUserDTO.Password);
                if (!registerResult.Succeeded)
                {
                    var strErrors = new StringBuilder();
                    foreach (var error in registerResult.Errors)
                    {
                        strErrors = strErrors.AppendLine($"{error.Description}");
                    }
                    return result.Fail(strErrors.ToString());
                }
                return result.Success(message:"User Created Successfully." , await userManager.FindByNameAsync(registerUserDTO.UserName));

            }
            catch (Exception ex)
            {
                return result.Fail(ex.Message);
            }
            
        }


        public async Task<Result<string>> RegisterCustomer(RegisterCustomerDto registerCustomerDTO)
        {
            var result = new Result<string>(isFail: false);

            try
            {
                var registerUserResult = await RegisterUser(registerCustomerDTO);
                if (registerUserResult.IsFail)
                    return result.Fail(registerUserResult.Message);
                var user = registerUserResult.ReturnedObj;
                Customer customer = mapper.Map<Customer>(registerCustomerDTO);
                customer.Id = user.Id;
                unitOfWork.CustomerRepo.Add(customer);
                await unitOfWork.SaveChangesAsync();
                return result.Success("Customer Registered Succefully");
               
            }
            catch (Exception ex)
            {
                return result.Fail(ex.Message);

            }
        }

    }
}

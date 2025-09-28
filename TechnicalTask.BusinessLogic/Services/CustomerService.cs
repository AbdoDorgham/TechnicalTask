using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.DTOs.CustomerDtos;
using TechnicalTask.BusinessLogic.Interfaces.IServices;
using TechnicalTask.BusinessLogic.Interfaces.IUnitOfWorks;
using TechnicalTask.BusinessLogic.Utils;

namespace TechnicalTask.BusinessLogic.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

        }
        public Result<DisplayCustomerDto> GetCustomerById(int id)
        {
            var result = new Result<DisplayCustomerDto>(isFail: false);
            try
            {
                var customer = unitOfWork.CustomerRepo.GetById(id);
                if (customer == null)
                    return result.Fail("Customer not found");
                var customerDto = mapper.Map<DisplayCustomerDto>(customer);
                return result.Success(returnedObj: customerDto);
            }
            catch (Exception ex)
            {

                return result.Fail(ex.Message);
            }
        }

        public Result<UpdateCustomerDto> Update(UpdateCustomerDto customerDto)
        {
            var result = new Result<UpdateCustomerDto>(isFail: false);
            try
            {
                var customer = unitOfWork.CustomerRepo.GetById(customerDto.Id);
                if (customer == null)
                    return result.Fail("Customer not found");
                mapper.Map(customerDto, customer);
                unitOfWork.CustomerRepo.Update(customer);
                unitOfWork.SaveChangesAsync();
                return result.Success("Customer updated successfully", customerDto);
            }
            catch (Exception ex)
            {

                return result.Fail(ex.Message);
            }

        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.DTOs.CustomerDtos;
using TechnicalTask.BusinessLogic.DTOs.OrderDtos;
using TechnicalTask.BusinessLogic.Entities.Buiseness;
using TechnicalTask.BusinessLogic.Interfaces.IServices;
using TechnicalTask.BusinessLogic.Interfaces.IUnitOfWorks;
using TechnicalTask.BusinessLogic.Utils;

namespace TechnicalTask.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICustomerService customerService;


        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ICustomerService customerService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.customerService = customerService;

        }

        public Result<IEnumerable<DisplayOrderDto>> GetAll()
        {
            var result = new Result<IEnumerable<DisplayOrderDto>>(isFail: false);
            try
            {
                var orders = unitOfWork.OrderRepo.GetAll();
                var ordersDto = mapper.Map<IEnumerable<DisplayOrderDto>>(orders);
                return result.Success(returnedObj: ordersDto);

            }
            catch (Exception ex)
            {
                return result.Fail(ex.Message);
            }


        }

        public Result<DisplayOrderDto> GetById(int id)
        {
            var result = new Result<DisplayOrderDto>(isFail: false);
            try
            {
                var order = unitOfWork.OrderRepo.GetById(id);
                if (order == null)
                    return result.Fail("Order not found");
                var orderDto = mapper.Map<DisplayOrderDto>(order);
                return result.Success(returnedObj: orderDto);
            }
            catch (Exception ex)
            {

                return result.Fail(ex.Message);

            }

        }

        public async Task<Result<CreateOrderDto>> Add(CreateOrderDto orderDto)
        {
            var result = new Result<CreateOrderDto>(isFail: false);
            try
            {
                var customer = customerService.GetCustomerById(orderDto.CustomerId.Value).ReturnedObj;
                if (customer is null)
                    return result.Fail("Customer not found");
                if (customer.BannedUntil > DateTime.Now)
                    return result.Fail($"Sorry {customer.UserName} , You`re Banned From Make Any Orders until {customer.BannedUntil}");
                var order = mapper.Map<Order>(orderDto);
                unitOfWork.OrderRepo.Add(order);
                await unitOfWork.SaveChangesAsync();
                var newOrderDto = mapper.Map<CreateOrderDto>(order);
                return result.Success("Order added successfully", newOrderDto);
            }
            catch (Exception ex)
            {

                return result.Fail(ex.Message);

            }
        }

        public Result<IEnumerable<DisplayOrderDto>> GetOrdersByCustomerId(int customerId)
        {
            var result = new Result<IEnumerable<DisplayOrderDto>>(isFail: false);
            try
            {
                var orders = unitOfWork.OrderRepo.GetAll().Where(o => o.CustomerId == customerId);
                var ordersDto = mapper.Map<IEnumerable<DisplayOrderDto>>(orders);
                return result.Success(returnedObj: ordersDto);

            }
            catch (Exception ex)
            {
                return result.Fail(ex.Message);
            }

        }

        public async Task<Result<string>> Delete(int id, int customerId)
        {
            var result = new Result<string>(isFail: false);
            try
            {

                var customer = customerService.GetCustomerById(customerId).ReturnedObj;
                if (customer is null)
                    return result.Fail("Customer not found");
                var sameDayDeletions = unitOfWork.OrderRepo.GetAllDeleted().Where(

                    o => o.CustomerId == customerId &&
                    o.CreatedAt.Value.Day == o.DeletedAt.Value.Day && o.DeletedAt.Value.Day == DateTime.Now.Date.Day).ToList();

                unitOfWork.OrderRepo.Delete(id);
                await unitOfWork.SaveChangesAsync(); 
                if (sameDayDeletions.Count() >= 2)
                {
                    customer.BannedUntil = DateTime.Now.AddHours(6);
                    customer.BannedCount++;
                    var customerForUpdate = mapper.Map<UpdateCustomerDto>(customer);
                    customerForUpdate.Id = customerId;
                    customerForUpdate = (await customerService.Update(customerForUpdate)).ReturnedObj;
                    if (customerForUpdate is null)
                        return result.Fail("Error In Update Customer");
                }
                return result.Success("Order deleted successfully");


            }
            catch (Exception ex)
            {
                return result.Fail(ex.Message);
            }
        }

    }
}

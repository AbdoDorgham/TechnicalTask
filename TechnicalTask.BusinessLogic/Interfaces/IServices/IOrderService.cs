using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.DTOs.AccountDtos;
using TechnicalTask.BusinessLogic.DTOs.OrderDtos;
using TechnicalTask.BusinessLogic.Entities.General;
using TechnicalTask.BusinessLogic.Utils;

namespace TechnicalTask.BusinessLogic.Interfaces.IServices
{
    public interface IOrderService
    {
        public Result<IEnumerable<DisplayOrderDto>> GetAll();
        public Result<DisplayOrderDto> GetById(int id);
        public Result<CreateOrderDto> Add(CreateOrderDto orderDto);
        public Result<IEnumerable<DisplayOrderDto>> GetOrdersByCustomerId(int customerId);
        public Task<Result<string>> Delete(int id, int customerId);






    }
}

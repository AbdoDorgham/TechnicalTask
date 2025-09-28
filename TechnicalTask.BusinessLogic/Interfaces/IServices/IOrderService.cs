using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.DTOs.AccountDtos;
using TechnicalTask.BusinessLogic.DTOs.OrderDtos;
using TechnicalTask.BusinessLogic.Entities.General;

namespace TechnicalTask.BusinessLogic.Interfaces.IServices
{
    public interface IOrderService
    {
        public Task<IEnumerable<DisplayOrderDto>> GetAll();
        public Task<DisplayOrderDto> GetById();
        public Task<DisplayOrderDto> Add(DisplayOrderDto orderDto);





    }
}

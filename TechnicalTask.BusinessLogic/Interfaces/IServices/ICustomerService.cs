using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.DTOs.CustomerDtos;
using TechnicalTask.BusinessLogic.DTOs.OrderDtos;
using TechnicalTask.BusinessLogic.Utils;

namespace TechnicalTask.BusinessLogic.Interfaces.IServices
{
    public interface ICustomerService
    {
        public Result<DisplayCustomerDto> GetCustomerById(int id);
        public Result<UpdateCustomerDto> Update(UpdateCustomerDto customerDto);

    }
}

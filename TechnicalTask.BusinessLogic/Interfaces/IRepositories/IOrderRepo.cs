using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.Entities.Buiseness;

namespace TechnicalTask.BusinessLogic.Interfaces.IRepositories
{
    public interface IOrderRepo : IBaseRepository<Order>
    {
        public IEnumerable<Order> GetAllDeleted();

    }
}

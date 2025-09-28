using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.Interfaces.IRepositories;

namespace TechnicalTask.BusinessLogic.Interfaces.IUnitOfWorks
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();

        public ICustomerRepo CustomerRepo { get; }
        public IOrderRepo OrderRepo { get; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.Interfaces.IRepositories;
using TechnicalTask.BusinessLogic.Interfaces.IUnitOfWorks;
using TechnicalTask.DataAccess.Data;
using TechnicalTask.DataAccess.Repositories;

namespace TechnicalTask.DataAccess.UnitOfWorks
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDBContext context;

        public UnitOfWork(ApplicationDBContext context)
        {
                this.context = context;
        }
        private readonly ICustomerRepo customerRepo;
        public ICustomerRepo CustomerRepo => customerRepo ?? new CustomerRepo(context);


        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.Entities.Buiseness;
using TechnicalTask.BusinessLogic.Interfaces.IRepositories;
using TechnicalTask.DataAccess.Data;

namespace TechnicalTask.DataAccess.Repositories
{
    public class CustomerRepo : BaseRepository<Customer> , ICustomerRepo
    {
        public CustomerRepo(ApplicationDBContext context) : base(context){ }
        
    }
}

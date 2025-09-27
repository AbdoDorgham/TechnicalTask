using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.Entities.General;

namespace TechnicalTask.BusinessLogic.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);

        T Add(T entity);

        T Update(T entity);

        T Delete(int id);

    }
}

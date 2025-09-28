using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.Entities.General;

namespace TechnicalTask.BusinessLogic.Entities.Buiseness
{
    public class Order : BaseEntity
    {

        public string Title { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        [ForeignKey(nameof(Customer))]
        public int? CusomerId { get; set; }
        public virtual Customer Customer { get; set; }

    }
}

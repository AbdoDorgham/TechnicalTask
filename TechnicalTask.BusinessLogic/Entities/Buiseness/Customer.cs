using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.Entities.General;

namespace TechnicalTask.BusinessLogic.Entities.Buiseness
{
    public class Customer : BaseEntity
    {
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        public int Gender { get; set; }
        public DateTime BannedUntil { get; set; }
        public int BannedCount { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}

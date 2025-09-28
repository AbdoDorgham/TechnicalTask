using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTask.BusinessLogic.DTOs.CustomerDtos
{
    public class UpdateCustomerDto
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        [Range(0, 2)]
        public int Gender { get; set; }

        public DateTime BannedUntil { get; set; }
        public int BannedCount { get; set; }

    }
}

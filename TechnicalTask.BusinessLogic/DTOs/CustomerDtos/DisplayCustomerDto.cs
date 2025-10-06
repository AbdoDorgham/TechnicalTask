using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTask.BusinessLogic.DTOs.CustomerDtos
{
    public class DisplayCustomerDto
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public DateTime BannedUntil { get; set; }
        public int BannedCount { get; set; }
        public string UserName { get; set; }
    }
}

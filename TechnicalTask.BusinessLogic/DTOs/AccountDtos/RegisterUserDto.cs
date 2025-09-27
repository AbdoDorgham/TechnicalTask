using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTask.BusinessLogic.DTOs.AccountDtos
{
    public class RegisterUserDto 
    {
        [MaxLength(40)]
        public string UserName { get; set; }

        [MaxLength(40) ,EmailAddress]
        public string Email { get; set; }
        [MaxLength(11), RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Phone number is not valid")]
        public string PhoneNumber { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }


    }
}

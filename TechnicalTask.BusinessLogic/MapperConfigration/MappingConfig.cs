using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.DTOs.AccountDtos;
using TechnicalTask.BusinessLogic.DTOs.CustomerDtos;
using TechnicalTask.BusinessLogic.Entities.Buiseness;
using TechnicalTask.BusinessLogic.Entities.General;

namespace TechnicalTask.BusinessLogic.MapperConfigration
{
    public class MappingConfig: Profile
    {

        public MappingConfig()
        {
            CreateMap<RegisterUserDto, ApplicationUser>().ReverseMap();
            CreateMap<RegisterCustomerDto, ApplicationUser>().IncludeBase<RegisterUserDto, ApplicationUser>().ReverseMap();
            CreateMap<RegisterCustomerDto, Customer>().ReverseMap();





        }

    }
}

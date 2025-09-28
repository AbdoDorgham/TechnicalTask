using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.DTOs.AccountDtos;
using TechnicalTask.BusinessLogic.DTOs.CustomerDtos;
using TechnicalTask.BusinessLogic.DTOs.OrderDtos;
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
            CreateMap<DisplayOrderDto, Order>().ReverseMap();
            CreateMap<CreateOrderDto, Order>().ReverseMap();
            CreateMap<Customer, DisplayCustomerDto>().AfterMap((src, dist) =>
            {
                dist.Email = src.User?.Email;
              

            }).ReverseMap();
            CreateMap<Customer,UpdateCustomerDto>().ReverseMap();
            CreateMap<DisplayCustomerDto, UpdateCustomerDto>().ReverseMap();





        }

    }
}

using AutoMapper;
using Microservices.Order.Application.Dto;
using Microservices.Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Order.Application.Mapping
{
    public class AddressMap : Profile
    {
        public AddressMap()
        {
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}

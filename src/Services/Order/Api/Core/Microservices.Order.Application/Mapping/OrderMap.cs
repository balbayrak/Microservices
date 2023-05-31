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
    public class OrderMap : Profile
    {
        public OrderMap()
        {
            CreateMap<OrderDto, Domain.Models.Order>().ReverseMap();
            CreateMap<OrderCreateDto, Domain.Models.Order>().ReverseMap();
        }
    }
}

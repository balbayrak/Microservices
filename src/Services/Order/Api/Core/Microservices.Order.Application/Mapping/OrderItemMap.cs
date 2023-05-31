using AutoMapper;
using Microservices.Order.Application.Dto;
using Microservices.Order.Domain.Models;

namespace Microservices.Order.Application.Mapping
{
    public class OrderItemMap : Profile
    {
        public OrderItemMap()
        {
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
        }
    }
}

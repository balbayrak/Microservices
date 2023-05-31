using AutoMapper;
using Microservices.Stock.Application.Dto;
using Microservices.Stock.EventStore;

namespace Microservices.Stock.Application.Mapping
{
    public class StockMap : Profile
    {
        public StockMap()
        {
            CreateMap<CreateStockDto, Domain.Models.Stock>().ReverseMap();
            CreateMap<StockDto, Domain.Models.Stock>().ReverseMap();
            CreateMap<StockDto, StockData>().ReverseMap();
        }
    }
}

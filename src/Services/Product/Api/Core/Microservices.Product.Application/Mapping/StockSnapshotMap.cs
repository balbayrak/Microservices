using AutoMapper;
using Microservices.Product.Application.Dto.StockSnapshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Product.Application.Mapping
{
    public class StockSnapshotMap : Profile
    {
        public StockSnapshotMap()
        {
            CreateMap<Domain.Models.StockSnapshot, StockSnapshotDto>().ReverseMap();
        }
    }
}

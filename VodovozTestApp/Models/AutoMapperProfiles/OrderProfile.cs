using AutoMapper;
using VodovozTestApp.DataAccess;

namespace VodovozTestApp.Models.AutoMapper;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
        this.CreateMap<Order, OrderModel>();
        this.CreateMap<OrderModel, Order>();
    }
}
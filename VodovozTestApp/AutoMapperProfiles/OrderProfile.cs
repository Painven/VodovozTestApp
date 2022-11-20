using AutoMapper;
using VodovozTestApp.DataAccess;
using VodovozTestApp.ViewModels;

namespace VodovozTestApp.Models.AutoMapper;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
        CreateMap<OrderTag, OrderTagModel>()
            .ForMember(x => x.TagID, x => x.MapFrom(o => o.tag_id))
            .ForMember(x => x.Name, x => x.MapFrom(o => o.name));

        CreateMap<OrderTagModel, OrderTag>()
        .ForMember(x => x.tag_id, x => x.MapFrom(o => o.TagID));

        this.CreateMap<Order, OrderModel>()
            .ForMember(x => x.OrderID, x => x.MapFrom(o => o.order_id))
            .ForMember(x => x.EmployeeID, x => x.MapFrom(o => o.employee_id))
            .ForMember(x => x.Tags, x => x.MapFrom(o => o.Tags));

        this.CreateMap<OrderModel, Order>()
            .ForMember(x => x.order_id, x => x.MapFrom(o => o.OrderID))
            .ForMember(x => x.employee_id, x => x.MapFrom(o => o.Manager.EmployeeID))
            .ForMember(x => x.product_name, x => x.MapFrom(o => o.ProductName))
            .ForMember(x => x.Tags, x => x.MapFrom(o => o.Tags));



    }
}
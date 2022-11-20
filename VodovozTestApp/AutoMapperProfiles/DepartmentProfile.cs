using AutoMapper;
using VodovozTestApp.DataAccess;

namespace VodovozTestApp.Models.AutoMapper;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();

        this.CreateMap<Department, DepartmentModel>()
            .ForMember(x => x.DepartmentID, x => x.MapFrom(d => d.department_id))
            .ForMember(x => x.LeadID, x => x.MapFrom(d => d.lead_id));

        this.CreateMap<DepartmentModel, Department>()
            .ForMember(x => x.department_id, x => x.MapFrom(d => d.DepartmentID))
            .ForMember(x => x.lead_id, x => x.MapFrom(d => d.Leader.EmployeeID));
    }
}

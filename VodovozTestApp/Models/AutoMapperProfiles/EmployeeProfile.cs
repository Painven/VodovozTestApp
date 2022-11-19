using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodovozTestApp.DataAccess;
using static Dapper.SqlMapper;

namespace VodovozTestApp.Models.AutoMapper;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
        this.CreateMap<Employee, EmployeeModel>()
            .ForMember(x => x.EmployeeID, x => x.MapFrom(e => e.employee_id));

        this.CreateMap<EmployeeModel, Employee>()
            .ForMember(x => x.employee_id, x => x.MapFrom(e => e.EmployeeID))
            .ForMember(x => x.first_name, x => x.MapFrom(e => e.FirstName))
            .ForMember(x => x.last_name, x => x.MapFrom(e => e.LastName))
            .ForMember(x => x.middle_name, x => x.MapFrom(e => e.MiddleName))
            .ForMember(x => x.date_of_birth, x => x.MapFrom(e => e.DateOfBirth))
            .ForMember(x => x.sex, x => x.MapFrom(e => e.Sex));
            
    }
}

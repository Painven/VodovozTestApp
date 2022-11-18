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
        this.CreateMap<Employee, EmployeeModel>();
    }
}
public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
        this.CreateMap<Department, DepartmentModel>();
    }
}
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
        this.CreateMap<Order, OrderModel>();
    }
}
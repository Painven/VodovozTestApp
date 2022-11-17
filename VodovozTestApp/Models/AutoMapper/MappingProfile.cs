using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodovozTestApp.DataAccess;

namespace VodovozTestApp.Models.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<Department, DepartmentModel>();
        this.CreateMap<Employee, EmployeeModel>();
        this.CreateMap<Order, OrderModel>();
    }
}

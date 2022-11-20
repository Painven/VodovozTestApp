using AutoMapper;
using System;
using System.Windows.Input;
using VodovozTestApp.DataAccess;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Models;

namespace VodovozTestApp.ViewModels;

public class DepartmentDetailsWindowViewModel : ViewModelBase
{
    private readonly IDepartmentRepository departmentRepostiry;
    private readonly IMapper mapper;

    DepartmentModel contextDepartment;
    public DepartmentModel ContextDepartment
    {
        get => contextDepartment;
        set => Set(ref contextDepartment, value);
    }

    public ICommand LoadedCommand { get; }

    public DepartmentDetailsWindowViewModel()
    {
        LoadedCommand = new LambdaCommand(Loaded);
    }

    public DepartmentDetailsWindowViewModel(IDepartmentRepository departmentRepostiry, IMapper mapper) : this()
    {
        this.departmentRepostiry = departmentRepostiry;
        this.mapper = mapper;
    }

    private async void Loaded(object obj)
    {
        var dep = await departmentRepostiry.GetById(ContextDepartment.DepartmentID);
        ContextDepartment = mapper.Map<DepartmentModel>(dep);
    }
}

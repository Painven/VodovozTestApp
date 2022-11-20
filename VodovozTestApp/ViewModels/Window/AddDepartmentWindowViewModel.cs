using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VodovozTestApp.DataAccess;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Models;
using VodovozTestApp.Services;

namespace VodovozTestApp.ViewModels;

public class AddDepartmentWindowViewModel : ViewModelBase
{
    private readonly IDepartmentRepository departmentRepository;
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;
    private readonly IDialogService dialogService;

    public ObservableCollection<EmployeeModel> Employees { get; } = new();

    DepartmentModel contextDepartment = new();
    public DepartmentModel ContextDepartment
    {
        get => contextDepartment;
        set
        {
            if(Set(ref contextDepartment, value))
            {
                if(value != null)
                {
                    ButtonActionText = "Обновить";
                    Title = "Редактирование отдела";
                }
            }
        }
    }

    public ICommand LoadedCommand { get; }
    public ICommand SaveCommand { get; }
    
    string title = "Добавление отдела";
    public string Title
    {
        get => title;
        set => Set(ref title, value);
    }

    string buttonActionText = "Добавить";
    public string ButtonActionText
    {
        get => buttonActionText;
        set => Set(ref buttonActionText, value);
    }

    public AddDepartmentWindowViewModel()
    {
        LoadedCommand = new LambdaCommand(async e => await Load());
        SaveCommand = new LambdaCommand(async e => await Save(e), e => !string.IsNullOrWhiteSpace(contextDepartment.Name) && contextDepartment.Name.Length > 3);
    }

    public AddDepartmentWindowViewModel(IDepartmentRepository departmentRepository, 
        IEmployeeRepository employeeRepository, 
        IMapper mapper,
        IDialogService dialogService) : this()
    {
        this.departmentRepository = departmentRepository;
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
        this.dialogService = dialogService;
    }


    private async Task Load()
    {
        var employees = mapper.Map<IEnumerable<EmployeeModel>>(await employeeRepository.GetAll());
        Employees.ClearAndAddRange(employees);

        if(ContextDepartment.Leader == null && ContextDepartment.LeadID.HasValue)
        {
            ContextDepartment.Leader = Employees.Single(emp => emp.EmployeeID == ContextDepartment.LeadID.Value);
        }
    }

    private async Task Save(object o)
    {
        var department = mapper.Map<Department>(ContextDepartment);
        await departmentRepository.AddOrUpdate(department);
        (o as Window).DialogResult = true;
    }
}

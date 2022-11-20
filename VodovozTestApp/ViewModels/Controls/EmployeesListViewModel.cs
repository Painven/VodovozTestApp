using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VodovozTestApp.DataAccess;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Models;
using VodovozTestApp.Services;

namespace VodovozTestApp.ViewModels;

public class EmployeesListViewModel : ViewModelBase
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IDepartmentRepository departmentRepository;
    private readonly IMapper mapper;
    private readonly IWindowService windowService;
    private readonly IDialogService dialogService;

    bool isLoadingInProgress;
    public bool IsLoadingInProgress
    {
        get => isLoadingInProgress;
        set => Set(ref isLoadingInProgress, value);
    }

    public ICommand LoadedCommand { get; }
    public ICommand AddNewEmployeeCommand { get; }

    public ObservableCollection<EmployeeModel> Employees { get; set; } = new()
    {
        new EmployeeModel() { FirstName = "Имя 1", LastName = "Фамилия 1", Sex = Sex.Male, MiddleName = "Отчество 1" },
        new EmployeeModel() { FirstName = "Имя 2", LastName = "Фамилия 2", Sex = Sex.Male, MiddleName = "Отчество 2" },
        new EmployeeModel() { FirstName = "Имя 3", LastName = "Фамилия 3", Sex = Sex.Male, MiddleName = "Отчество 3" },
        new EmployeeModel() { FirstName = "Имя 4", LastName = "Фамилия 4", Sex = Sex.Male, MiddleName = "Отчество 4" },
        new EmployeeModel() { FirstName = "Имя 5", LastName = "Фамилия 5", Sex = Sex.Male, MiddleName = "Отчество 5" },
    };

    public EmployeesListViewModel()
    {
        LoadedCommand = new LambdaCommand(async t => await LoadEmployees());
        AddNewEmployeeCommand = new LambdaCommand(async t => await AddNewEmployee());
    }

    public EmployeesListViewModel(IEmployeeRepository employeeRepository, 
        IDepartmentRepository departmentRepository,
        IMapper mapper, 
        IWindowService windowService,
        IDialogService dialogService) : this()
    {       
        this.employeeRepository = employeeRepository;
        this.departmentRepository = departmentRepository;
        this.mapper = mapper;
        this.windowService = windowService;
        this.dialogService = dialogService;
    }

    private async Task AddNewEmployee()
    {
        if (windowService.ShowAddEmployeeWindow())
        {
            await LoadEmployees();
        }
    }

    private async Task LoadEmployees()
    {
        Employees.Clear();
        IsLoadingInProgress = true;

        var employees = mapper.Map<List<EmployeeModel>>(await employeeRepository.GetAll());
        
        foreach(var employee in employees)
        {
            employee.OnDeleteClicked += async (e) => 
            {
                if (dialogService.ShowConfirmDialog($"Подтвердите удаление '{e.FirstName} {e.LastName}'"))
                {
                    Employees.Remove(e);
                    await employeeRepository.Delete(e.EmployeeID);
                }
            };
            employee.OnEditClicked += (e) => windowService.ShowAddEmployeeWindow(e);
            employee.OnShowDetailsClicked += (e) => windowService.ShowEmployeeDetailsWindow(e);
            Employees.Add(employee);
        }

        IsLoadingInProgress = false;
    }
}


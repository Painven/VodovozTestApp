using AutoMapper;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VodovozTestApp.DataAccess;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Models;
using VodovozTestApp.Services;

namespace VodovozTestApp.ViewModels;

public class EmployeesListViewModel : ViewModelBase
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;
    private readonly IWindowService windowService;

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
        LoadedCommand = new LambdaCommand(LoadEmployees);
        AddNewEmployeeCommand = new LambdaCommand(e => windowService.ShowAddEmployeeWindow());
    }

    public EmployeesListViewModel(IEmployeeRepository employeeRepository, 
        IMapper mapper, 
        IWindowService windowService) : this()
    {       
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
        this.windowService = windowService;
    }

    private async void LoadEmployees(object obj)
    {
        /*Employees.Clear();

        var employees = await employeeRepository.GetAll();
        employees.ForEach(emp =>
        {
            Employees.Add(mapper.Map<EmployeeModel>(emp));
        });*/
    }
}


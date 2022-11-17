using AutoMapper;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VodovozTestApp.DataAccess;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Models;

namespace VodovozTestApp.ViewModels;

public class EmployeesListViewModel : ViewModelBase
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public ICommand LoadedCommand { get; }

    public ObservableCollection<EmployeeModel> Employees { get; set; } = new();

    public EmployeesListViewModel(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        LoadedCommand = new LambdaCommand(LoadEmployees);
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    private async void LoadEmployees(object obj)
    {
        var employees = await employeeRepository.GetAll();
        employees.ForEach(emp =>
        {
            Employees.Add(mapper.Map<EmployeeModel>(emp));
        });
    }
}


using System;
using System.Windows.Input;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Services;

namespace VodovozTestApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IWindowService windowService;
    public DepartmentsListViewModel Departments { get; } = new();
    public EmployeesListViewModel Employees { get; } = new();
    public OrdersListViewModel Orders { get; } = new();

    string title = "Тестовое приложение. Веселый Водовоз";
    public string Title
    {
        get => title;
        set => Set(ref title, value);
    }

    public ICommand LoadedCommand { get; }
    public ICommand AddNewDepartmentCommand { get; }
    public ICommand AddNewEmployeeCommand { get; }
    public ICommand AddNewOrderCommand { get; }

    public MainWindowViewModel()
    {
        LoadedCommand = new LambdaCommand(e => Title += $" загружено в {DateTime.Now}");
        AddNewDepartmentCommand = new LambdaCommand(e => windowService.ShowAddDepartmentWindow());
        AddNewEmployeeCommand = new LambdaCommand(e => windowService.ShowAddEmployeeWindow());
        AddNewOrderCommand = new LambdaCommand(e => windowService.ShowAddOrderWindow());
    }

    public MainWindowViewModel(IWindowService windowService, 
        DepartmentsListViewModel departments,
        EmployeesListViewModel employees,
        OrdersListViewModel orders) : this()
    {
        this.windowService = windowService;
        this.Departments = departments;
        this.Employees = employees;
        this.Orders = orders;
    }
}

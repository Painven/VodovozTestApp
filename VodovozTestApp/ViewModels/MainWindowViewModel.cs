using System;
using System.Threading.Tasks;
using System.Windows.Input;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Services;

namespace VodovozTestApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IWindowService windowService;
    public DepartmentsListViewModel DepartmentsData { get; } = new();
    public EmployeesListViewModel EmployeesData { get; } = new();
    public OrdersListViewModel OrdersData { get; } = new();

    string title = "WPF Тестовое приложение с MVVM";
    public string Title
    {
        get => title;
        set => Set(ref title, value);
    }

    public ICommand LoadedCommand { get; }

    public MainWindowViewModel()
    {
        LoadedCommand = new LambdaCommand(async (e) => await Loaded(e));
    }

    private async Task Loaded(object obj)
    {
        Title += $" | Загружено в {DateTime.Now.ToLongTimeString()}";
    }

    public MainWindowViewModel(IWindowService windowService,
        DepartmentsListViewModel departments,
        EmployeesListViewModel employees,
        OrdersListViewModel orders) : this()
    {
        this.windowService = windowService;
        this.DepartmentsData = departments;
        this.EmployeesData = employees;
        this.OrdersData = orders;
    }
}

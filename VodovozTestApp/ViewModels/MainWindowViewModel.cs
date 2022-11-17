using System;
using System.Windows.Input;
using VodovozTestApp.Infrastructure.Commands;

namespace VodovozTestApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly AddDepartmentWindowViewModel departmentVm;
    private readonly AddEmployeeWindowViewModel employeeVm;
    private readonly AddOrderWindowViewModel orderVm;

    string title = "Тестовое приложение. Веселый Водовоз";
    public string Title
    {
        get => title;
        set => Set(ref title, value);
    }

    public ICommand LoadedCommand { get; }

    public MainWindowViewModel()
    {
        LoadedCommand = new LambdaCommand(e => Title += $" загружено в {DateTime.Now}");
    }

    public MainWindowViewModel(AddDepartmentWindowViewModel departmentVm,
        AddEmployeeWindowViewModel employeeVm,
        AddOrderWindowViewModel orderVm) : this()
    {
        this.departmentVm = departmentVm;
        this.employeeVm = employeeVm;
        this.orderVm = orderVm;
    }
}

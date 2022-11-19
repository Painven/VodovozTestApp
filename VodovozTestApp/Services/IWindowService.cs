using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using VodovozTestApp.Models;
using VodovozTestApp.ViewModels;
using VodovozTestApp.Views;

namespace VodovozTestApp.Services;

public interface IWindowService 
{
    void ShowAddDepartmentWindow(DepartmentModel editDepartment = null);
    void ShowAddEmployeeWindow(EmployeeModel editEmployee = null);
    void ShowAddOrderWindow();
}

public class WindowService : IWindowService
{
    private readonly IHost host;

    public WindowService(IHost host)
    {
        this.host = host;
    }

    public void ShowAddDepartmentWindow(DepartmentModel editDepartment = null)
    {
        var window = host.Services.GetRequiredService<AddDepartmentWindow>();
        var viewModel = host.Services.GetRequiredService<AddDepartmentWindowViewModel>();
        if(editDepartment != null)
        {
            viewModel.ContextDepartment = editDepartment;
        }
        window.DataContext = viewModel;
        window.ShowDialog();
    }
    public void ShowAddEmployeeWindow(EmployeeModel editEmployee = null)
    {
        var window = host.Services.GetRequiredService<AddEmployeeWindow>();
        var viewModel = host.Services.GetRequiredService<AddEmployeeWindowViewModel>();
        if(editEmployee != null)
        {
            viewModel.ContextEmployee = editEmployee;
        }
        window.DataContext = viewModel;
        window.ShowDialog();
    }
    public void ShowAddOrderWindow()
    {
        var window = host.Services.GetRequiredService<AddOrderWindow>();
        window.DataContext = host.Services.GetRequiredService<AddOrderWindowViewModel>();
        window.ShowDialog();
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using VodovozTestApp.Models;
using VodovozTestApp.ViewModels;
using VodovozTestApp.Views;

namespace VodovozTestApp.Services;

public interface IWindowService 
{
    bool ShowAddDepartmentWindow(DepartmentModel editDepartment = null);
    bool ShowAddEmployeeWindow(EmployeeModel editEmployee = null);
    bool ShowAddOrderWindow(OrderModel editOrder = null);
    void ShowEmployeeDetailsWindow(EmployeeModel employee);
    void ShowDepartmentDetailsWindow(DepartmentModel department);
}

public class WindowService : IWindowService
{
    private readonly IHost host;

    public WindowService(IHost host)
    {
        this.host = host;
    }

    public bool ShowAddDepartmentWindow(DepartmentModel editDepartment = null)
    {
        var window = host.Services.GetRequiredService<AddDepartmentWindow>();
        var viewModel = host.Services.GetRequiredService<AddDepartmentWindowViewModel>();
        if(editDepartment != null)
        {
            viewModel.ContextDepartment = editDepartment;
        }
        window.DataContext = viewModel;
        bool dialogResult = (bool)window.ShowDialog();

        return dialogResult;
    }
    
    public bool ShowAddEmployeeWindow(EmployeeModel editEmployee = null)
    {
        var window = host.Services.GetRequiredService<AddEmployeeWindow>();
        var viewModel = host.Services.GetRequiredService<AddEmployeeWindowViewModel>();
        if(editEmployee != null)
        {
            viewModel.ContextEmployee = editEmployee;
        }
        window.DataContext = viewModel;

        bool dialogResult = (bool)window.ShowDialog();

        return dialogResult;
    }
    
    public bool ShowAddOrderWindow(OrderModel editOrder = null)
    {
        var window = host.Services.GetRequiredService<AddOrderWindow>();
        var viewModel = host.Services.GetRequiredService<AddOrderWindowViewModel>();
        if (editOrder != null)
        {
            viewModel.ContextOrder = editOrder;
        }
        window.DataContext = viewModel;

        bool dialogResult = (bool)window.ShowDialog();

        return dialogResult;
    }

    public void ShowDepartmentDetailsWindow(DepartmentModel department)
    {
        var window = host.Services.GetRequiredService<DepartmentDetailsWindow>();
        var viewModel = host.Services.GetRequiredService<DepartmentDetailsWindowViewModel>();
        viewModel.ContextDepartment = department;
        window.DataContext = viewModel;
        window.ShowDialog();
    }

    public void ShowEmployeeDetailsWindow(EmployeeModel employee)
    {
        var window = host.Services.GetRequiredService<EmployeeDetailsWindow>();
        window.DataContext = employee;
        window.ShowDialog();
    }
}

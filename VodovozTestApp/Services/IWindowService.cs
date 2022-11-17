using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using VodovozTestApp.ViewModels;
using VodovozTestApp.Views;

namespace VodovozTestApp.Services;

public interface IWindowService 
{
    void ShowAddDepartmentWindow();
    void ShowAddEmployeeWindow();
    void ShowAddOrderWindow();
}

public class WindowService : IWindowService
{
    private readonly IHost host;

    public WindowService(IHost host)
    {
        this.host = host;
    }

    public void ShowAddDepartmentWindow()
    {
        var window = host.Services.GetRequiredService<AddDepartmentWindow>();
        window.DataContext = host.Services.GetRequiredService<AddDepartmentWindowViewModel>();
        window.ShowDialog();
    }
    public void ShowAddEmployeeWindow()
    {
        var window = host.Services.GetRequiredService<AddEmployeeWindow>();
        window.DataContext = host.Services.GetRequiredService<AddEmployeeWindowViewModel>();
        window.ShowDialog();
    }
    public void ShowAddOrderWindow()
    {
        var window = host.Services.GetRequiredService<AddOrderWindow>();
        window.DataContext = host.Services.GetRequiredService<AddOrderWindowViewModel>();
        window.ShowDialog();
    }
}

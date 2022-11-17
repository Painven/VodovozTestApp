using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using VodovozTestApp.ViewModels;
using VodovozTestApp.Views;

namespace VodovozTestApp;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost host;

    public App()
    {
        host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.ConfigureAppWindows();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        var mainWindow = host.Services.GetRequiredService<MainWindow>();
        mainWindow.DataContext = host.Services.GetRequiredService<MainWindowViewModel>();
        mainWindow.Show();

        await host.StartAsync();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await host.StopAsync(TimeSpan.FromSeconds(3));
        host?.Dispose();
    }
}

public static class ConfigureServicesExtensions
{
    public static void ConfigureAppWindows(this IServiceCollection services)
    {
        services.AddSingleton<AddDepartmentWindowViewModel>();
        services.AddSingleton<AddEmployeeWindowViewModel>();
        services.AddSingleton<AddOrderWindowViewModel>();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();
    }
}

﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using VodovozTestApp.DataAccess;
using VodovozTestApp.Services;
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
                services.AddAutoMapper(typeof(App).Assembly);
                
                services.ConfigureDatabaseRepositories();
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
    public static void ConfigureDatabaseRepositories(this IServiceCollection services)
    {
        services.AddTransient<IDepartmentRepository, DepartmentRepository>();
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
    }

    public static void ConfigureAppWindows(this IServiceCollection services)
    {
        services.AddSingleton<IWindowService, WindowService>();

        services.AddSingleton<DepartmentsListViewModel>();
        services.AddSingleton<EmployeesListViewModel>();
        services.AddSingleton<OrdersListViewModel>();

        services.AddTransient<AddDepartmentWindow>();
        services.AddSingleton<AddDepartmentWindowViewModel>();

        services.AddTransient<AddEmployeeWindow>();
        services.AddSingleton<AddEmployeeWindowViewModel>();

        services.AddTransient<AddOrderWindow>();
        services.AddSingleton<AddOrderWindowViewModel>();

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();
    }
}

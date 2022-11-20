using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.ViewModels;

namespace VodovozTestApp.Models;

public class OrderModel : ViewModelBase
{
    public event Action<OrderModel> OnShowDetailsClicked;

    public int OrderID { get; set; }
    public int? EmployeeID { get; set; }
    
    public string ProductName { get; set; }
    public ObservableCollection<OrderTagModel> Tags { get; set; } = new();

    EmployeeModel manager;
    public EmployeeModel Manager
    {
        get => manager;
        set
        {
            if(Set(ref manager, value))
            {
                EmployeeID = manager?.EmployeeID;
            }
        }
    }

    public ICommand ShowDetailsWindowCommand { get; }

    public OrderModel()
    {
        ShowDetailsWindowCommand = new LambdaCommand(e => OnShowDetailsClicked?.Invoke(this));
    }
}

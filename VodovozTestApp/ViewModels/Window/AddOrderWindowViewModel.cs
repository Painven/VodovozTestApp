using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VodovozTestApp.DataAccess;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Models;

namespace VodovozTestApp.ViewModels;

public class AddOrderWindowViewModel : ViewModelBase
{
    private readonly IOrderRepository orderRepository;
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    OrderModel contextOrder = new();
    public OrderModel ContextOrder
    {
        get => contextOrder;
        set
        {
            if (Set(ref contextOrder, value))
            {
                Title = $"Детали заказа #{value.OrderID}";
                ButtonActionText = "Обновить данные";
            }
        }
    }

    public ObservableCollection<OrderTagModel> Tags { get; } = new();
    public ObservableCollection<EmployeeModel> Employees { get; } = new();

    public ICommand SaveCommand { get; }
    public ICommand LoadedCommand { get; }

    string title = "Новый заказ";
    public string Title
    {
        get => title;
        set => Set(ref title, value);
    }

    string buttonActionText = "Создать";
    public string ButtonActionText
    {
        get => buttonActionText;
        set => Set(ref buttonActionText, value);
    }

    public AddOrderWindowViewModel()
    {
        SaveCommand = new LambdaCommand(async e => await MakeOrder(e), 
            e => !string.IsNullOrWhiteSpace(contextOrder.ProductName) && contextOrder.Manager != null);
        LoadedCommand = new LambdaCommand(async e => await Loaded());
    } 

    public AddOrderWindowViewModel(IOrderRepository orderRepository, IEmployeeRepository employeeRepository, IMapper mapper) : this()
    {
        this.orderRepository = orderRepository;
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    private async Task Loaded()
    {
        if (contextOrder.OrderID != 0)
        {
            var orderFullInfo = await orderRepository.GetById(ContextOrder.OrderID);
            ContextOrder = mapper.Map<OrderModel>(orderFullInfo);
        }

        var allTags = mapper.Map<List<OrderTagModel>>(await orderRepository.GetTags());
        Tags.ClearAndAddRange(allTags);

        var employees = mapper.Map<List<EmployeeModel>>(await employeeRepository.GetAll());
        Employees.ClearAndAddRange(employees);

        contextOrder.Manager = Employees.FirstOrDefault(emp => emp.EmployeeID == contextOrder.EmployeeID);

        if (contextOrder.Tags.Any())
        {
            foreach (var tag in Tags)
            {
                tag.IsChecked = contextOrder.Tags.FirstOrDefault(t => t.TagID == tag.TagID) != null;
            }
        }
    }

    private async Task MakeOrder(object obj)
    {
        ContextOrder.Tags.ClearAndAddRange(Tags.Where(t => t.IsChecked));

        var order = mapper.Map<Order>(ContextOrder);

        if (ContextOrder.OrderID == 0)
        {
            await orderRepository.AddNew(order);
        }
        else
        {
            await orderRepository.Update(order);
        }
        (obj as Window).DialogResult = true;
    }
}

public class OrderTagModel : ViewModelBase
{
    public int TagID { get; set; }
    public string Name { get; set; }

    bool isChecked;
    public bool IsChecked
    {
        get => isChecked;
        set => Set(ref isChecked, value);
    }
}
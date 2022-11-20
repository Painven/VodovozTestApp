using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VodovozTestApp.DataAccess;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Models;
using VodovozTestApp.Services;

namespace VodovozTestApp.ViewModels;

public class OrdersListViewModel : ViewModelBase
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;
    private readonly IWindowService windowService;

    public ICommand LoadedCommand { get; }
    public ICommand AddNewOrderCommand { get; }
   
    public ObservableCollection<OrderModel> Orders { get; set; } = new()
    {
        new OrderModel() { ProductName = "Товар в заказе 1", OrderID = 1 },
        new OrderModel() { ProductName = "Товар в заказе 2", OrderID = 2 },
        new OrderModel() { ProductName = "Товар в заказе 3", OrderID = 3 },
        new OrderModel() { ProductName = "Товар в заказе 4", OrderID = 4 },
        new OrderModel() { ProductName = "Товар в заказе 5", OrderID = 5 },
    };

    public OrdersListViewModel()
    {
        LoadedCommand = new LambdaCommand(async e => await LoadOrders());
        AddNewOrderCommand = new LambdaCommand(async e => await AddOrder());
    }

    public OrdersListViewModel(IOrderRepository orderRepository, 
        IMapper mapper, 
        IWindowService windowService) : this()
    {     
        this.orderRepository = orderRepository;
        this.mapper = mapper;
        this.windowService = windowService;
    }

    private async Task LoadOrders()
    {
        Orders.Clear();

        var orders = mapper.Map<List<OrderModel>>(await orderRepository.GetAll());

        foreach (var order in orders)
        {
            order.OnShowDetailsClicked += (e) => windowService.ShowAddOrderWindow(e);
            Orders.Add(order);
        }
    }

    private async Task AddOrder()
    {
        if (windowService.ShowAddOrderWindow())
        {
            await LoadOrders();
        }
    }
}


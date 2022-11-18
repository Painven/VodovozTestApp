using AutoMapper;
using System.Collections.ObjectModel;
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
        new OrderModel() { ProductName = "Товар в заказе 1" },
        new OrderModel() { ProductName = "Товар в заказе 2" },
        new OrderModel() { ProductName = "Товар в заказе 3" },
        new OrderModel() { ProductName = "Товар в заказе 4" },
        new OrderModel() { ProductName = "Товар в заказе 5" },
    };

    public OrdersListViewModel()
    {
        LoadedCommand = new LambdaCommand(LoadOrders);
        AddNewOrderCommand = new LambdaCommand(e => windowService.ShowAddOrderWindow());
    }

    public OrdersListViewModel(IOrderRepository orderRepository, 
        IMapper mapper, 
        IWindowService windowService) : this()
    {     
        this.orderRepository = orderRepository;
        this.mapper = mapper;
        this.windowService = windowService;
    }

    private async void LoadOrders(object obj)
    {
        /*Orders.Clear();

        var orders = await orderRepository.GetAll();
        orders.ForEach(o =>
        {
            Orders.Add(mapper.Map<OrderModel>(o));
        });*/
    }
}


using AutoMapper;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VodovozTestApp.DataAccess;
using VodovozTestApp.Infrastructure.Commands;
using VodovozTestApp.Models;

namespace VodovozTestApp.ViewModels;

public class OrdersListViewModel : ViewModelBase
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public ICommand LoadedCommand { get; }

    public ObservableCollection<OrderModel> Orders { get; set; } = new();

    public OrdersListViewModel(IOrderRepository orderRepository, IMapper mapper)
    {
        LoadedCommand = new LambdaCommand(LoadOrders);
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    private async void LoadOrders(object obj)
    {
        var orders = await orderRepository.GetAll();
        orders.ForEach(o =>
        {
            Orders.Add(mapper.Map<OrderModel>(o));
        });
    }
}


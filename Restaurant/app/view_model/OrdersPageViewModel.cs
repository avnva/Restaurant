using Restaurant.app.view;
using Restaurant.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.app.view_model;

public class OrdersPageViewModel : ViewModelBase
{
    private ObservableCollection<Order> orders;
    private OrderRepository repository;
    private Order selectedOrder;

    public event Action<Order> NewOrderAdded;

    public ObservableCollection<Order> Orders
    {
        get { return orders; }
        set
        {
            orders = value;
            OnPropertyChanged(nameof(Orders));
        }
    }

    public Order SelectedOrder
    {
        get { return selectedOrder; }
        set
        {
            selectedOrder = value;
            OnPropertyChanged(nameof(SelectedOrder));
        }
    }

    public RelayCommand AddNewOrderCommand { get; private set; }
    public RelayCommand OpenOrderInfoCommand { get; private set; }
    public RelayCommand ReloadCommand { get; private set; }
    public RelayCommand ReduceGridCommand { get; private set; }

    public OrdersPageViewModel()
    {
        repository = new OrderRepository(new RestaurantDbContext());

        ReloadCommand = new RelayCommand(LoadOrders);
        AddNewOrderCommand = new RelayCommand(AddNewOrder);
        OpenOrderInfoCommand = new RelayCommand(OpenOrderInfo, CanOpenOrderInfo);
        ReduceGridCommand = new RelayCommand(ReduceGrid);

        LoadOrders();
    }
    private void ReduceGrid(object obj)
    {
        repository = new OrderRepository(new RestaurantDbContext());
        LoadOrders();
    }

    private bool CanOpenOrderInfo(object obj)
    {
        return SelectedOrder != null;
    }

    private void OpenOrderInfo(object obj)
    {
        OpenOrderInfoPage(SelectedOrder);
    }

    private void LoadOrders(object obj = null)
    {
        List<Order> loadedOrders = repository.GetOrders();
        Orders = new ObservableCollection<Order>(loadedOrders);
    }

    private void AddNewOrder(object obj)
    {
        OpenOrderInfoPage(null);
    }

    private void OpenOrderInfoPage(Order order)
    {
        EditOrderInfoPage editOrderInfoPage = new EditOrderInfoPage(order);

        DataStore.Frame.NavigationService.Navigate(editOrderInfoPage);
    }

}

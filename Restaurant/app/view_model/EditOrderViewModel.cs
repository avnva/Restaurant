using Restaurant.app.model;
using Restaurant.data.repository;
using Restaurant.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Restaurant.app.view_model;

public class EditOrderViewModel : ViewModelBase
{
    private RestaurantDbContext db;
    private OrderRepository orderRepository;
    private OrdersDishesRepository ordersDishesRepository;
    private DishRepository dishRepository;

    public Order Order { get; set; }

    private ObservableCollection<OrdersDishes> ordersDishes;
    public ObservableCollection<Dish> Dishes { get; set; }

    public RelayCommand AddDishCommand { get; set; }

    public RelayCommand SaveCommand { get; set; }
    public RelayCommand DeleteDishCommand { get; set; }
    public RelayCommand DeleteOrderCommand { get; set; }

    private int selectedQuantity;
    public int SelectedQuantity
    {
        get { return selectedQuantity; }
        set
        {
            selectedQuantity = value;
            OnPropertyChanged(nameof(SelectedQuantity));
        }
    }
    private Dish selectedDish;
    public Dish SelectedDish
    {
        get => selectedDish;
        set
        {
            selectedDish = value;
            OnPropertyChanged(nameof(SelectedDish));
        }
    }

    public ObservableCollection<OrdersDishes> OrdersDishes
    {
        get { return ordersDishes; }
        set
        {
            ordersDishes = value;
            OnPropertyChanged(nameof(OrdersDishes));
        }
    }
    private void AddDish(object obj)
    {
        // Проверка существующего ингредиента в коллекции
        if (SelectedDish != null && SelectedQuantity > 0)
        {
            // Проверка существующего ингредиента в коллекции
            bool flag = false;
            foreach (var order in OrdersDishes)
            {
                if (order.OrderID == Order.OrderID && order.DishID == SelectedDish.DishID)
                {
                    order.Quantity += SelectedQuantity;
                    Order.OrderCost += SelectedDish.DishCost * SelectedQuantity;
                    flag = true;
                }
            }
            if (!flag)
            {
                // Ингредиент не найден, добавляем новый
                var newOrdersDishes = new OrdersDishes
                {
                    OrderID = Order.OrderID,
                    DishID = SelectedDish.DishID,
                    Order = Order,
                    Dish = SelectedDish,
                    Quantity = SelectedQuantity
                };
                Order.OrderCost += SelectedDish.DishCost * SelectedQuantity;
                OrdersDishes.Add(newOrdersDishes);

            }
            OrdersDishes = new ObservableCollection<OrdersDishes>(OrdersDishes);
            OnPropertyChanged(nameof(OrdersDishes));
            OnPropertyChanged(nameof(Order));
        }
        else
        {
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, выберите блюдо и количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Show();
        }
    }

    private void SaveChanges(object obj)
    {
        if (Order.OrderCost == 0 || OrdersDishes.Count() == 0)
        {
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Show();
        }
        else
        {
            if (Order.OrderID != 0)
            {
                // Если уже существует, обновляем его данные
                orderRepository.UpdateOrder(Order);

                foreach (var order in OrdersDishes)
                {
                    ordersDishesRepository.Update(order);
                }

            }
            else
            {
                // Иначе добавляем новое блюдо
                int ID = 0;
                if (allOrdersDishes != null)
                {
                    ID = orderRepository.GetMaxOrderId() + 1;
                }
                if (ID == 0)
                    ID = 1;
                Order.OrderID = ID;
                orderRepository.AddOrder(Order);

                foreach (var order in OrdersDishes)
                {
                    order.OrderID = ID;
                    ordersDishesRepository.Add(order);
                }

            }
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Заказ сохранен", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            args.Show();
        }

    }
    private void DeleteDish(object obj)
    {
        bool flag = false;
        if (SelectedDish != null && SelectedQuantity > 0)
        {
            // Проверка существующего ингредиента в коллекции
            foreach (var order in OrdersDishes)
            {
                if (order.OrderID == Order.OrderID && order.DishID == SelectedDish.DishID && order.Quantity == SelectedQuantity)
                {
                    OrdersDishes.Remove(order);
                    Order.OrderCost -= SelectedDish.DishCost * SelectedQuantity;
                    flag = true;
                }
                else if (order.OrderID == Order.OrderID && order.DishID == SelectedDish.DishID && order.Quantity > SelectedQuantity)
                {
                    order.Quantity -= SelectedQuantity;
                    Order.OrderCost -= SelectedDish.DishCost * SelectedQuantity;
                    flag = true;
                }

            }
            if (flag == false)
            {
                MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Некорректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Show();
            }
            OrdersDishes = new ObservableCollection<OrdersDishes>(OrdersDishes);
            OnPropertyChanged(nameof(OrdersDishes));
        }
        else
        {
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, выберите блюдо и количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Show();
        }

    }
    void SelectOrdersDishes(ObservableCollection<OrdersDishes> all)
    {
        if (all.Count != 0 && Order != null)
        {
            if (OrdersDishes != null )
            {
                OrdersDishes.Clear();
            }
            else
            {
                OrdersDishes = new ObservableCollection<OrdersDishes>();
            }
            foreach (var order in all)
            {
                if (order.OrderID == Order.OrderID)
                {
                    OrdersDishes.Add(order);
                }
            }
        }
        else
        {
            OrdersDishes = new ObservableCollection<OrdersDishes>();
        }
    }
    private void DeleteOrder(object obj)
    {

        ordersDishesRepository.Delete(Order.OrderID);
        orderRepository.DeleteOrder(Order.OrderID);
        MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Заказ удален", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
        args.Show();
    }
    ObservableCollection<OrdersDishes> allOrdersDishes;
    public EditOrderViewModel(Order order)
    {
        db = new RestaurantDbContext();
        Order = order;
        SelectedQuantity = 1;

        orderRepository = new OrderRepository(db);
        ordersDishesRepository = new OrdersDishesRepository(db);
        dishRepository = new DishRepository(db);

        Dishes = new ObservableCollection<Dish>(dishRepository.GetDishes());
        allOrdersDishes = new ObservableCollection<OrdersDishes>(ordersDishesRepository.Get());
        SelectOrdersDishes(allOrdersDishes);

        if (Order == null) 
        { 
            Order = new Order();
            Order.OrderID = 0;
            Order.OrderDate = DateTime.Now;
            Order.OrderCost = 0;
            OrdersDishes = new ObservableCollection<OrdersDishes>();
        }

        AddDishCommand = new RelayCommand(AddDish);
        SaveCommand = new RelayCommand(SaveChanges);

        DeleteDishCommand = new RelayCommand(DeleteDish);
        DeleteOrderCommand = new RelayCommand(DeleteOrder);
    }
}

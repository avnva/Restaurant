using Restaurant.app.model;
using Restaurant.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.app.view_model;

public class WarehousesPageViewModel : ViewModelBase
{
    private ObservableCollection<Warehouse> warehouses;
    private WarehouseRepository repository;
    private Warehouse selectedWarehouse;

    public event Action<Warehouse> NewWarehouseAdded;

    public ObservableCollection<Warehouse> Warehouses
    {
        get { return warehouses; }
        set
        {
            warehouses = value;
            OnPropertyChanged(nameof(Warehouses));
        }
    }

    public Warehouse SelectedWarehouse
    {
        get { return selectedWarehouse; }
        set
        {
            selectedWarehouse = value;
            OnPropertyChanged(nameof(SelectedWarehouse));
        }
    }

    public RelayCommand AddNewWarehouseCommand { get; private set; }
    public RelayCommand OpenWarehouseInfoCommand { get; private set; }
    public RelayCommand ReloadCommand { get; private set; }

    public WarehousesPageViewModel()
    {
        repository = new WarehouseRepository(new RestaurantDbContext());

        ReloadCommand = new RelayCommand(LoadWarehouses);
        AddNewWarehouseCommand = new RelayCommand(AddNewWarehouse);
        OpenWarehouseInfoCommand = new RelayCommand(OpenWarehouseInfo, CanOpenWarehouseInfo);

        LoadWarehouses();
    }

    private bool CanOpenWarehouseInfo(object obj)
    {
        return SelectedWarehouse != null;
    }

    private void OpenWarehouseInfo(object obj)
    {
        OnNewWarehouseAdded(SelectedWarehouse);
    }

    private void LoadWarehouses(object obj = null)
    {
        List<Warehouse> loadedWarehouses = repository.GetWarehouses();
        Warehouses = new ObservableCollection<Warehouse>(loadedWarehouses);
    }

    private void AddNewWarehouse(object obj)
    {
        Warehouse newWarehouse = new Warehouse();

        // Добавление информации о продукте для нового склада
        Product newProduct = new Product(); // Создание нового продукта
        newWarehouse.Product = newProduct; // Привязка продукта к складу

        Supplier productSupplier = new Supplier();
        newWarehouse.Supplier = productSupplier;
        OnNewWarehouseAdded(new Warehouse());
    }
    private void OnNewWarehouseAdded(Warehouse warehouse)
    {
        NewWarehouseAdded?.Invoke(warehouse);
    }
}

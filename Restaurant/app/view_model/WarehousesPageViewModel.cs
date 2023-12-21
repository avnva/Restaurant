using Restaurant.app.model;
using Restaurant.app.view;
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

    public RelayCommand ReloadCommand { get; private set; }
    public RelayCommand OpenSupplierInfoCommand { get; set; }
    public RelayCommand ReduceGridCommand { get; set; }

    public WarehousesPageViewModel()
    {
        repository = new WarehouseRepository(new RestaurantDbContext());

        ReloadCommand = new RelayCommand(LoadWarehouses);
        OpenSupplierInfoCommand = new RelayCommand(OpenSupplierInfo, CanOpenSupplierInfo);
        ReduceGridCommand = new RelayCommand(ReduceGrid);

        LoadWarehouses();
    }
    private void ReduceGrid(object obj)
    {

    }

    private void LoadWarehouses(object obj = null)
    {
        List<Warehouse> loadedWarehouses = repository.GetWarehouses();
        Warehouses = new ObservableCollection<Warehouse>(loadedWarehouses);
    }

    private bool CanOpenSupplierInfo(object obj)
    {
        return SelectedWarehouse != null;
    }

    private void OpenSupplierInfo(object obj)
    {
        OpenSupplierInfoPage(SelectedWarehouse);
    }
    private void OpenSupplierInfoPage(Warehouse warehouse)
    {
        SupplierInfo suplierInfoPage = new SupplierInfo(warehouse.Supplier);

        DataStore.Frame.NavigationService.Navigate(suplierInfoPage);
    }
}

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
    private Supplier selectedSupplier;

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

    public Supplier SelectedSupplier
    {
        get { return selectedSupplier; }
        set
        {
            selectedSupplier = value;
            OnPropertyChanged(nameof(SelectedSupplier));

        }
    }

    public RelayCommand ReloadCommand { get; private set; }
    public RelayCommand OpenSupplierInfoCommand { get; private set; }

    public WarehousesPageViewModel()
    {
        repository = new WarehouseRepository(new RestaurantDbContext());

        ReloadCommand = new RelayCommand(LoadWarehouses);
        OpenSupplierInfoCommand = new RelayCommand(OpenSupplierInfo, CanOpenSupplierInfo);

        LoadWarehouses();
    }

    private void LoadWarehouses(object obj = null)
    {
        List<Warehouse> loadedWarehouses = repository.GetWarehouses();
        Warehouses = new ObservableCollection<Warehouse>(loadedWarehouses);
    }

    private bool CanOpenSupplierInfo(object obj)
    {
        return SelectedSupplier != null;
    }

    private void OpenSupplierInfo(object obj)
    {
        OpenSupplierInfoPage(SelectedSupplier);
    }
    private void OpenSupplierInfoPage(Supplier supplier)
    {
        SupplierInfo suplierInfoPage = new SupplierInfo(supplier);

        DataStore.Frame.NavigationService.Navigate(suplierInfoPage);
    }
}

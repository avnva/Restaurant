using Restaurant.app.view;
using Restaurant.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.app.view_model;

public class SuppliesPageViewModel : ViewModelBase
{
    private ObservableCollection<Supply> supplies;
    private SupplyRepository repository;
    private Supply selectedSupply;

    public event Action<Supply> NewSupplyAdded;

    public ObservableCollection<Supply> Supplies
    {
        get { return supplies; }
        set
        {
            supplies = value;
            OnPropertyChanged(nameof(Supplies));
        }
    }

    public Supply SelectedSupply
    {
        get { return selectedSupply; }
        set
        {
            selectedSupply = value;
            OnPropertyChanged(nameof(SelectedSupply));
        }
    }

    public RelayCommand AddNewSupplyCommand { get; private set; }
    public RelayCommand OpenSupplyInfoCommand { get; private set; }
    public RelayCommand ReloadCommand { get; private set; }
    public RelayCommand OpenSupplierInfoCommand { get; set; }
    public RelayCommand ReduceGridCommand { get; set; }


    public SuppliesPageViewModel()
    {
        repository = new SupplyRepository(new RestaurantDbContext());

        ReloadCommand = new RelayCommand(LoadSupplies);
        AddNewSupplyCommand = new RelayCommand(AddNewSupply);
        OpenSupplyInfoCommand = new RelayCommand(OpenSupplyInfo, CanOpenSupplyInfo);
        OpenSupplierInfoCommand = new RelayCommand(OpenSupplierInfo, CanOpenSupplierInfo);
        ReduceGridCommand = new RelayCommand(ReduceGrid);
        LoadSupplies();
    }
    private void ReduceGrid(object obj)
    {
        repository = new SupplyRepository(new RestaurantDbContext());
        LoadSupplies();
    }


    private bool CanOpenSupplyInfo(object obj)
    {
        return SelectedSupply != null;
    }

    private void OpenSupplyInfo(object obj)
    {
        OpenSupplyInfoPage(SelectedSupply);
        //OnNewSupplyAdded(SelectedSupply);
    }

    private void LoadSupplies(object obj = null)
    {
        List<Supply> loadedSupplies = repository.GetSupplies();
        Supplies = new ObservableCollection<Supply>(loadedSupplies);
    }

    private void AddNewSupply(object obj)
    {
        //Supply newSupply = new Supply();

        //Supplier productSupplier = new Supplier();
        //newSupply.Supplier = productSupplier;
        OpenSupplyInfoPage(null);
        //OnNewSupplyAdded(new Supply());
    }
    //private void OnNewSupplyAdded(Supply supply)
    //{
    //    NewSupplyAdded?.Invoke(supply);
    //}
    private bool CanOpenSupplierInfo(object obj)
    {
        return SelectedSupply != null;
    }
    private void OpenSupplierInfo(object obj)
    {
        OpenSupplierInfoPage(SelectedSupply);
    }
    private void OpenSupplierInfoPage(Supply supply)
    {
        SupplierInfo suplierInfoPage = new SupplierInfo(supply.Supplier);

        DataStore.Frame.NavigationService.Navigate(suplierInfoPage);
    }

    private void OpenSupplyInfoPage(Supply supply)
    {
        EditSupllyInfoPage editSupllyInfoPage = new EditSupllyInfoPage(supply);

        DataStore.Frame.NavigationService.Navigate(editSupllyInfoPage);
    }

}

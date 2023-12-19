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

    public SuppliesPageViewModel()
    {
        repository = new SupplyRepository(new RestaurantDbContext());

        ReloadCommand = new RelayCommand(LoadSupplies);
        AddNewSupplyCommand = new RelayCommand(AddNewSupply);
        OpenSupplyInfoCommand = new RelayCommand(OpenSupplyInfo, CanOpenSupplyInfo);

        LoadSupplies();
    }

    private bool CanOpenSupplyInfo(object obj)
    {
        return SelectedSupply != null;
    }

    private void OpenSupplyInfo(object obj)
    {
        OnNewSupplyAdded(SelectedSupply);
    }

    private void LoadSupplies(object obj = null)
    {
        List<Supply> loadedSupplies = repository.GetSupplies();
        Supplies = new ObservableCollection<Supply>(loadedSupplies);
    }

    private void AddNewSupply(object obj)
    {
        Supply newSupply = new Supply();

        Supplier productSupplier = new Supplier();
        newSupply.Supplier = productSupplier;
        OnNewSupplyAdded(new Supply());
    }
    private void OnNewSupplyAdded(Supply supply)
    {
        NewSupplyAdded?.Invoke(supply);
    }
}

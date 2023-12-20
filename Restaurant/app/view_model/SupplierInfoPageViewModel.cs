using Restaurant.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Restaurant.app.view_model;

public class SupplierInfoPageViewModel : ViewModelBase
{
    private ObservableCollection<Supplier> suppliers;
    private SupplierRepository repository;
    private Supplier selectedSupplier;

    public event Action<Supplier> NewSupplierAdded;

    public ObservableCollection<Supplier> Suppliers
    {
        get { return suppliers; }
        set
        {
            suppliers = value;
            OnPropertyChanged(nameof(Suppliers));
        }
    }

    public Supplier SelectedSupplier
    {
        get { return selectedSupplier; }
        set
        {
            selectedSupplier = value;
            OnPropertyChanged(nameof(SelectedSupplier));
            OnPropertyChanged(nameof(SelectedSupplierName));
            OnPropertyChanged(nameof(SelectedSupplierAddress));
            OnPropertyChanged(nameof(SelectedSupplierContactPersonName));
            OnPropertyChanged(nameof(SelectedSupplierPhone));
            OnPropertyChanged(nameof(SelectedSupplierBankName));
            OnPropertyChanged(nameof(SelectedSupplierBankAccount));
            OnPropertyChanged(nameof(SelectedSupplierINN));
        }
    }

    public string SelectedSupplierName => SelectedSupplier?.SupplierName;
    public string SelectedSupplierAddress => SelectedSupplier?.Address;
    public string SelectedSupplierContactPersonName => SelectedSupplier?.ContactPersonName;
    public string SelectedSupplierPhone => SelectedSupplier?.Phone;
    public string SelectedSupplierBankName => SelectedSupplier?.BankName;
    public string SelectedSupplierBankAccount => SelectedSupplier?.BankAccount;
    public string SelectedSupplierINN => SelectedSupplier?.INN;

    public RelayCommand AddNewSupplierCommand { get; private set; }
    public RelayCommand OpenSupplierInfoCommand { get; private set; }
    public RelayCommand ReloadCommand { get; private set; }

    public SupplierInfoPageViewModel()
    {
        repository = new SupplierRepository(new RestaurantDbContext());

        ReloadCommand = new RelayCommand(LoadSuppliers);
        AddNewSupplierCommand = new RelayCommand(AddNewSupplier);
        OpenSupplierInfoCommand = new RelayCommand(OpenSupplierInfo, CanOpenSupplierInfo);

        LoadSuppliers();
    }

    private bool CanOpenSupplierInfo(object obj)
    {
        return SelectedSupplier != null;
    }

    private void OpenSupplierInfo(object obj)
    {
        OnNewSupplierAdded(SelectedSupplier);
    }

    private void LoadSuppliers(object obj = null)
    {
        List<Supplier> loadedSuppliers = repository.GetSuppliers();
        Suppliers = new ObservableCollection<Supplier>(loadedSuppliers);
    }

    private void AddNewSupplier(object obj)
    {
        OnNewSupplierAdded(new Supplier());
    }
    private void OnNewSupplierAdded(Supplier supplier)
    {
        NewSupplierAdded?.Invoke(supplier);
    }
}

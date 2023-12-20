using Restaurant.app.model;
using Restaurant.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.app.view_model;

public class EditSupplyViewModel : ViewModelBase
{
    private RestaurantDbContext db;
    private SupplierRepository supplierRepository;

    private Supplier selectedSupplier;

    public Supply Supply { get; set; }

    public ObservableCollection<Supplier> Suppliers { get; set; }

    public Supplier SelectedSupplier
    {
        get => selectedSupplier;
        set
        {
            selectedSupplier = value;
            
            Supply.Supplier = value;
            Supply.SupplierID = value.SupplierID;
            OnPropertyChanged(nameof(SelectedSupplier));
        }
    }

    public EditSupplyViewModel(Supply supply)
    {
        db = new RestaurantDbContext();
        Supply = supply;

        supplierRepository = new SupplierRepository(db);
        Suppliers = new ObservableCollection<Supplier>(supplierRepository.GetSuppliers());

        //Supplier.SupplierID = Suppliers.FirstOrDefault(i => i.SupplierID == Supplier.GroupID);
        //SelectedSupplier = Supplier.DishGroup;
    }
}

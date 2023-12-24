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

public class EditSupplyViewModel : ViewModelBase
{
    private RestaurantDbContext db;
    private SupplierRepository supplierRepository;
    private SupplyRepository supplyRepository;
    private SuppliesProductsRepository suppliesProductsRepository;
    private ProductRepository productRepository;

    private Supplier selectedSupplier;

    public Supply Supply { get; set; }

    public ObservableCollection<Supplier> Suppliers { get; set; }
    private ObservableCollection<SuppliesProducts> suppliesProducts;
    public ObservableCollection<Product> Products { get; set; }

    public RelayCommand AddDishCommand { get; set; }

    public RelayCommand SaveCommand { get; set; }
    public RelayCommand DeleteDishCommand { get; set; }
    public RelayCommand DeleteSupplyCommand { get; set; }

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
    private Product selectedProduct;
    public Product SelectedProduct
    {
        get => selectedProduct;
        set
        {
            selectedProduct = value;
            OnPropertyChanged(nameof(SelectedProduct));
        }
    }

    public ObservableCollection<SuppliesProducts> SuppliesProducts
    {
        get { return suppliesProducts; }
        set
        {
            suppliesProducts = value;
            OnPropertyChanged(nameof(SuppliesProducts));
        }
    }
    private void AddDish(object obj)
    {
        // Проверка существующего ингредиента в коллекции
        if (SelectedProduct != null && SelectedQuantity > 0)
        {
            // Проверка существующего ингредиента в коллекции
            bool flag = false;
            foreach (var supply in SuppliesProducts)
            {
                if (supply.SupplyID == Supply.SupplyID && supply.ProductID == SelectedProduct.ProductID)
                {
                    supply.DeliveredQuantity += SelectedQuantity;

                    flag = true;
                }
            }
            if (!flag)
            {
                // Ингредиент не найден, добавляем новый
                var newSupplyProduct = new SuppliesProducts
                {
                    SupplyID = Supply.SupplyID,
                    ProductID = SelectedProduct.ProductID,
                    Supply = Supply,
                    Product = SelectedProduct,
                    DeliveredQuantity = SelectedQuantity
                };

                SuppliesProducts.Add(newSupplyProduct);
            }
            SuppliesProducts = new ObservableCollection<SuppliesProducts>(SuppliesProducts);
            OnPropertyChanged(nameof(SuppliesProducts));
        }
        else
        {
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, выберите продукт и количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Show();
        }
    }

    private void SaveChanges(object obj)
    {
        if (SelectedSupplier == null || Supply.PurchasePrice == 0 || SuppliesProducts.Count() == 0)
        {
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Show();
        }
        else
        {
            if (Supply.SupplyID != 0)
            {
                // Если уже существует, обновляем его данные
                supplyRepository.UpdateSupply(Supply);

                foreach (var supply in SuppliesProducts)
                {
                    suppliesProductsRepository.Update(supply);
                }

            }
            else
            {
                // Иначе добавляем новое блюдо
                int ID = 0;
                if (allSuppliesProducts != null)
                {
                    ID = supplyRepository.GetMaxSupplyId() + 1;
                }
                if (ID == 0)
                    ID = 1;
                Supply.SupplyID = ID;
                supplyRepository.AddSupply(Supply);

                foreach (var product in SuppliesProducts)
                {
                    product.SupplyID = ID;
                    suppliesProductsRepository.Add(product);
                }

            }
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Поставка сохранена", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            args.Show();
        }

    }
    private void DeleteDish(object obj)
    {
        bool flag = false;
        if (SelectedProduct != null && SelectedQuantity > 0)
        {
            // Проверка существующего ингредиента в коллекции
            foreach (var supply in SuppliesProducts)
            {
                if (supply.SupplyID == Supply.SupplyID && supply.ProductID == SelectedProduct.ProductID && supply.DeliveredQuantity == SelectedQuantity)
                {
                    SuppliesProducts.Remove(supply);
                    flag = true;
                }
                else if (supply.SupplyID == Supply.SupplyID && supply.ProductID == SelectedProduct.ProductID && supply.DeliveredQuantity > SelectedQuantity)
                {
                    supply.DeliveredQuantity -= SelectedQuantity;
                    flag = true;
                }

            }
            if (flag == false)
            {
                MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Некорректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Show();
            }
            SuppliesProducts = new ObservableCollection<SuppliesProducts>(SuppliesProducts);
            OnPropertyChanged(nameof(SuppliesProducts));
        }
        else
        {
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, выберите продукт и количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Show();
        }

    }
    void SelectDishesProducts(ObservableCollection<SuppliesProducts> all)
    {
        if (all.Count != 0 && Supply != null)
        {
            if (SuppliesProducts != null)
            {
                SuppliesProducts.Clear();
            }
            else
            {
                SuppliesProducts = new ObservableCollection<SuppliesProducts>();
            }
            foreach (var supply in all)
            {
                if (supply.SupplyID == Supply.SupplyID)
                {
                    SuppliesProducts.Add(supply);
                }
            }
        }
        else
        {
            SuppliesProducts = new ObservableCollection<SuppliesProducts>();
        }
    }
    private void DeleteSupply(object obj)
    {
        
        suppliesProductsRepository.Delete(Supply.SupplyID);
        supplyRepository.DeleteSupply(Supply.SupplierID);
        MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Поставка удалена", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
        args.Show();
    }
    ObservableCollection<SuppliesProducts> allSuppliesProducts;
    public EditSupplyViewModel(Supply supply)
    {
        db = new RestaurantDbContext();
        Supply = supply;
        SelectedQuantity = 1;


        supplierRepository = new SupplierRepository(db);
        supplyRepository = new SupplyRepository(db);
        suppliesProductsRepository = new SuppliesProductsRepository(db);
        productRepository = new ProductRepository(db);

        Suppliers = new ObservableCollection<Supplier>(supplierRepository.GetSuppliers());
        Products = new ObservableCollection<Product>(productRepository.GetProductsWithUnitOfMeasure());
        allSuppliesProducts = new ObservableCollection<SuppliesProducts>(suppliesProductsRepository.Get());
        SelectDishesProducts(allSuppliesProducts);

        if (Supply != null)
        {
            SelectedSupplier = Suppliers.FirstOrDefault(i => i.SupplierID == Supply.Supplier.SupplierID);
        }
        else
        {
            Supply = new Supply();
            Supply.SupplyID = 0;
            Supply.SupplyDate = DateTime.Now;
            Supply.PurchasePrice = 0;
            SuppliesProducts = new ObservableCollection<SuppliesProducts>();
        }

        AddDishCommand = new RelayCommand(AddDish);
        SaveCommand = new RelayCommand(SaveChanges);
        
        DeleteDishCommand = new RelayCommand(DeleteDish);
        DeleteSupplyCommand = new RelayCommand(DeleteSupply);
    }

}

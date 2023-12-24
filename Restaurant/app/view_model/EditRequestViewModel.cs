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

public class EditRequestViewModel : ViewModelBase
{
    private RestaurantDbContext db;
    private RequestRepository requestRepository;
    private RequestsProductsRepository requestProductRepository;
    private ProductRepository productRepository;
    private DepartmentRepository departmentRepository;
    private WarehouseRepository warehouseRepository;
    private Department selectedDepartment;
    

    public Request Request { get; set; }

    private ObservableCollection<RequestsProducts> requestProduct;
    public ObservableCollection<Product> Products { get; set; }
    public ObservableCollection<Department> Departments { get; set; }
    public ObservableCollection<Warehouse> Warehouses { get; set; }

    public RelayCommand AddProductCommand { get; set; }

    public RelayCommand SaveCommand { get; set; }
    public RelayCommand DeleteProductCommand { get; set; }
    public RelayCommand DeleteRequestCommand { get; set; }

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

    public ObservableCollection<RequestsProducts> RequestsProducts
    {
        get { return requestProduct; }
        set
        {
            requestProduct = value;
            OnPropertyChanged(nameof(RequestsProducts));
        }
    }
    public Department SelectedDepartment
    {
        get => selectedDepartment;
        set
        {
            if (value != null)
            {
                selectedDepartment = value;

                if (Request != null)
                {
                    Request.Department = value;
                    Request.DepartmentID = value.DepartmentID;
                }

                OnPropertyChanged(nameof(SelectedDepartment));
            }
        }
    }
    private void AddProduct(object obj)
    {

        if (SelectedProduct != null && SelectedQuantity > 0)
        {
            Warehouse _warehouse = null;
            foreach (var warehouse in Warehouses)
            {
                if (warehouse.ProductID == SelectedProduct.ProductID)
                    _warehouse = warehouse;
            }
            if (_warehouse != null)
            {
                if (_warehouse.StockBalance > SelectedQuantity)
                {
                    // Проверка существующего ингредиента в коллекции
                    bool flag = false;
                    foreach (var request in RequestsProducts)
                    {
                        if (request.RequestID == Request.RequestID && request.ProductID == SelectedProduct.ProductID)
                        {
                            request.Quantity += SelectedQuantity;
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        // Ингредиент не найден, добавляем новый
                        var newRequestsProducts = new RequestsProducts
                        {
                            RequestID = Request.RequestID,
                            ProductID = SelectedProduct.ProductID,
                            Request = Request,
                            Product = SelectedProduct,
                            Quantity = SelectedQuantity
                        };
                        RequestsProducts.Add(newRequestsProducts);

                    }
                    foreach (var warehouse in Warehouses)
                    {
                        if (warehouse.ProductID == SelectedProduct.ProductID)
                            warehouse.StockBalance -= SelectedQuantity;
                    }
                    RequestsProducts = new ObservableCollection<RequestsProducts>(RequestsProducts);
                    OnPropertyChanged(nameof(RequestsProducts));
                    OnPropertyChanged(nameof(Request));
                }
                else
                {
                    MessageBoxEventArgs args = new MessageBoxEventArgs(null, "На складе нет такого количества.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    args.Show();
                }
            }
            else
            {
                MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Такого продукта на складе нет.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Show();
            }
        }
        else
        {
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, выберите продукт и количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Show();
        }
    }

    private void SaveChanges(object obj)
    {
        if (SelectedDepartment == null || RequestsProducts.Count() == 0)
        {
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Show();
        }
        else
        {
            if (Request.RequestID != 0)
            {
                // Если уже существует, обновляем его данные
                requestRepository.UpdateRequest(Request);

                foreach (var request in RequestsProducts)
                {
                    requestProductRepository.Update(request);
                }
                foreach (var warehouse in Warehouses)
                {
                    warehouseRepository.UpdateWarehouse(warehouse);
                }

            }
            else
            {
                // Иначе добавляем новое блюдо
                int ID = 0;
                if (allRequestsProducts != null)
                {
                    ID = requestRepository.GetMaxRequestId() + 1;
                }
                if (ID == 0)
                    ID = 1;
                Request.RequestID = ID;
                requestRepository.AddRequest(Request);

                foreach (var request in RequestsProducts)
                {
                    request.RequestID = ID;
                    requestProductRepository.Add(request);
                }

                foreach (var warehouse in Warehouses)
                {
                    warehouseRepository.UpdateWarehouse(warehouse);
                }

            }
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Заявка сохранена", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            args.Show();
        }

    }
    private void DeleteProduct(object obj)
    {
        bool flag = false;
        if (SelectedProduct != null && SelectedQuantity > 0)
        {
            List<RequestsProducts> itemsToRemove = new List<RequestsProducts>();

            foreach (var request in RequestsProducts)
            {
                if (request.RequestID == Request.RequestID && request.ProductID == SelectedProduct.ProductID && request.Quantity == SelectedQuantity)
                {
                    itemsToRemove.Add(request);
                    flag = true;
                }
                else if (request.RequestID == Request.RequestID && request.ProductID == SelectedProduct.ProductID && request.Quantity > SelectedQuantity)
                {
                    request.Quantity -= SelectedQuantity;
                    flag = true;
                }
            }

            foreach (var requestToRemove in itemsToRemove)
            {
                RequestsProducts.Remove(requestToRemove);
            }

            if (flag == false)
            {
                MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Некорректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Show();
            }
            else
            {
                foreach (var warehouse in Warehouses)
                {
                    if (warehouse.ProductID == SelectedProduct.ProductID)
                        warehouse.StockBalance += SelectedQuantity;
                }
            }
            RequestsProducts = new ObservableCollection<RequestsProducts>(RequestsProducts);
            OnPropertyChanged(nameof(RequestsProducts));
        }
        else
        {
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, выберите продукт и количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Show();
        }
    }

    void SelectRequestsProducts(ObservableCollection<RequestsProducts> all)
    {
        if (all.Count != 0 && Request != null)
        {
            if (RequestsProducts != null)
            {
                RequestsProducts.Clear();
            }
            else
            {
                RequestsProducts = new ObservableCollection<RequestsProducts>();
            }
            foreach (var request in all)
            {
                if (request.RequestID == Request.RequestID)
                {
                    RequestsProducts.Add(request);
                }
            }
        }
        else
        {
            RequestsProducts = new ObservableCollection<RequestsProducts>();
        }
    }
    private void DeleteRequest(object obj)
    {
        foreach (var warehouse in Warehouses)
        {
            foreach(var requestProducts in RequestsProducts)
            {
                if (warehouse.ProductID == requestProducts.ProductID)
                {
                    warehouse.StockBalance += SelectedQuantity;
                    warehouseRepository.UpdateWarehouse(warehouse);
                }   
            }
        }
        requestProductRepository.Delete(Request.RequestID);
        requestRepository.DeleteRequest(Request.RequestID);
        MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Заявка удалена", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
        args.Show();
    }

    void SelectProducts(ObservableCollection<Product> all)
    {
        if (all.Count != 0)
        {
            if (Products != null)
            {
                Products.Clear();
            }
            else
            {
                Products = new ObservableCollection<Product>();
            }
            foreach (var product in all)
            {
                foreach (var warehouse in Warehouses)
                {
                    if (product.ProductID == warehouse.ProductID)
                    {
                        Products.Add(product);
                    }
                }
            }
        }
        else
        {
            Products = new ObservableCollection<Product>();
        }
    }
    ObservableCollection<RequestsProducts> allRequestsProducts;
    ObservableCollection<Product> allProducts;
    public EditRequestViewModel(Request request)
    {
        db = new RestaurantDbContext();
        Request = request;
        SelectedQuantity = 1;

        requestRepository = new RequestRepository(db);
        requestProductRepository = new RequestsProductsRepository(db);
        productRepository = new ProductRepository(db);
        departmentRepository = new DepartmentRepository(db);
        warehouseRepository = new WarehouseRepository(db);

        allProducts = new ObservableCollection<Product>(productRepository.GetProductsWithUnitOfMeasure());
        allRequestsProducts = new ObservableCollection<RequestsProducts>(requestProductRepository.Get());
        Departments = new ObservableCollection<Department>(departmentRepository.GetDepartments());
        Warehouses = new ObservableCollection<Warehouse>(warehouseRepository.GetWarehouses());
        SelectRequestsProducts(allRequestsProducts);
        SelectProducts(allProducts);

        if (Request == null)
        {
            Request = new Request();
            Request.RequestID = 0;
            Request.RequestDate = DateTime.Now;
            RequestsProducts = new ObservableCollection<RequestsProducts>();
        }
        else
        {
            Request.Department = Departments.FirstOrDefault(i => i.DepartmentID == Request.DepartmentID);
            SelectedDepartment = Request.Department;
        }

        AddProductCommand = new RelayCommand(AddProduct);
        SaveCommand = new RelayCommand(SaveChanges);

        DeleteProductCommand = new RelayCommand(DeleteProduct);
        DeleteRequestCommand = new RelayCommand(DeleteRequest);
    }
}

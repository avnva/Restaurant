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

    public Request Request { get; set; }

    private ObservableCollection<RequestsProducts> requestProduct;
    public ObservableCollection<Product> Products { get; set; }

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
    private void AddProduct(object obj)
    {
        // Проверка существующего ингредиента в коллекции
        if (SelectedProduct != null && SelectedQuantity > 0)
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
            RequestsProducts = new ObservableCollection<RequestsProducts>(RequestsProducts);
            OnPropertyChanged(nameof(RequestsProducts));
            OnPropertyChanged(nameof(Request));
        }
        else
        {
            MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, выберите продукт и количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Show();
        }
    }

    private void SaveChanges(object obj)
    {
        if ( false/*Request.OrderCost == 0 || RequestsProducts.Count() == 0*/)
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
            // Проверка существующего ингредиента в коллекции
            foreach (var request in RequestsProducts)
            {
                if (request.RequestID == Request.RequestID && request.ProductID == SelectedProduct.ProductID && request.Quantity == SelectedQuantity)
                {
                    RequestsProducts.Remove(request);
                    flag = true;
                }
                else if (request.RequestID == Request.RequestID && request.ProductID == SelectedProduct.ProductID && request.Quantity > SelectedQuantity)
                {
                    request.Quantity -= SelectedQuantity;
                    flag = true;
                }

            }
            if (flag == false)
            {
                MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Некорректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Show();
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
    private void DeleteOrder(object obj)
    {

        requestProductRepository.Delete(Request.RequestID);
        requestRepository.DeleteRequest(Request.RequestID);
        MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Заявка удалена", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
        args.Show();
    }
    ObservableCollection<RequestsProducts> allRequestsProducts;
    public EditRequestViewModel(Request request)
    {
        db = new RestaurantDbContext();
        Request = request;
        SelectedQuantity = 1;

        requestRepository = new RequestRepository(db);
        requestProductRepository = new RequestsProductsRepository(db);
        productRepository = new ProductRepository(db);

        Products = new ObservableCollection<Product>(productRepository.GetProductsWithUnitOfMeasure());
        allRequestsProducts = new ObservableCollection<RequestsProducts>(requestProductRepository.Get());
        SelectRequestsProducts(allRequestsProducts);

        if (Request == null)
        {
            Request = new Request();
            Request.RequestID = 0;
            Request.RequestDate = DateTime.Now;
            RequestsProducts = new ObservableCollection<RequestsProducts>();
        }

        AddProductCommand = new RelayCommand(AddProduct);
        SaveCommand = new RelayCommand(SaveChanges);

        DeleteProductCommand = new RelayCommand(DeleteProduct);
        DeleteRequestCommand = new RelayCommand(DeleteOrder);
    }
}

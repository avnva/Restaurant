using Microsoft.EntityFrameworkCore;
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
using System.Windows.Controls;
using System.Windows.Input;

namespace Restaurant
{
    public class EditDishViewModel : ViewModelBase
    {
        private RestaurantDbContext db;

        private StatusRepository statusRepository;
        private DishGroupRepository dishGroupRepository;
        private MenuRepository menuRepository;
        private ProductRepository productRepository;
        private DishRepository dishRepository;
        private DishesProductsRepository dishesProductsRepository;

        private Status selectedStatus;
        private DishGroup selectedDishGroup;
        private Product selectedProduct;

        public Dish Dish { get; set; }
        //public Dish dish;
        public Menu Menu { get; set; }

        public ObservableCollection<Status> Statuses { get; set; }
        public ObservableCollection<DishGroup> DishGroups { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        public RelayCommand AddDishCommand { get; set; }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand DeleteDishCommand { get; set; }


        public Status SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                Menu.StatusId = value.StatusId;
                OnPropertyChanged(nameof(SelectedStatus));
            }
        }

        public DishGroup SelectedDishGroup
        {
            get => selectedDishGroup;
            set
            {
                if (value != null)
                {
                    selectedDishGroup = value;

                    if (Dish != null)
                    {
                        Dish.DishGroup = value;
                        Dish.GroupID = value.GroupId;
                    }

                    OnPropertyChanged(nameof(SelectedDishGroup));
                }
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

        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }
        private ObservableCollection<DishesProducts> dishesProducts;
        public ObservableCollection<DishesProducts> DishesProducts
        {
            get { return dishesProducts; }
            set
            {
                dishesProducts = value;
                OnPropertyChanged(nameof(DishesProducts));
            }
        }

        private void AddDish(object obj)
        {
                // Проверка существующего ингредиента в коллекции
            if(SelectedProduct != null && SelectedQuantity > 0)
            {
                // Проверка существующего ингредиента в коллекции
                bool flag = false;
                foreach (var dish in DishesProducts)
                {
                    if (dish.DishID == Dish.DishID && dish.ProductID == SelectedProduct.ProductID)
                    {
                        dish.Quantity += SelectedQuantity;

                        flag = true;
                    }
                }
                if (!flag)
                {
                    // Ингредиент не найден, добавляем новый
                var newDishProduct = new DishesProducts
                {
                    DishID = Dish.DishID,
                    ProductID = SelectedProduct.ProductID,
                    Dish = Dish,
                    Product = SelectedProduct,
                    Quantity = SelectedQuantity
                };

                DishesProducts.Add(newDishProduct);
                }
                DishesProducts = new ObservableCollection<DishesProducts>(DishesProducts);
                OnPropertyChanged(nameof(DishesProducts));
            }
            else
            {
                MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, выберите продукт и количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Show();
            }
        }
        private void DeleteDish(object obj)
        {
            bool flag = false;
            if (SelectedProduct != null && SelectedQuantity > 0)
            {
                // Проверка существующего ингредиента в коллекции
                foreach (var dish in DishesProducts)
                {
                    if (dish.DishID == Dish.DishID && dish.ProductID == SelectedProduct.ProductID && dish.Quantity == SelectedQuantity)
                    {
                        DishesProducts.Remove(dish);
                        flag = true;
                    }
                    else if (dish.DishID == Dish.DishID && dish.ProductID == SelectedProduct.ProductID && dish.Quantity > SelectedQuantity)
                    {
                        dish.Quantity -= SelectedQuantity;
                        flag = true;
                    }
                    
                }
                if (flag == false)
                {
                    MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Некорректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    args.Show();
                }
                DishesProducts = new ObservableCollection<DishesProducts>(DishesProducts);
                OnPropertyChanged(nameof(DishesProducts));
            }
            else
            {
                MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, выберите продукт и количество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Show();
            }

        }

        private void SaveChanges(object obj)
        {
            if (string.IsNullOrEmpty(Dish.DishName) || Dish.GroupID == 0 || Dish.DishCost == 0 || Dish.OutputWeight == 0 || string.IsNullOrEmpty(Dish.CookingTechnology) || DishesProducts.Count() == 0)
            {
                MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Show();
            }
            else
            {
                if (Dish.DishID != 0)
                {
                    // Если блюдо уже существует, обновляем его данные
                    //Dish.Status = SelectedStatus;
                    dishRepository.UpdateDish(Dish);

                    Menu.StatusId = SelectedStatus.StatusId;
                    menuRepository.UpdateDishInMenu(Menu);

                    foreach (var product in DishesProducts)
                    {
                        // Установите DishID и ProductID перед вызовом Update
                        product.DishID = Dish.DishID;
                        product.ProductID = product.Product.ProductID;

                        // Вызовите Update после установки DishID и ProductID
                        dishesProductsRepository.Update(product);
                    }

                }
                else
                {
                    // Иначе добавляем новое блюдо
                    int ID = dishRepository.GetMaxDishId() + 1;
                    Dish.DishID = ID;
                    dishRepository.AddDish(Dish);

                    int menuID = menuRepository.GetMaxDishId() + 1;
                    Menu menu = new Menu();
                    menu.DishInMenuID = menuID;
                    menu.DishId = ID;
                    menu.StatusId = SelectedStatus.StatusId;
                    menuRepository.AddDishToMenu(menu);

                    foreach(var product in DishesProducts)
                    {
                        dishesProductsRepository.Add(product);
                    }
                    
                }
                MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Блюдо сохранено", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                args.Show();
            }

        }

        void SelectDishesProducts(ObservableCollection<DishesProducts> all)
        {
            if (DishesProducts != null)
            {
                DishesProducts.Clear();
            }
            else
            {
                DishesProducts = new ObservableCollection<DishesProducts>();
            }
            foreach(var dish in all)
            {
                if (dish.DishID == Dish.DishID)
                {
                    DishesProducts.Add(dish);
                }
            }
        }
        public EditDishViewModel(Dish dish)
        {
            db = new RestaurantDbContext();
            Dish = dish;
            Menu = new Menu();
            ObservableCollection<DishesProducts> allDishesProducts;
            SelectedQuantity = 1;

            statusRepository = new StatusRepository(db);
            dishGroupRepository = new DishGroupRepository(db);
            menuRepository = new MenuRepository(db);
            productRepository = new ProductRepository(db);
            dishRepository = new DishRepository(db);
            dishesProductsRepository = new DishesProductsRepository(db);


            Statuses = new ObservableCollection<Status>(statusRepository.GetStatuses());
            DishGroups = new ObservableCollection<DishGroup>(dishGroupRepository.GetDishGroups());
            Products = new ObservableCollection<Product>(productRepository.GetProductsWithUnitOfMeasure());
            allDishesProducts = new ObservableCollection<DishesProducts>(dishesProductsRepository.Get());
            AddDishCommand = new RelayCommand(AddDish);
            SaveCommand = new RelayCommand(SaveChanges);
            SelectDishesProducts(allDishesProducts);
            DeleteDishCommand = new RelayCommand(DeleteDish);
            //Ingredients = new ObservableCollection<DishesProducts>(DishesProducts);

            Dish.DishGroup = DishGroups.FirstOrDefault(i => i.GroupId == Dish.GroupID);
            SelectedDishGroup = Dish.DishGroup;
            if (dish.DishID != null)
            {
                Menu = menuRepository.GetMenu().FirstOrDefault(i => i.DishId == Dish.DishID);
                if (Menu != null)
                {
                    SelectedStatus = Statuses.FirstOrDefault(i => i.StatusId == Menu.StatusId);
                }
                else
                {
                    // Обработка ситуации, когда Menu равен null
                    // Например, установка значения SelectedStatus по умолчанию
                    SelectedStatus = Statuses.FirstOrDefault();
                }
            }
            else
            {
                Menu = new Menu();
                Menu.StatusId = 1;
            }
            //SelectedStatus = Statuses.FirstOrDefault(i => i.StatusId == Menu.StatusId);
        }
    }
}
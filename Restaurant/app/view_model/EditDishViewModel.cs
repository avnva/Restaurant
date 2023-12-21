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
        public Dish dish;
        public Menu menu { get; set; }

        public ObservableCollection<Status> Statuses { get; set; }
        public ObservableCollection<DishGroup> DishGroups { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        public ObservableCollection<DishesProducts> DishesProducts { get; set; }
        public RelayCommand AddDishCommand { get; set; }

        public RelayCommand SaveCommand { get; set; }

        private DishesProducts selectedIngredient;
        public DishesProducts SelectedIngredient
        {
            get { return selectedIngredient; }
            set
            {
                selectedIngredient = value;
                OnPropertyChanged(nameof(SelectedIngredient));
            }
        }

        public Status SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                menu.StatusId = value.StatusId;
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
        private ObservableCollection<DishesProducts> ingredients;
        public ObservableCollection<DishesProducts> Ingredients
        {
            get { return ingredients; }
            set
            {
                ingredients = value;
                OnPropertyChanged(nameof(Ingredients));
            }
        }

        private void AddDish(object obj)
        {
            if (SelectedProduct != null && SelectedQuantity > 0)
            {
                // Проверка существующего ингредиента в коллекции
                var existingIngredient = DishesProducts.FirstOrDefault(d => d.Product.ProductID == SelectedProduct.ProductID);

                if (existingIngredient != null)
                {
                    // Ингредиент уже существует, обновляем его количество
                    existingIngredient.Quantity += SelectedQuantity;
                }
                else
                {
                    // Ингредиент не найден, добавляем новый
                    var newDishProduct = new DishesProducts
                    {
                        Product = SelectedProduct,
                        Quantity = SelectedQuantity
                    };

                    DishesProducts.Add(newDishProduct);
                }

                // Обновление Ingredients после добавления или обновления продукта
                Ingredients = new ObservableCollection<DishesProducts>(DishesProducts);

                dishRepository.UpdateDish(Dish);


            }
        }

        private void SaveChanges(object obj)
        {
            // Логика сохранения изменений в базе данных
            // Вызывается только после нажатия кнопки "Сохранить"
            if (string.IsNullOrEmpty(Dish.DishName) || Dish.GroupID == 0 || Dish.DishCost == 0 || Dish.OutputWeight == 0 || string.IsNullOrEmpty(Dish.CookingTechnology) || Ingredients.Count() == 0)
            {
                MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Show();
            }
            else
            {
                if (Dish.DishID != 0)
                {
                    // Если блюдо уже существует, обновляем его данные
                    dishRepository.UpdateDish(Dish);
                }
                else
                {
                    // Иначе добавляем новое блюдо
                    int ID = dishRepository.GetMaxDishId() + 1;
                    Dish.DishID = ID;
                    dishRepository.AddDish(Dish);
                }
            }

        }

        public EditDishViewModel(Dish dish)
        {
            db = new RestaurantDbContext();
            Dish = dish;
            menu = new Menu();

            statusRepository = new StatusRepository(db);
            dishGroupRepository = new DishGroupRepository(db);
            menuRepository = new MenuRepository(db);
            productRepository = new ProductRepository(db);
            dishRepository = new DishRepository(db);
            dishesProductsRepository = new DishesProductsRepository(db);

            Statuses = new ObservableCollection<Status>(statusRepository.GetStatuses());
            DishGroups = new ObservableCollection<DishGroup>(dishGroupRepository.GetDishGroups());
            Products = new ObservableCollection<Product>(productRepository.GetProductsWithUnitOfMeasure());
            DishesProducts = new ObservableCollection<DishesProducts>(dishesProductsRepository.Get());
            AddDishCommand = new RelayCommand(AddDish);
            SaveCommand = new RelayCommand(SaveChanges);
            Ingredients = new ObservableCollection<DishesProducts>(DishesProducts);

            Dish.DishGroup = DishGroups.FirstOrDefault(i => i.GroupId == Dish.GroupID);
            SelectedDishGroup = Dish.DishGroup;
            if (dish.DishID != null)
            {
                menu = menuRepository.GetMenu().FirstOrDefault(i => i.DishId == Dish.DishID);
            }
            else
            {
                menu = new Menu();
                menu.StatusId = 1;
            }
            SelectedStatus = Statuses.FirstOrDefault(i => i.StatusId == menu.StatusId);
        }
    }
}
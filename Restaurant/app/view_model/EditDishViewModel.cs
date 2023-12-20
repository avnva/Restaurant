using Restaurant.app.model;
using Restaurant.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private Status selectedStatus;
        private DishGroup selectedDishGroup;
        private Product selectedProduct;

        public Dish Dish { get; set; }
        public Menu menu { get; set; }

        public ObservableCollection<Status> Statuses { get; set; }
        public ObservableCollection<DishGroup> DishGroups { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Product> Ingredients { get; set; }
        public ObservableCollection<DishesProducts> DishesProducts { get; set; }

        public RelayCommand SaveCommand { get; set; }

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
                selectedDishGroup = value;
                Dish.DishGroup = value;
                Dish.GroupID = value.GroupId;
                OnPropertyChanged(nameof(SelectedDishGroup));
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

        public EditDishViewModel(Dish dish)
        {
            db = new RestaurantDbContext();
            Dish = dish;
            menu = new Menu();

            statusRepository = new StatusRepository(db);
            dishGroupRepository = new DishGroupRepository(db);
            menuRepository = new MenuRepository(db);
            productRepository = new ProductRepository(db);

            Statuses = new ObservableCollection<Status>(statusRepository.GetStatuses());
            DishGroups = new ObservableCollection<DishGroup>(dishGroupRepository.GetDishGroups());
            Products = new ObservableCollection<Product>(productRepository.GetProducts());

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
            DishesProducts = new ObservableCollection<DishGroup>()
        }
    }
}

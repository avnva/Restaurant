using Microsoft.EntityFrameworkCore;
using Restaurant.app.model;
using Restaurant.app.view;
using Restaurant.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Restaurant
{
    public class DishesPageViewModel : ViewModelBase
    {
        private ObservableCollection<Dish> dishes;
        private DishRepository repository;
        private Dish selectedDish;

        public event Action<Dish> NewDishAdded;

        public ObservableCollection<Dish> Dishes
        {
            get { return dishes; }
            set
            {
                dishes = value;
                OnPropertyChanged(nameof(Dishes));
            }
        }

        public Dish SelectedDish
        {
            get { return selectedDish; }
            set
            {
                selectedDish = value;
                OnPropertyChanged(nameof(SelectedDish));
            }
        }

        public RelayCommand AddNewDishCommand { get; private set; }
        public RelayCommand OpenDishInfoCommand { get; private set; }
        public RelayCommand ReloadCommand { get; private set; }

        public DishesPageViewModel()
        {
            repository = new DishRepository(new RestaurantDbContext());

            ReloadCommand = new RelayCommand(LoadDishes);
            AddNewDishCommand = new RelayCommand(AddNewDish);
            OpenDishInfoCommand = new RelayCommand(OpenDishInfo, CanOpenDishInfo);

            LoadDishes();
        }

        private bool CanOpenDishInfo(object obj)
        {
            return SelectedDish != null;
        }

        private void OpenDishInfo(object obj)
        {
            OnNewDishAdded(SelectedDish);
        }

        private void LoadDishes(object obj = null)
        {
            List<Dish> loadedDishes = repository.GetDishes();
            Dishes = new ObservableCollection<Dish>(loadedDishes);
        }

        private void AddNewDish(object obj)
        {
            OnNewDishAdded(new Dish());
        }

        private void OnNewDishAdded(Dish dish)
        {
            EditDishInfoPage editEmployeeInfoPage = new EditDishInfoPage(dish);
            DataStore.Frame.NavigationService.Navigate(editEmployeeInfoPage);
        }
    }
}

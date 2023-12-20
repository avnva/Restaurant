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

        private Status selectedStatus;
        private DishGroup selectedDishGroup;

        public Dish Dish { get; set; }
        public ObservableCollection<Status> Statuses { get; set; }
        public ObservableCollection<DishGroup> DishGroups { get; set; }

        public Status SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
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

        public EditDishViewModel(Dish dish)
        {
            db = new RestaurantDbContext();
            Dish = dish;

            statusRepository = new StatusRepository(db);
            dishGroupRepository = new DishGroupRepository(db);

            Statuses = new ObservableCollection<Status>(statusRepository.GetStatuses());
            DishGroups = new ObservableCollection<DishGroup>(dishGroupRepository.GetDishGroups());

            Dish.DishGroup = DishGroups.FirstOrDefault(i => i.GroupId == Dish.GroupID);
            SelectedDishGroup = Dish.DishGroup;
        }
    }
}

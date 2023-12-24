using Restaurant.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.app.view_model
{
    public class ExportDocumentViewModel : ViewModelBase
    {
        private ObservableCollection<Supply> supplies;
        private SupplyRepository repository;

        public ObservableCollection<Supply> Supplies
        {
            get { return supplies; }
            set
            {
                supplies = value;
                OnPropertyChanged(nameof(Supplies));
            }
        }
        public RelayCommand ExportExcelCommand { get; set; }
        
        public ExportDocumentViewModel()
        {
            repository = new SupplyRepository(new RestaurantDbContext());

            ExportExcelCommand = new RelayCommand(ExportExcel);
            LoadSupplies();
        }
        private void ExportExcel(object obj)
        {
            repository = new SupplyRepository(new RestaurantDbContext());
            LoadSupplies();
        }

        private void LoadSupplies(object obj = null)
        {
            List<Supply> loadedSupplies = repository.GetSupplies();
            Supplies = new ObservableCollection<Supply>(loadedSupplies);
        }

    }
}

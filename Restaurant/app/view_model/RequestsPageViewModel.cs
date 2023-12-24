using Restaurant.app.model;
using Restaurant.app.view;
using Restaurant.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Restaurant.app.view_model;

public class RequestsPageViewModel : ViewModelBase
{
    private ObservableCollection<Request> requests;
    private RequestRepository repository;
    private Request selectedRequest;

    public event Action<Request> NewRequestAdded;

    public ObservableCollection<Request> Requests
    {
        get { return requests; }
        set
        {
            requests = value;
            OnPropertyChanged(nameof(Requests));
        }
    }

    public Request SelectedRequest
    {
        get { return selectedRequest; }
        set
        {
            selectedRequest = value;
            OnPropertyChanged(nameof(SelectedRequest));
        }
    }

    public RelayCommand AddNewRequestCommand { get; private set; }
    public RelayCommand OpenRequestInfoCommand { get; private set; }
    public RelayCommand ReloadCommand { get; private set; }
    public RelayCommand ReduceGridCommand { get; private set; }

    public RequestsPageViewModel()
    {
        repository = new RequestRepository(new RestaurantDbContext());

        ReloadCommand = new RelayCommand(LoadRequests);
        AddNewRequestCommand = new RelayCommand(AddNewRequest);
        OpenRequestInfoCommand = new RelayCommand(OpenRequestInfo, CanOpenRequestInfo);
        ReduceGridCommand = new RelayCommand(ReduceGrid);

        LoadRequests();
    }
    private void ReduceGrid(object obj)
    {
        repository = new RequestRepository(new RestaurantDbContext());
        LoadRequests();
    }
    private bool CanOpenRequestInfo(object obj)
    {
        return SelectedRequest != null;
    }

    private void OpenRequestInfo(object obj)
    {
        OpenRequestInfoPage(SelectedRequest);
    }

    private void LoadRequests(object obj = null)
    {
        List<Request> loadedRequests = repository.GetRequests();
        Requests = new ObservableCollection<Request>(loadedRequests);
    }

    private void AddNewRequest(object obj)
    {

        OpenRequestInfoPage(null);
    }
    private void OpenRequestInfoPage(Request request)
    {
        EditRequestInfoPage editRequestInfoPage = new EditRequestInfoPage(request);

        DataStore.Frame.NavigationService.Navigate(editRequestInfoPage);
    }
}

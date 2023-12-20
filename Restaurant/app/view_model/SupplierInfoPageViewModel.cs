using Restaurant.app.view;
using Restaurant.repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Restaurant.app.view_model;

public class SupplierInfoPageViewModel : ViewModelBase
{
    private Supplier selectedSupplier;

    public Supplier SelectedSupplier
    {
        get { return selectedSupplier; }
        set
        {
            selectedSupplier = value;
            OnPropertyChanged(nameof(SelectedSupplier));
            OnPropertyChanged(nameof(SelectedSupplierName));
            OnPropertyChanged(nameof(SelectedSupplierAddress));
            OnPropertyChanged(nameof(SelectedSupplierContactPersonName));
            OnPropertyChanged(nameof(SelectedSupplierPhone));
            OnPropertyChanged(nameof(SelectedSupplierBankName));
            OnPropertyChanged(nameof(SelectedSupplierBankAccount));
            OnPropertyChanged(nameof(SelectedSupplierINN));
        }
    }

    public string SelectedSupplierName => SelectedSupplier?.SupplierName;
    public string SelectedSupplierAddress => SelectedSupplier?.Address;
    public string SelectedSupplierContactPersonName => SelectedSupplier?.ContactPersonName;
    public string SelectedSupplierPhone => SelectedSupplier?.Phone;
    public string SelectedSupplierBankName => SelectedSupplier?.BankName;
    public string SelectedSupplierBankAccount => SelectedSupplier?.BankAccount;
    public string SelectedSupplierINN => SelectedSupplier?.INN;

    public SupplierInfoPageViewModel(Supplier supplier)
    {
        selectedSupplier = supplier;
    }

}

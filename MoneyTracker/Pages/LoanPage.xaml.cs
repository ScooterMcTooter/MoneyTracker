using MoneyTracker.ViewModels;
using MoneyTrackerMigrations;
using Microsoft.Maui.Controls;
using System;

namespace MoneyTracker.Pages;

public partial class LoanPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    public LoanPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        BindingContext = _serviceProvider.GetService<LoanViewModel>();
    }

    private void LoanList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem is MoneyTrackerMigrations.Models.LoanModel loan)
            {

                _serviceProvider.GetService<LoanViewModel>().EditSelectedLoanCommand.Execute(loan.Id);
            }
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            return;
        }
    }
}

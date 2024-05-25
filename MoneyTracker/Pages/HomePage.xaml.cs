using CommunityToolkit.Mvvm.Messaging;
using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;

        WeakReferenceMessenger.Default.Register<ScrollToMessage>(this, async (r, m) =>
        {
            await HomeScroll.ScrollToAsync(QuickAddFrame, ScrollToPosition.Start, true);
        });

        WeakReferenceMessenger.Default.Register<ScrollToTop>(this, async (r, m) =>
        {
            await HomeScroll.ScrollToAsync(0, 0, true);
        });
    }
}
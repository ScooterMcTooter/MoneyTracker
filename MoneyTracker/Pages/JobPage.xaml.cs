using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class JobPage : ContentPage
{
	public JobPage(JobViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private async void CompanyInfoButton_Pressed(object sender, EventArgs e)
    {
		await Shell.Current.DisplayAlert(
			"Company Info & Job Type", 
			$"The information we are looking for in the entry is the name of the company the job is for. {Environment.NewLine}{Environment.NewLine}" +
			"The Information we are looking for in the Job Type picker is what kind of job you have.", 
			"OK");
        return;
    }

    private async void TitleInfoButton_Pressed(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert(
            "Company Info & Job Working Location",
            $"The information we are looking for in the entry is the name of the company the job is for. {Environment.NewLine}{Environment.NewLine}" +
            "The Information we are looking for in the job working location is where you are expected to perform your work according to your signed work agreement/contract.",
            "OK");
        return;
    }

    private async void DescriptionInfoButton_Pressed(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert(
            "Job Description",
            $"The information we are looking for in the entry is the name of the company the job is for.",
            "OK");
        return;
    }

    private async void PayInfoButton_Pressed(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert(
            "Job Pay & Weekly Hours",
            $"The information we are looking for in the entry is the pay that you are making on a payday basis. {Environment.NewLine}{Environment.NewLine}" +
            "The Information we are looking for in the job hours is how many hours you are expected to work a week",
            "OK");
        return;
    }
}
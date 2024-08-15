public interface IDialogService
{
    Task<bool> ShowConfirmationDialogAsync(string title, string message, string acceptButton, string cancelButton);
}

public class DialogService : IDialogService
{
    public async Task<bool> ShowConfirmationDialogAsync(string title, string message, string acceptButton, string cancelButton)
    {
        return await Application.Current?.MainPage?.DisplayAlert(title, message, acceptButton, cancelButton);
    }
}

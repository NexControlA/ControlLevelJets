using System.Diagnostics;


namespace ControlLevelJets.Services;

public class DialogContentService : IDialogContentService
{
    public async Task ShowContentDialogAsync(string title, string message,
        string closeButton = "OK")
    {
        var xamlRoot = Window.Current?.Content?.XamlRoot;

        var dialog = new ContentDialog
        {
            Title = title,
            Content = message,
            CloseButtonText = closeButton,
        };

        if (xamlRoot is not null)
            dialog.XamlRoot = xamlRoot;

        await dialog.ShowAsync();
    }

    public async Task<bool> ShowConfirmationDialog(string title, string message, string primaryText = "Confirm",
        string closeButton = "Cancel")
    {
        var xamlRoot = Window.Current?.Content?.XamlRoot;

        var dialog = new ContentDialog()
        {
            Title = title,
            Content = message,
            PrimaryButtonText = primaryText,
            CloseButtonText = closeButton,
            DefaultButton = ContentDialogButton.Primary
        };

        if (xamlRoot is not null)
            dialog.XamlRoot = xamlRoot;

        var result = await dialog.ShowAsync();

        return result == ContentDialogResult.Primary;
    }
}

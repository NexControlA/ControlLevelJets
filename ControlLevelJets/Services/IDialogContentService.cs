namespace ControlLevelJets.Services;

public interface IDialogContentService
{
    Task ShowContentDialogAsync(string title, string message, string closeButton = "OK");
    
    Task<bool> ShowConfirmationDialog(string title, string message,string primaryText ="Confirm", string closeButton = "Cancel");
}

using System.Collections.ObjectModel;
using System.Diagnostics;
using ControlLevelJets.Controls;
using ControlLevelJets.Services;

namespace ControlLevelJets.Presentation;

public partial class HomeViewModel : ObservableObject
{
    private readonly IS7ConnectionService _s7ConnectionService;
    private readonly IDialogContentService _dialogContentService;

    public HomeViewModel(IDialogContentService dialogContentService,
        IS7ConnectionService s7ConnectionService)
    {
        _dialogContentService = dialogContentService;
        _s7ConnectionService = s7ConnectionService;
    }

    public ObservableCollection<JetViewModel> Jets { get; } =
    [
        new() { Name = "Jet 114", },
        new() { Name = "Jet 115", },
        new() { Name = "Jet 109", },
    ];

    [RelayCommand]
    public async Task ConnectS7Station()
    {
        try
        {
            // var panel = new StackPanel { Spacing = 12 };
            //
            // panel.Children.Add(new TextBlock
            // {
            //     Text = "Conectando a controlador S7...",
            //     FontSize = 16,
            // });
            //
            // panel.Children.Add(new ProgressRing { IsActive = true });

            var result = await _dialogContentService.ShowConfirmationDialog("Establish Connection", "are you sure?");

            if (result)
            {
                Debug.WriteLine(result);
                await _s7ConnectionService.ConnectS7Station();
            }

            if (result == false)
            {
                Debug.WriteLine("False");
            }
        }
        catch (Exception ex)
        {
            await _dialogContentService.ShowContentDialogAsync("Connection Error", ex.Message, "Close");
        }
    }
}

using System.Collections.ObjectModel;
using System.Diagnostics;
using ControlLevelJets.Controls;
using ControlLevelJets.Services;
using ControlLevelJets.Presentation;
using ControlLevelJets.Presentation.Interfaces;

namespace ControlLevelJets.Presentation;

public partial class HomeViewModel : ObservableObject, IJetActions
{
    private readonly IS7ConnectionService _s7ConnectionService;
    private readonly IDialogContentService _dialogContentService;

    //Jets Data
    public ObservableCollection<JetViewModel> Jets { get; }


    public HomeViewModel(IDialogContentService dialogContentService,
        IS7ConnectionService s7ConnectionService)
    {
        _dialogContentService = dialogContentService;
        _s7ConnectionService = s7ConnectionService;

        IsConnected = false;

        Jets = [
            new JetViewModel(this)
            {
                Name = "Jet 118",
                SetpointLitersAddress = "DB1.DBD0",
                ResetValuesAddress = "DB1.DBX8.0"
            },
            new JetViewModel(this){
                Name="Jet 107",
                SetpointLitersAddress = "DB1.DBD4",
                ResetValuesAddress = "DB1.DBX8.1"
            },
            new JetViewModel(this){
                Name="Jet 109",
                SetpointLitersAddress = "",
                ResetValuesAddress="",
            }

            ];
    }


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotConnected))]
    private bool _isConnected;

    public bool IsNotConnected => !IsConnected;

    [RelayCommand]
    public async Task ConnectS7Station()
    {
        await _s7ConnectionService.ConnectS7Station();
    }

    public async Task WriteValues(JetViewModel jet)
    {
        Debug.WriteLine($"Test: {jet.Name}");
        await _dialogContentService.ShowConfirmationDialog("dialog test", $"dialog from actions interface {jet.Name}");
    }

    public async Task ResetCurrentValues(JetViewModel jet)
    {
        await _dialogContentService.ShowConfirmationDialog("dialog test", $"dialog from actions interface {jet.Name}");

    }


}

using System.Collections.ObjectModel;
using System.Diagnostics;
using ControlLevelJets.Controls;
using ControlLevelJets.Services;
using ControlLevelJets.Presentation;
using ControlLevelJets.Presentation.Interfaces;
using System.ComponentModel;

namespace ControlLevelJets.Presentation;

public partial class HomeViewModel : ObservableObject, IJetActions
{
    private readonly IS7ConnectionService _s7ConnectionService;
    private readonly IDialogContentService _dialogContentService;

    private readonly IS7WriteService _s7WriteService;
    private readonly IS7ReadService _s7ReadService;

    // Jets data struct
    private const int DbNumber = 3;
    private const int StartByte = 0;


    //Jets Data
    public ObservableCollection<JetViewModel> Jets { get; }


    public HomeViewModel(IDialogContentService dialogContentService,
        IS7ConnectionService s7ConnectionService, IS7WriteService s7WriteService, IS7ReadService s7ReadService)
    {
        _dialogContentService = dialogContentService;
        _s7ConnectionService = s7ConnectionService;
        _s7WriteService = s7WriteService;
        _s7ReadService = s7ReadService;

        Jets = [
            new JetViewModel(this, _s7ReadService.JetModel118)
            {
                Name = "Jet 118",
                SetpointLitersAddress = "DB2.DBW0",
                ResetValuesAddress = "DB2.DBX2.0",

            },
            new JetViewModel(this, _s7ReadService.JetModel107){
                Name="Jet 107",
                SetpointLitersAddress = "DB7.DBW0",
                ResetValuesAddress = "DB7.DBX2.0"
            },
            new JetViewModel(this, _s7ReadService.JetModel109){
                Name="Jet 109",
                SetpointLitersAddress = "DB5.DBW0",
                ResetValuesAddress="DB5.DBX2.0"
            }

            ];


    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotConnected))]
    [NotifyCanExecuteChangedFor(nameof(ConnectS7StationCommand))]
    private bool _isConnected;


    public bool IsNotConnected => !IsConnected;

    [RelayCommand(CanExecute = nameof(IsNotConnected))]
    public async Task ConnectS7Station()
    {
        IsConnected = await _s7ConnectionService.ConnectS7Station();
        Debug.WriteLine($"Value of IsConnected {IsConnected}");

        if (IsConnected)
        {
            // Start reading the jets data struct
            _ = _s7ReadService.ReadJetsStruct(DbNumber, StartByte, IsConnected);
        }
    }

    public async Task WriteValues(JetViewModel jet)
    {
        var taskOne = _s7WriteService.WriteSetpoint(jet.SetpointLitersAddress, jet.DesiredLiters);
        var taskTwo = _s7WriteService.ResetValues(jet.ResetValuesAddress, false);
        await Task.WhenAll(taskOne, taskTwo);

    }

    public async Task ResetCurrentValues(JetViewModel jet)
    {
        await _s7WriteService.ResetValues(jet.ResetValuesAddress, true);
    }

}

using Microsoft.UI.Dispatching;
using Windows.System;

namespace ControlLevelJets.Services;

public partial class S7ReadService : ObservableObject, IS7ReadService
{
    private readonly IS7ConnectionService _s7ConnectionService;
    private readonly IDialogContentService _dialogContentService;
    private readonly HomeViewModel _homeViewModel;

    // Data Struct
    //Jet 118
    [ObservableProperty] private short _setpointLiters118;
    [ObservableProperty] private float _currentLiters118;
    [ObservableProperty] private bool _valveStatus118;

    // Jet 107
    [ObservableProperty] private short _setpointLiters107;
    [ObservableProperty] private float _currentLiters107;
    [ObservableProperty] private bool _valveStatus107;

    // Jet 107
    [ObservableProperty] private short _setpointLiters109;
    [ObservableProperty] private float _currentLiters109;
    [ObservableProperty] private bool _valveStatus109;


    public S7ReadService(IS7ConnectionService s7ConnectionService,
        IDialogContentService dialogContentService,
        HomeViewModel homeViewModel)
    {
        _s7ConnectionService = s7ConnectionService;
        _dialogContentService = dialogContentService;
        _homeViewModel = homeViewModel;
    }


    public async Task ReadJetsStruct(int dbNumber, int startByte)
    {
        var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();


        while (_homeViewModel.IsConnected)
        {
            try
            {
                JetData readValues = (JetData)await _s7ConnectionService.PlcStation.ReadStructAsync<JetData>(
            dbNumber,
            startByte);



                dispatcherQueue.TryEnqueue(() =>
                {
                    SetpointLiters118 = readValues.SetpointLiters118;
                    CurrentLiters118 = readValues.CurrentLiters118;
                    ValveStatus118 = readValues.ValveStatus118;

                    SetpointLiters107 = readValues.SetpointLiters107;
                    CurrentLiters107 = readValues.CurrentLiters107;
                    ValveStatus107 = readValues.ValveStatus107;

                    SetpointLiters109 = readValues.SetpointLiters109;
                    CurrentLiters109 = readValues.CurrentLiters109;
                    ValveStatus109 = readValues.ValveStatus109;
                });


            }
            catch
            {
                await _dialogContentService.ShowContentDialogAsync("Error", "Error reading data from PLC. Please check the connection and try again.", "OK");
            }
        }

        await Task.Delay(500);


    }



}

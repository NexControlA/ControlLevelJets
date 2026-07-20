using Microsoft.UI.Dispatching;
using Windows.System;

namespace ControlLevelJets.Services;

public partial class S7ReadService : ObservableObject, IS7ReadService
{
    private readonly IS7ConnectionService _s7ConnectionService;
    private readonly IDialogContentService _dialogContentService;

    public JetModel JetModel118 { get; } = new();
    public JetModel JetModel107 { get; } = new();
    public JetModel JetModel109 { get; } = new();


    public S7ReadService(IS7ConnectionService s7ConnectionService,
        IDialogContentService dialogContentService)
    {
        _s7ConnectionService = s7ConnectionService;
        _dialogContentService = dialogContentService;
    }


    public async Task ReadJetsStruct(int dbNumber, int startByte, bool isConnected)
    {
        var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();

        while (isConnected)
        {
            try
            {
                JetData readValues = (JetData)await _s7ConnectionService.PlcStation.ReadStructAsync<JetData>(
            dbNumber,
            startByte);

                dispatcherQueue.TryEnqueue(() =>
                {
                    JetModel118.SetpointLiters = readValues.SetpointLiters118;
                    JetModel118.CurrentLiters = readValues.CurrentLiters118;
                    JetModel118.ValveStatus = readValues.ValveStatus118;

                    JetModel107.SetpointLiters = readValues.SetpointLiters107;
                    JetModel107.CurrentLiters = readValues.CurrentLiters107;
                    JetModel107.ValveStatus = readValues.ValveStatus107;

                    JetModel109.SetpointLiters = readValues.SetpointLiters109;
                    JetModel109.CurrentLiters = readValues.CurrentLiters109;
                    JetModel109.ValveStatus = readValues.ValveStatus109;
                });
            }
            catch
            {
                await _dialogContentService.ShowContentDialogAsync("Error", "Error reading data from PLC. Please check the connection and try again.", "OK");

            }
            await Task.Delay(TimeSpan.FromSeconds(3));
        }
    }
}

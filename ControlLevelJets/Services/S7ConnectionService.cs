using S7.Net;

namespace ControlLevelJets.Services;

public partial class S7ConnectionService : ObservableObject, IS7ConnectionService
{
    private const string PlcIpAddress = "192.168.20.60";
    private const CpuType PlcCpuType = CpuType.S71200;
    private const short PlcRackId = 0;
    private const short PlcSlotId = 1;

    private Plc? PlcStation;

    private readonly IDialogContentService _dialogContentService;

    public S7ConnectionService(IDialogContentService dialogContentService)
    {
        _dialogContentService = dialogContentService;
    }

    public async Task ConnectS7Station()
    {
        var dialogResult = await _dialogContentService.ShowConfirmationDialog("Establish Connection", "Are you sure you want to connect to the S7 station?", "Connect");

        if (dialogResult)
        {
            try
            {
                PlcStation = new Plc(PlcCpuType, PlcIpAddress, PlcRackId, PlcSlotId);

                await PlcStation.OpenAsync();
            }
            catch (Exception ex)
            {
                await _dialogContentService.ShowContentDialogAsync("Connection Error", $"Failed to connect to the S7 station: {ex.Message}");
            }
        }

    }

    public Task DisconnectS7Station()
    {
        throw new NotImplementedException();
    }
}

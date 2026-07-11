using S7.Net;

namespace ControlLevelJets.Services;

public partial class S7ConnectionService : ObservableObject, IS7ConnectionService
{
    private const string PlcIpAddress = "192.168.20.50";
    private const CpuType PlcCpuType = CpuType.S71200;
    private const short PlcRackId = 0;
    private const short PlcSlotId = 1;

    public Plc? PlcStation;

    public async Task ConnectS7Station()
    {

        PlcStation = new Plc(PlcCpuType, PlcIpAddress, PlcRackId, PlcSlotId);

        await PlcStation.OpenAsync();

    }
}

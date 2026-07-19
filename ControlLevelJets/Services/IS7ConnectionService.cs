using S7.Net;

namespace ControlLevelJets.Services;

public interface IS7ConnectionService
{
    Plc? PlcStation { get; }

    Task<bool> ConnectS7Station();
    Task DisconnectS7Station();
}

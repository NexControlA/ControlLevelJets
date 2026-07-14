using S7.Net;

namespace ControlLevelJets.Services;

public interface IS7ConnectionService
{
    Task ConnectS7Station();
    Task DisconnectS7Station();
}

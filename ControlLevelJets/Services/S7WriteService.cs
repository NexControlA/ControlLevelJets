using System;
using System.Collections.Generic;
using System.Text;

namespace ControlLevelJets.Services;

public partial class S7WriteService : ObservableObject, IS7WriteService
{
    private readonly IS7ConnectionService _connectionService;

    public S7WriteService(IS7ConnectionService s7ConnectionService)
    {
        _connectionService = s7ConnectionService;
    }
    public async Task ResetValues(string resetPlcAddress, bool value)
    {
        await _connectionService.PlcStation.WriteAsync(resetPlcAddress, value);
    }

    public async Task WriteSetpoint(string litersPlcAddress, short value)
    {
        await _connectionService.PlcStation.WriteAsync(litersPlcAddress, value);
    }
}

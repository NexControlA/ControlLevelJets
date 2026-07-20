
using System.ComponentModel;

namespace ControlLevelJets.Services;

public interface IS7ReadService
{
    JetModel JetModel118 { get; }
    JetModel JetModel107 { get; }
    JetModel JetModel109 { get; }
    Task ReadJetsStruct(int dbNumber, int startByte, bool isConnected);
}

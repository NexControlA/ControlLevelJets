
namespace ControlLevelJets.Services;

public interface IS7ReadService
{
    Task ReadJetsStruct(int dbNumber, int startByte);
}

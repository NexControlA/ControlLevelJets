namespace ControlLevelJets.Services;

public interface IS7WriteService
{
    Task ResetValues(string resetPlcAddress, bool value);
    Task WriteSetpoint(string litersPlcAddress, short value);
}

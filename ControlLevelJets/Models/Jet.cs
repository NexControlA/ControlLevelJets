namespace ControlLevelJets.Models;

public class Jet
{
    public string Name { get; set; } = string.Empty;
    public double DesiredLiters { get; set; }
    public double TotalLiters { get; set; }
    public bool ValveOpen { get; set; }
}

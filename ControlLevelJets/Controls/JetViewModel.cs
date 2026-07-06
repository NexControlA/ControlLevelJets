using System.ComponentModel;

namespace ControlLevelJets.Controls;

public partial class JetViewModel : ObservableObject
{
    [ObservableProperty] private string _name = "";

    [ObservableProperty] private double _desiredLiters;

    [ObservableProperty] private double _totalLiters;

    [ObservableProperty] private bool _valveState = false;
    
    [ObservableProperty] private string _valveStatus = string.Empty;

    public JetViewModel()
    {
       ValveStatus = ValveState ?  "Abierta" : "Cerrada";
        
    }

    [RelayCommand]
    private void WriteValues()
    {
        // escribir al PLC
    }

    [RelayCommand]
    private void ResetValues()
    {
        // reset
    }

    partial void OnValveStateChanged(bool value)
    {
       ValveStatus = value ?  "Abierta" : "Cerrada";
    }
}

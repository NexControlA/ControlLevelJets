using System.ComponentModel;
using ControlLevelJets.Presentation.Interfaces;

namespace ControlLevelJets.Controls;

public partial class JetViewModel : ObservableObject
{
    [ObservableProperty] private string _name = string.Empty;

    [ObservableProperty] private double _desiredLiters;

    [ObservableProperty] private double _totalLiters;

    [ObservableProperty] private bool _valveState = false;
    
    [ObservableProperty] private string _valveStatus = string.Empty;

    // PLC Addresses
    [ObservableProperty] private string _setpointLitersAddress = string.Empty;
    [ObservableProperty] private string _resetValuesAddress = string.Empty;

    private readonly IJetActions _jetActions;

    public JetViewModel(IJetActions jetActions)
    {
       ValveStatus = ValveState ?  "Abierta" : "Cerrada";

        _jetActions = jetActions;

    }

    [RelayCommand]
    private void WriteSetpoints() => _jetActions.WriteValues(this);

    [RelayCommand]
    private void ResetCurrentValues()=> _jetActions.ResetCurrentValues(this);
}

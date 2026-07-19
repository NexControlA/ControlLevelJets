using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ControlLevelJets.Presentation.Interfaces;
using ControlLevelJets.Services;

namespace ControlLevelJets.Controls;

public partial class JetViewModel : ObservableObject
{
    [ObservableProperty] private string _name;

    [ObservableProperty] private short _desiredLiters;

    [ObservableProperty] private double _totalLiters;

    [ObservableProperty] private bool _valveState;

    [ObservableProperty] private string _valveStatus;

    // PLC Addresses
    [ObservableProperty] private string _setpointLitersAddress = string.Empty;
    [ObservableProperty] private string _resetValuesAddress = string.Empty;

    // Properties for data binding from Read Current Values
    [ObservableProperty] private short _setpointLiters;

    private readonly IJetActions _jetActions;

    public JetViewModel(IJetActions jetActions)
    {

        _jetActions = jetActions;

        // Initialize properties values
        //Name = string.Empty;
        //DesiredLiters = 0;
        //TotalLiters = 0;
        //ValveState = false;
        //ValveStatus = ValveState ? "Abierta" : "Cerrada";

    }

    [RelayCommand]
    private void WriteSetpoints() => _jetActions.WriteValues(this);

    [RelayCommand]
    private void ResetCurrentValues() => _jetActions.ResetCurrentValues(this);
}

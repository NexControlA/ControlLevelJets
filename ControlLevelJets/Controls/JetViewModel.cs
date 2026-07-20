using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using ControlLevelJets.Presentation.Interfaces;
using ControlLevelJets.Services;

namespace ControlLevelJets.Controls;

public partial class JetViewModel : ObservableObject
{
    [ObservableProperty] private string _name;
    [ObservableProperty] private short _desiredLiters;
    [ObservableProperty] private string _valveStatus;

    // PLC Addresses
    [ObservableProperty] private string _setpointLitersAddress = string.Empty;
    [ObservableProperty] private string _resetValuesAddress = string.Empty;

    // Properties for data binding from Read Current Values
    public short SetpointL => Model.SetpointLiters;
    public float TotalLiters => Model.CurrentLiters;
    public bool ValveState => Model.ValveStatus;

    private readonly IJetActions _jetActions;
    private readonly JetModel Model;

    public JetViewModel(IJetActions jetActions, JetModel model)
    {

        _jetActions = jetActions;
        Model = model;

        // Initialize properties values
        Name = string.Empty;
        DesiredLiters = 0;
        ValveStatus = ValveState ? "Abierta" : "Cerrada";

        Model.PropertyChanged += OnModelChanged;

    }

    [RelayCommand]
    private void WriteSetpoints() => _jetActions.WriteValues(this);

    [RelayCommand]
    private void ResetCurrentValues() => _jetActions.ResetCurrentValues(this);

    public void OnModelChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Model.SetpointLiters))
        {
            OnPropertyChanged(nameof(SetpointL));
            Debug.WriteLine($"Value of setpointL {SetpointL}");
        }

        if (e.PropertyName == nameof(Model.CurrentLiters))
        {
            OnPropertyChanged(nameof(TotalLiters));
            Debug.WriteLine($"Value of totalLiters {TotalLiters}");
        }

        if (e.PropertyName == nameof(Model.ValveStatus))
        {
            OnPropertyChanged(nameof(ValveState));
            Debug.WriteLine($"Value of valveState {ValveState}");
            ValveStatus = ValveState ? "Abierta" : "Cerrada";
        }

    }
}

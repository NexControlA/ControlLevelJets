using System;
using System.Collections.Generic;
using System.Text;

namespace ControlLevelJets.Models;

public partial class JetModel : ObservableObject
{
    [ObservableProperty] private short _setpointLiters;
    [ObservableProperty] private float _currentLiters;
    [ObservableProperty] private bool _valveStatus;
}

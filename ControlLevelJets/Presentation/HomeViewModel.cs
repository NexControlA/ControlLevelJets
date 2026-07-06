using System.Collections.ObjectModel;
using ControlLevelJets.Controls;

namespace ControlLevelJets.Presentation;

public partial class HomeViewModel : ObservableObject
{
    public ObservableCollection<JetViewModel> Jets { get; } =
    [
        new (){ Name =  "Jet 114",},
        new (){ Name =  "Jet 115",},
        new (){ Name =  "Jet 109",},
    ];
}

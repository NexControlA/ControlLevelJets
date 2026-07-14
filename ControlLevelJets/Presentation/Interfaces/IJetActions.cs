using System;
using System.Collections.Generic;
using System.Text;
using ControlLevelJets.Controls;

namespace ControlLevelJets.Presentation.Interfaces;

public interface IJetActions
{
    Task WriteValues(JetViewModel jet);
    Task ResetCurrentValues(JetViewModel jet);
}

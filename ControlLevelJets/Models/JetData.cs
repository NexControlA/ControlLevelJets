using System.Runtime.InteropServices;

namespace ControlLevelJets.Models;

[StructLayout(LayoutKind.Explicit)]
public struct JetData
{
    // Jet 118
    [FieldOffset(0)] public short SetpointLiters118;
    [FieldOffset(2)] public float CurrentLiters118;
    [FieldOffset(6)] public bool ValveStatus118;
    [FieldOffset(7)] public byte pad7;

    // Jet 107
    [FieldOffset(8)] public short SetpointLiters107;
    [FieldOffset(10)] public float CurrentLiters107;
    [FieldOffset(14)] public bool ValveStatus107;
    [FieldOffset(15)] public byte pad15;

    // Jet 109
    [FieldOffset(16)] public short SetpointLiters109;
    [FieldOffset(18)] public float CurrentLiters109;
    [FieldOffset(22)] public bool ValveStatus109;
    [FieldOffset(23)] public byte pad23;
}

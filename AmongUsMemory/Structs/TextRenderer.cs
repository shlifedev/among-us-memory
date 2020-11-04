using System;
using System.Runtime.InteropServices;

[System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
public struct TextRenderer
{
    [System.Runtime.InteropServices.FieldOffset(0x10)]   public float scale;
    [System.Runtime.InteropServices.FieldOffset(0x30)]   public Color Color; 
    [System.Runtime.InteropServices.FieldOffset(0x84)]   public uint _;
}
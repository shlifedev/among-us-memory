using System; 
 using System.Runtime.InteropServices;

 [System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
public struct LightSource{
[System.Runtime.InteropServices.FieldOffset(8)]	public uint m_CachedPtr;
[System.Runtime.InteropServices.FieldOffset(12)]	public IntPtr child;
[System.Runtime.InteropServices.FieldOffset(16)]	public IntPtr requiredDels;
[System.Runtime.InteropServices.FieldOffset(20)]	public IntPtr myMesh;
[System.Runtime.InteropServices.FieldOffset(24)]	public uint MinRays;
[System.Runtime.InteropServices.FieldOffset(28)]	public float LightRadius;
[System.Runtime.InteropServices.FieldOffset(32)]	public IntPtr Material;
[System.Runtime.InteropServices.FieldOffset(36)]	public IntPtr verts;
[System.Runtime.InteropServices.FieldOffset(40)]	public uint vertCount;
[System.Runtime.InteropServices.FieldOffset(44)]	public IntPtr buffer;
[System.Runtime.InteropServices.FieldOffset(48)]	public IntPtr hits;
[System.Runtime.InteropServices.FieldOffset(52)]	public uint filter;
[System.Runtime.InteropServices.FieldOffset(80)]	public IntPtr vec;
[System.Runtime.InteropServices.FieldOffset(84)]	public IntPtr uvs;
[System.Runtime.InteropServices.FieldOffset(88)]	public IntPtr triangles;
[System.Runtime.InteropServices.FieldOffset(92)]	public float tol;
[System.Runtime.InteropServices.FieldOffset(96)]	public uint del;
[System.Runtime.InteropServices.FieldOffset(104)]	public uint tan;
[System.Runtime.InteropServices.FieldOffset(112)]	public uint side;
[System.Runtime.InteropServices.FieldOffset(120)]	public IntPtr lightHits;
}
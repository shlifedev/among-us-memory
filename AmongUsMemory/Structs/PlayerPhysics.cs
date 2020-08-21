using System; 
 using System.Runtime.InteropServices;

 [System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
public struct PlayerPhysics{
[System.Runtime.InteropServices.FieldOffset(8)]	public uint m_CachedPtr;
[System.Runtime.InteropServices.FieldOffset(12)]	public uint SpawnId;
[System.Runtime.InteropServices.FieldOffset(16)]	public uint NetId;
[System.Runtime.InteropServices.FieldOffset(20)]	public uint DirtyBits;
[System.Runtime.InteropServices.FieldOffset(24)]	public uint SpawnFlags;
[System.Runtime.InteropServices.FieldOffset(25)]	public uint sendMode;
[System.Runtime.InteropServices.FieldOffset(28)]	public uint OwnerId;
[System.Runtime.InteropServices.FieldOffset(32)]	public byte DespawnOnDestroy;
[System.Runtime.InteropServices.FieldOffset(36)]	public float Speed;
[System.Runtime.InteropServices.FieldOffset(40)]	public float GhostSpeed;
[System.Runtime.InteropServices.FieldOffset(44)]	public IntPtr body;
[System.Runtime.InteropServices.FieldOffset(48)]	public IntPtr Animator;
[System.Runtime.InteropServices.FieldOffset(52)]	public IntPtr rend;
[System.Runtime.InteropServices.FieldOffset(56)]	public IntPtr myPlayer;
[System.Runtime.InteropServices.FieldOffset(60)]	public IntPtr RunAnim;
[System.Runtime.InteropServices.FieldOffset(64)]	public IntPtr IdleAnim;
[System.Runtime.InteropServices.FieldOffset(68)]	public IntPtr GhostIdleAnim;
[System.Runtime.InteropServices.FieldOffset(72)]	public IntPtr EnterVentAnim;
[System.Runtime.InteropServices.FieldOffset(76)]	public IntPtr ExitVentAnim;
[System.Runtime.InteropServices.FieldOffset(80)]	public IntPtr SpawnAnim;
[System.Runtime.InteropServices.FieldOffset(84)]	public IntPtr Skin;
}
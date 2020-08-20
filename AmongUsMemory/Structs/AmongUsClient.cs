using System; 
 using System.Runtime.InteropServices;

 [System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
public struct AmongUsClient{
[System.Runtime.InteropServices.FieldOffset(8)]	public uint m_CachedPtr;
[System.Runtime.InteropServices.FieldOffset(12)]	public float MinSendInterval;
[System.Runtime.InteropServices.FieldOffset(16)]	public uint NetIdCnt;
[System.Runtime.InteropServices.FieldOffset(20)]	public float timer;
[System.Runtime.InteropServices.FieldOffset(24)]	public IntPtr SpawnableObjects;
[System.Runtime.InteropServices.FieldOffset(28)]	public byte InOnlineScene;
[System.Runtime.InteropServices.FieldOffset(32)]	public IntPtr DestroyedObjects;
[System.Runtime.InteropServices.FieldOffset(36)]	public IntPtr allObjects;
[System.Runtime.InteropServices.FieldOffset(40)]	public IntPtr allObjectsFast;
[System.Runtime.InteropServices.FieldOffset(44)]	public IntPtr Streams;
[System.Runtime.InteropServices.FieldOffset(48)]	public IntPtr networkAddress;
[System.Runtime.InteropServices.FieldOffset(52)]	public uint networkPort;
[System.Runtime.InteropServices.FieldOffset(56)]	public IntPtr connection;
[System.Runtime.InteropServices.FieldOffset(60)]	public uint mode;
[System.Runtime.InteropServices.FieldOffset(64)]	public uint GameId;
[System.Runtime.InteropServices.FieldOffset(68)]	public uint HostId;
[System.Runtime.InteropServices.FieldOffset(72)]	public uint ClientId;
[System.Runtime.InteropServices.FieldOffset(76)]	public IntPtr allClients;
[System.Runtime.InteropServices.FieldOffset(80)]	public uint LastDisconnectReason;
[System.Runtime.InteropServices.FieldOffset(84)]	public IntPtr LastCustomDisconnect;
[System.Runtime.InteropServices.FieldOffset(88)]	public IntPtr PreSpawnDispatcher;
[System.Runtime.InteropServices.FieldOffset(92)]	public IntPtr Dispatcher;
[System.Runtime.InteropServices.FieldOffset(96)]	public byte IsGamePublic;
[System.Runtime.InteropServices.FieldOffset(100)]	public uint GameState;
[System.Runtime.InteropServices.FieldOffset(104)]	public IntPtr TempQueue;
[System.Runtime.InteropServices.FieldOffset(108)]	public byte appPaused;
[System.Runtime.InteropServices.FieldOffset(112)]	public uint AutoOpenStore;
[System.Runtime.InteropServices.FieldOffset(116)]	public uint GameMode;
[System.Runtime.InteropServices.FieldOffset(120)]	public IntPtr OnlineScene;
[System.Runtime.InteropServices.FieldOffset(124)]	public IntPtr MainMenuScene;
[System.Runtime.InteropServices.FieldOffset(128)]	public IntPtr GameDataPrefab;
[System.Runtime.InteropServices.FieldOffset(132)]	public IntPtr PlayerPrefab;
[System.Runtime.InteropServices.FieldOffset(136)]	public IntPtr ShipPrefabs;
[System.Runtime.InteropServices.FieldOffset(140)]	public uint TutorialMapId;
[System.Runtime.InteropServices.FieldOffset(144)]	public float SpawnRadius;
[System.Runtime.InteropServices.FieldOffset(148)]	public uint discoverState;
[System.Runtime.InteropServices.FieldOffset(152)]	public IntPtr DisconnectHandlers;
[System.Runtime.InteropServices.FieldOffset(156)]	public IntPtr GameListHandlers;
}
namespace HamsterCheese.AmongUsMemory
{
    public sealed class Pattern
    {
        public static string PlayerControl_Pointer = "GameAssembly.dll+1C57F7C";
        public static string ShipStatus_Pointer = "GameAssembly.dll+1C56C38";
        public static string AmongusClient_Pointer = "GameAssembly.dll+1C57F54";

        public static string GameCode_Field_Pointer = "GameAssembly.dll+1af20fc,0x5C,0,0x20,0x28"; // gameCode: [28254460, 92, 0, 32, 40],
        public static string MeetingHud_State_Field_Pointer = "GameAssembly.dll+1c573a4,0x5C,0,0x8,0x84,0x0"; // meetingHud: [29717412, 92, 0], meetingHudCachePtr: [8], meetingHudState: [132],

        public static string PlayerControl_GetData = "55 8B EC 80 3D A9 D1 ??";
    }
} 
  

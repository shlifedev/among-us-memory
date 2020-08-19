

using System;
using System.Linq;

namespace HamsterCheese.AmongUsMemory
{
    public class PlayerData
    {
        public PlayerControll Instance;
         
        public IntPtr PlayerControl_GetData_Offset = IntPtr.Zero;


        private string playerInfoOffset = null;
        public IntPtr playerInfoOffset_ptr;


        public IntPtr offset_ptr;
        public string offset_str;

        /// <summary>
        /// PlayerInfo 가져오기 
        /// </summary>
        public PlayerInfo? PlayerInfo
        {
            get
            {
                if (m_pInfo == null)
                {
                    if (PlayerControl_GetData_Offset == IntPtr.Zero)
                    {
                        var aobScan = Cheese.mem.AoBScan(Pattern.PlayerControl_GetData);

                        aobScan.Wait();
                        if (aobScan.Result.Count() == 1)
                            PlayerControl_GetData_Offset = (IntPtr)aobScan.Result.First();
                    }

                    var scanTask = PlayerControl_GetData_Offset;


                    if (PlayerControl_GetData_Offset != IntPtr.Zero)
                    {
                        var ptr = PlayerControl_GetData_Offset;
                        var playerInfoAddress = Cheese.ProcessMemory.CallFunction(ptr, this.offset_ptr);
                        playerInfoOffset = playerInfoAddress.GetAddress();
                        playerInfoOffset_ptr = (IntPtr)playerInfoAddress;


                        PlayerInfo pInfo = Utils.FromBytes<PlayerInfo>(Cheese.mem.ReadBytes(playerInfoOffset, Utils.SizeOf<PlayerInfo>()));
                        this.m_pInfo = pInfo;
                    }
                }
                else
                {
                    PlayerInfo pInfo =  Utils.FromBytes<PlayerInfo>(Cheese.mem.ReadBytes(playerInfoOffset, Utils.SizeOf<PlayerInfo>()));
                    this.m_pInfo = pInfo;
                }


                return m_pInfo;
            }
        }
        private PlayerInfo? m_pInfo = null;


    }
}
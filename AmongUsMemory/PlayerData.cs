

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

        public Vector2 GetSyncPosition()
        {
            try
            {
                int _offset_vec2_position = 60;
                int _offset_vec2_sizeOf = 8;
                var netTransform = ((int)Instance.NetTransform + _offset_vec2_position).ToString("X");
                var vec2Data= Cheese.mem.ReadBytes($"{netTransform}",_offset_vec2_sizeOf); // 주소로부터 8바이트 읽는다   
                if (vec2Data != null && vec2Data.Length != 0)
                {
                    var vec2 = Utils.FromBytes<Vector2>(vec2Data);
                    return vec2;
                }
                else
                {
                    return Vector2.Zero;
                }
            }


            catch (Exception e)
            {
                Console.WriteLine(e);
                return Vector2.Zero;
            }
        }
        public Vector2 GetMyPosition()
        {
            try
            {
                int _offset_vec2_position = 80;
                int _offset_vec2_sizeOf = 8;
                var netTransform = ((int)Instance.NetTransform + _offset_vec2_position).ToString("X");
                var vec2Data= Cheese.mem.ReadBytes($"{netTransform}",_offset_vec2_sizeOf); // 주소로부터 8바이트 읽는다  
                if (vec2Data != null && vec2Data.Length != 0)
                {
                    var vec2 = Utils.FromBytes<Vector2>(vec2Data);
                    return vec2;
                }
                else
                {
                    return Vector2.Zero;
                }
            }
            catch
            {
                return Vector2.Zero;
            }
        }



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
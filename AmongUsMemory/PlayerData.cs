

using System;
using System.Linq;

namespace HamsterCheese.AmongUsMemory
{
    public class PlayerData
    {
        #region ObserveStates
        private bool observe_dieFlag = false;
        #endregion

        public PlayerControll Instance;
        public System.Action<Vector2, byte> onDie; 
        public IntPtr PlayerControl_GetData_Offset = IntPtr.Zero;


        private string playerInfoOffset = null;
        public IntPtr playerInfoOffset_ptr;
        public IntPtr offset_ptr;
        public string offset_str;


        public void WriteMemory_Imposter(byte value)
        {
            var targetPointer = playerInfoOffset_ptr.Sum(40); 
            Cheese.mem.WriteMemory(targetPointer.GetAddress(), "byte", value.ToString());
        }
        /// <summary>
        /// Set Player Dead State.
        /// </summary>
        /// <param name="value"></param>
        public void WriteMemory_IsDead(byte value)
        {
            var targetPointer = playerInfoOffset_ptr.Sum(41);
            Cheese.mem.WriteMemory(targetPointer.GetAddress(), "byte", value.ToString());
        }
        /// <summary>
        /// Set Player KillTimer
        /// </summary>
        /// <param name="value"></param>
        public void WriteMemory_KillTimer(float value)
        {
            var targetPointer = offset_ptr.Sum(44); 
            Cheese.mem.WriteMemory(targetPointer.GetAddress(), "float", value.ToString());
        }


        public void ObserveState()
        {
            if (PlayerInfo.HasValue)
            {
                if (observe_dieFlag == false && PlayerInfo.Value.IsDead == 1)
                {
                    observe_dieFlag = true;
                    onDie?.Invoke(Position, PlayerInfo.Value.ColorId);
                }
            }
        }

        public Vector2 Position
        {
            get
            {
                if (IsLocalPlayer)
                    return GetMyPosition();
                else
                    return GetSyncPosition();
            }
        }

        public void ReadMemory()
        {
            Instance = Utils.FromBytes<PlayerControll>(Cheese.mem.ReadBytes(offset_str, Utils.SizeOf<PlayerControll>()));
        }

        public bool IsLocalPlayer
        {
            get
            {
                if (Instance.myLight == IntPtr.Zero) return false;
                else
                {
                    return true;
                }
            }
        }


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
                    playerInfoOffset = Methods.Call_PlayerControl_GetData(this.offset_ptr).GetAddress();
                    Console.WriteLine(playerInfoOffset);
                    PlayerInfo pInfo = Utils.FromBytes<PlayerInfo>(Cheese.mem.ReadBytes(playerInfoOffset, Utils.SizeOf<PlayerInfo>()));
                    m_pInfo = pInfo;
                } 
                return m_pInfo;
            }
        }
        private PlayerInfo? m_pInfo = null;


    }
}
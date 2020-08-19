using ProcessUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace HamsterCheese.AmongUsMemory
{
    public static class Cheese
    {
        public static Memory.Mem mem = new Memory.Mem();
        public static ProcessMemory ProcessMemory = null;
        public static bool Init()
        {
            var state = mem.OpenProcess("Among Us");
    
            if (state)
            {
                Methods.Init();
                Process proc = Process.GetProcessesByName("Among Us")[0];
                ProcessMemory = new ProcessMemory(proc);
                ProcessMemory.Open(ProcessAccess.AllAccess);
                return true;
            }
            return false;
        } 
        public static List<PlayerData> GetAllPlayers()
        { 
            List<PlayerData > datas = new List<PlayerData>();

            // find player pointer
            byte[] playerAoB = Cheese.mem.ReadBytes(Pattern.PlayerControl_Pointer, Utils.SizeOf<PlayerControll>()); 
            int cnt = 0;
            // aob pattern
            string aobData = ""; 
            // read 4byte aob pattern.
            foreach (var _byte in playerAoB)
            {
                if (_byte < 16)
                    aobData += "0" + _byte.ToString("X");
                else
                    aobData += _byte.ToString("X");

                if (cnt + 1 != 4)
                    aobData += " ";

                cnt++;
                if (cnt == 4)
                {
                    aobData += " ?? ?? ?? ??";
                    break;
                }
            }
            // get result 
            var result = Cheese.mem.AoBScan(aobData, true, true);
            result.Wait();
            var results =    result.Result;

            // player
            foreach (var x in results)
            {
                var bytes = Cheese.mem.ReadBytes(x.GetAddress(), Utils.SizeOf<PlayerControll>());
                var playerControll = Utils.FromBytes<PlayerControll>(bytes);
                // among us real instanced player ownerid is 257 :)
                if (playerControll.OwnerId == 257 && playerControll.netId != 0)
                { 
                    datas.Add(new PlayerData()
                    {
                        Instance = playerControll,
                        offset_str = x.GetAddress(),
                        offset_ptr = new IntPtr((int)x)
                    });
                }
            }
            return datas;
        }


    }
}

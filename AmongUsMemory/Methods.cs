
using HamsterCheese.AmongUsMemory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HamsterCheese. AmongUsMemory
{
    public class CallAttribute : System.Attribute { }
    public class InitAttribute : System.Attribute { }

    public static class Methods
    {
        #region PlayerControl.GetData
        public static IntPtr PlayerControl_GetDataPTR = IntPtr.Zero;
        #endregion

        [Init]
        public static void Init_PlayerControl_GetData()
        {
            Console.WriteLine("Init PlayerControl GetData");
            if (PlayerControl_GetDataPTR == IntPtr.Zero)
            {
                var aobScan = HamsterCheese.AmongUsMemory.Cheese.mem.AoBScan(Pattern.PlayerControl_GetData);
                aobScan.Wait();
                if (aobScan.Result.Count() == 1)
                {
                    PlayerControl_GetDataPTR = (IntPtr)aobScan.Result.First(); 
                }
            }
        }

        [Call]
        public static int Call_PlayerControl_GetData(IntPtr playerInfoPtr)
        { 
            Console.WriteLine("Call_PlayerControl_GetData");
            if (PlayerControl_GetDataPTR != IntPtr.Zero)
            {
                var ptr = PlayerControl_GetDataPTR;
                var playerInfoAddress = Cheese.ProcessMemory.CallFunction(ptr, playerInfoPtr); 
                return playerInfoAddress;
            }  
            return -1;
        }
        public static void Init()
        {
            var methods = typeof(Methods).GetMethods(); 
            foreach(var m in methods)
            {
                var atts = m.GetCustomAttributes(true); 
                foreach(var att in atts)
                { 
                    if (att.GetType() == typeof(InitAttribute))
                    { 
                        m.Invoke(null, null);
                    }
                }
            } 
        }
    }
}
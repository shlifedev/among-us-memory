using ProcessUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

        private static ShipStatus prevShipStatus;
        private static ShipStatus shipStatus;
        static Dictionary<string, CancellationTokenSource> Tokens = new Dictionary<string, CancellationTokenSource>();
        static System.Action<uint> onChangeShipStatus;


        static void _ObserveShipStatus()
        {
            while (Tokens.ContainsKey("ObserveShipStatus") && Tokens["ObserveShipStatus"].IsCancellationRequested == false)
            {
                Thread.Sleep(250);
                shipStatus = Cheese.GetShipStatus();
                if (prevShipStatus.OwnerId != shipStatus.OwnerId)
                {
                    prevShipStatus = shipStatus;
                    onChangeShipStatus?.Invoke(shipStatus.Type);
                    Console.WriteLine("OnShipStatusChanged");
                }
                else
                { 

                }
            }
        }


        public static void ObserveShipStatus(System.Action<uint> onChangeShipStatus)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            if (Tokens.ContainsKey("ObserveShipStatus"))
            {
                Tokens["ObserveShipStatus"].Cancel();
                Tokens.Remove("ObserveShipStatus");
            }

            Tokens.Add("ObserveShipStatus", cts);
            Cheese.onChangeShipStatus = onChangeShipStatus;
            Task.Factory.StartNew(_ObserveShipStatus, cts.Token);
        }

        public static ShipStatus GetShipStatus()
        { 
            ShipStatus shipStatus = new ShipStatus();
            byte[] shipAob = Cheese.mem.ReadBytes(Pattern.ShipStatus_Pointer, Utils.SizeOf<ShipStatus>());
            var aobStr = MakeAobString(shipAob, 4, "00 00 00 00 ?? ?? ?? ??");
            var aobResults = Cheese.mem.AoBScan(aobStr, true, true); 
            aobResults.Wait();  
            foreach (var result in aobResults.Result)
            {

                byte[] resultByte = Cheese.mem.ReadBytes(result.GetAddress(), Utils.SizeOf<ShipStatus>());
                ShipStatus resultInst = Utils.FromBytes<ShipStatus>(resultByte); 
                if (resultInst.AllVents != IntPtr.Zero && resultInst.NetId < uint.MaxValue - 10000)
                {
                    if (resultInst.MapScale < 6470545000000 && resultInst.MapScale > 0.1f)
                    {  
                        shipStatus = resultInst;  
                        Console.WriteLine(result.GetAddress());
                    }
                }
            }  
            return shipStatus;
        }

        private static Exception Exception(string v)
        {
            throw new NotImplementedException();
        }

        public static string MakeAobString(byte[] aobTarget, int length, string unknownText = "?? ?? ?? ??")
        {
            int cnt = 0;
            // aob pattern
            string aobData = "";
            // read 4byte aob pattern.
            foreach (var _byte in aobTarget)
            {
                if (_byte < 16)
                    aobData += "0" + _byte.ToString("X");
                else
                    aobData += _byte.ToString("X");

                if (cnt + 1 != 4)
                    aobData += " ";

                cnt++;
                if (cnt == length)
                {
                    aobData += $" {unknownText}";
                    break;
                }
            }
            return aobData;
        }
        public static List<PlayerData> GetAllPlayers()
        {
            List<PlayerData > datas = new List<PlayerData>();

            // find player pointer
            byte[] playerAoB = Cheese.mem.ReadBytes(Pattern.PlayerControl_Pointer, Utils.SizeOf<PlayerControl>());
            // aob pattern
            string aobData = MakeAobString(playerAoB, 4, "?? ?? ?? ??"); 
            // get result 
            var result = Cheese.mem.AoBScan(aobData, true, true);
            result.Wait();



            var results =    result.Result;
            // real-player
            foreach (var x in results)
            {
                var bytes = Cheese.mem.ReadBytes(x.GetAddress(), Utils.SizeOf<PlayerControl>());
                var PlayerControl = Utils.FromBytes<PlayerControl>(bytes);
                // filter garbage instance datas.
                if (PlayerControl.SpawnFlags == 257 && PlayerControl.NetId < uint.MaxValue - 10000)
                {
                    datas.Add(new PlayerData()
                    {
                        Instance = PlayerControl,
                        PlayerControllPTROffset = x.GetAddress(),
                        PlayerControllPTR = new IntPtr((int)x)
                    });
                }
            }
            Console.WriteLine("data => " + datas.Count);
            return datas;
        }


    }
}

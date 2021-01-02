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
        private static Dictionary<string, CancellationTokenSource> Tokens = new Dictionary<string, CancellationTokenSource>();
        private static System.Action<uint> onChangeShipStatus;


        static void _ObserveShipStatus()
        {
            while (Tokens.ContainsKey("ObserveShipStatus") && Tokens["ObserveShipStatus"].IsCancellationRequested == false)
            {
                Thread.Sleep(250);
                shipStatus = Cheese.GetShipStatus();
                if (prevShipStatus.OwnerId != shipStatus.OwnerId || (prevShipStatus.OwnerId == shipStatus.OwnerId && shipStatus.Timer > 0f))
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

        /// <summary>
        /// Subscribe shipstatus changed.
        /// </summary>
        /// <param name="onChangeShipStatus"></param>
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


        /// <summary>
        /// Get Ship Status From AmongUs Proccess
        /// </summary>
        /// <returns></returns>
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
                    if (resultInst.MapScale < 6470545000000 && resultInst.MapScale > 0) // 0.1f
                    {  
                        shipStatus = resultInst;
                        Console.WriteLine($"ShipStatus selected {result.GetAddress()}");
                    }
                }
            }  
            return shipStatus;
        }

        public static AmongUsClient GetClient()
        {
            AmongUsClient amongUsClient = new AmongUsClient();
            byte[] clientAob = Cheese.mem.ReadBytes(Pattern.AmongusClient_Pointer, Utils.SizeOf<AmongUsClient>());
            var aobStr = MakeAobString(clientAob, 4, "00 00 00 00 ?? ?? ?? ??");
            var aobResults = Cheese.mem.AoBScan(aobStr, true, true);
            aobResults.Wait();
            foreach (var result in aobResults.Result)
            {
                byte[] resultByte = Cheese.mem.ReadBytes(result.GetAddress(), Utils.SizeOf<ShipStatus>());
                AmongUsClient resultInst = Utils.FromBytes<AmongUsClient>(resultByte);
                if (resultInst.connection != IntPtr.Zero && resultInst.GameId != 0 && resultInst.GameState < 1000)
                {
                    amongUsClient = resultInst;
                    Console.WriteLine($"AmongUsClient selected {result.GetAddress()}");
                }
            }
            return amongUsClient;
        }

        public static String GetGameCode()
        {
            try
            {
                String code_string = Utils.ReadString((IntPtr)mem.ReadInt(Pattern.GameCode_Field_Pointer));
                return code_string.Split('\n')[1];
            }
            catch (NullReferenceException)
            {
            }
            catch (IndexOutOfRangeException)
            {
            }
            return null;
        }

        public static int GetMeetingStatus()
        {
            try
            {
                return mem.ReadInt(Pattern.MeetingHud_State_Field_Pointer);
            }
            catch (NullReferenceException)
            {
            }
            return 0;
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

        /// <summary>
        /// Get All Players From AmongUs Proccess
        /// </summary>
        /// <returns></returns>
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

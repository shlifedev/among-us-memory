
using HamsterCheese.AmongUsMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourCheese
{
    class Program
    {
        static int tableWidth = 75;

        static List<PlayerData> playerDatas;

        static void Main(string[] args)
        {  
            HamsterCheese.AmongUsMemory.Cheese.Init(); 

            // Update Player Data When Every Game
            HamsterCheese.AmongUsMemory.Cheese.ObserveShipStatus((x) => 
            {
                playerDatas = HamsterCheese.AmongUsMemory.Cheese.GetAllPlayers(); 
                Console.WriteLine("ob");
            });

            // 
            Task.Factory.StartNew(() => {
                while(true)
                {
                    Console.WriteLine("Test Read Player Datas..");
                    PrintRow("offset", "netId", "OwnerId", "PlayerId", "spawnid", "spawnflag");
                    PrintLine();
                    foreach (var data in playerDatas)
                    {
                        if (data.IsLocalPlayer)
                            Console.ForegroundColor = ConsoleColor.Green;
                        PrintRow($"{(data.IsLocalPlayer == true ? "Me->" : "")}{data.offset_str}", $"{data.Instance.NetId}", $"{data.Instance.OwnerId}", $"{data.Instance.PlayerId}", $"{data.Instance.SpawnId}", $"{data.Instance.SpawnFlags}");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.WriteLine(HamsterCheese.AmongUsMemory.Utils.ReadString(data.PlayerInfo.Value.PlayerName));
                        PrintLine();
                    }
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }

            });
     
            System.Threading.Thread.Sleep(1000000);
        }

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        } 
    }
}



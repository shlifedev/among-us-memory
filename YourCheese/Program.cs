
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
        static void Main(string[] args)
        {  
            HamsterCheese.AmongUsMemory.Cheese.Init();
            var players = HamsterCheese.AmongUsMemory.Cheese.GetAllPlayers();

            Console.WriteLine("Test Read Player Datas..");

            /// draw table

            PrintRow("offset", "netId", "OwnerId", "PlayerId", "spawnid", "spawnflag");
            PrintLine();
            foreach (var data in players)
            {
                if (data.IsLocalPlayer)
                {
                    var xx = data.LightSource;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(xx.LightRadius);


                    Console.ForegroundColor = ConsoleColor.Green;
           
                }
                PrintRow($"{(data.IsLocalPlayer == true ? "Me->" : "")}{data.offset_str}", $"{data.Instance.NetId}", $"{data.Instance.OwnerId}", $"{data.Instance.PlayerId}", $"{data.Instance.SpawnId}", $"{data.Instance.SpawnFlags}");


                

                Console.ForegroundColor = ConsoleColor.White;
                PrintLine();
            }   



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



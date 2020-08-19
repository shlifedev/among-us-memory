using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourCheese
{
    class Program
    {
        static void Main(string[] args)
        {
            HamsterCheese.AmongUsMemory.Cheese.Init();
            var players = HamsterCheese.AmongUsMemory.Cheese.GetAllPlayers();

            Console.WriteLine("Test Read Player Datas..");
            while (true)
            {
                foreach (var data in players)
                {
                    Console.WriteLine("find player color : " + data.PlayerInfo.Value.ColorId);
                    Console.WriteLine("find player position : " + data.GetSyncPosition().x + "," + data.GetSyncPosition().y);
                }

            }
            System.Threading.Thread.Sleep(1000000);
        }
    }
}

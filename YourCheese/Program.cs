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
            foreach(var data in players)
            {
                Console.WriteLine("find player color : " + data.PlayerInfo.Value.ColorId);
            }

            System.Threading.Thread.Sleep(1000000);
        }
    }
}

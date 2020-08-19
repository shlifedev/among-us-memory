# AmongUsMemory

**Working Fine 2020.8.12s**

## How to Use
 
 ```cs
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
 ```

You can get all player info ( IsImposter / IsDead / Position .. etc )

## Example Cheat

 this is my private cheat
 ![](https://github.com/shlifedev/AmongUsPublic/blob/master/Example.PNG) 
 https://www.youtube.com/watch?v=Cfk9_wNjEto&feature=youtu.be
 
 
Just use it for study purposes only.



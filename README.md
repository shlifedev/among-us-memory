# AmongUsMemory

## How to Use
 
 ```cs
 
            // init your cheat. (if you want true return, require among us.exe proccess)
            var init = HamsterCheese.AmongUsMemory.Cheese.Init(); 
            if(init)
            {
              var players = HamsterCheese.AmongUsMemory.Cheese.GetAllPlayers();
              Console.WriteLine("player count : " + players.Count);
              foreach(var data in players)
              {
                  Console.WriteLine("find player color : " + data.PlayerInfo.Value.ColorId);
              }


              // ignore this.
              System.Threading.Thread.Sleep(1000000);  
            }
 ```


## Example Cheat

 this is my private cheat
 ![](https://github.com/shlifedev/AmongUsPublic/blob/master/Example.PNG) 
 https://www.youtube.com/watch?v=Cfk9_wNjEto&feature=youtu.be
 
 
Just use it for study purposes only.



# AmongUsPublicCheese 
 ▼ my private cheat
 ![](https://github.com/shlifedev/AmongUsPublic/blob/master/111111111111111.PNG) 
 https://www.youtube.com/watch?v=Cfk9_wNjEto&feature=youtu.be
 
## Source Code

 I do not share the cheat source code.  
 However, I share Among us Memory Reader.
 
 

### How to Use
 
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

Just use it for study purposes only.


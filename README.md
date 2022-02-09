# AmongUsMemory (Deprecated)

![image](https://user-images.githubusercontent.com/49047211/91510992-9581fe00-e919-11ea-8be1-93e333de762b.png)
[blog post](https://blog.shlife.dev/4)

You are responsible for any disadvantages caused by using this src.  
It only works with the version downloaded from Steam. 

Please use only for study. Do not abuse it!   

When game update require update new offset, offset update guide here! : https://github.com/shlifedev/AmongUsMemory/blob/master/OffsetGuide.md  
It's very easy. does not require il2cpp dumper.  

  ----------------------
## How to Use
 1. Download Source Code
 2. Add Reference AmongUsMemory Your Project 
 3. Now, Let's write your code and build project!  
   
### Example Start Cheating.
 
 ```cs
       
        // Readed Player List
        static List<PlayerData> playerDatas = new List<PlayerData>(); 
        
        // Update Your Cheat 
        static void UpdateCheat()
        {
            while (true)
            { 
                foreach (var data in playerDatas)
                {
                    Console.WriteLine("Find Player Name :: " + Utils.ReadString(data.PlayerInfo.Value.PlayerName));
                } 
                System.Threading.Thread.Sleep(100); 
            }
        }
        
        // Update Player List EveryGame.
        static void OnDetectJoinNewGame()
        {
            playerDatas = HamsterCheese.AmongUsMemory.Cheese.GetAllPlayers();
        }
        
        static void Main(string[] args)
        {
            // Cheat Init
            if (HamsterCheese.AmongUsMemory.Cheese.Init())
            { 
                // Update Player Data When Join New Map.
                HamsterCheese.AmongUsMemory.Cheese.ObserveShipStatus(OnDetectJoinNewGame);

                // Start Your Cheat 
                CancellationTokenSource cts = new CancellationTokenSource();
                Task.Factory.StartNew(
                    UpdateCheat
                , cts.Token); 
            }

            System.Threading.Thread.Sleep(1000000);
        }
        
 ```  
## Example Cheat

 This is my private cheat :)
 ![](https://github.com/shlifedev/AmongUsPublic/blob/master/Example.PNG) 
 https://www.youtube.com/watch?v=Cfk9_wNjEto&feature=youtu.be 
 
`offset is no longer updated.` 
`if you have new offset pull request plz.`
 
 

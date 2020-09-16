# AmongUsMemory
![image](https://user-images.githubusercontent.com/49047211/91510992-9581fe00-e919-11ea-8be1-93e333de762b.png)


  You are responsible any disadvantages caused by use this src.
  **It only works with the version downloaded from Steam** 
  And, Use only for study. never abuse! please.. 
  
  if game updated then require offset update. contact me.
  
  ----------------------
## How to Use
 1. Download Source Code
 2. Add Reference AmongUsMemory Your Project 
 3. Now, Let's Write Code!
 
 
## Offset Update Guide
https://github.com/shlifedev/AmongUsMemory/blob/master/OffsetGuide.md
here :)
   
   
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
 
 You can get/set player memory. ex) `IsImposter` `Position` `IsDead` `InVent` ..etc

## Example Cheat

 this is my private cheat !!
 ![](https://github.com/shlifedev/AmongUsPublic/blob/master/Example.PNG) 
 https://www.youtube.com/watch?v=Cfk9_wNjEto&feature=youtu.be
 
  
 

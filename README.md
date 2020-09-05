 

# AmongUsMemory
![image](https://user-images.githubusercontent.com/49047211/91510992-9581fe00-e919-11ea-8be1-93e333de762b.png)


  You are responsible any disadvantages caused by use this src.
  It only works with the version downloaded from Steam  
  And, Use only for study. never abuse! please.. 
  
  if game updated then require offset update. 
  
## How to Use
 1. Download Source Code
 2. Add Reference AmongUsMemory Your Project 
 3. Now, Let's Write Code!
 
 
 
## ChangeLog
 * 2020-09-03 -- offset updated 2020.9.01s version
 * 2020-09-02 -- offset updated 2020.8.31s version
 * 2020-08-27 -- added **ObserveShipStatus**, you can detect join new game/new map
 * 2020-08-26 -- added **readString** function in Utils.cs. now you can read string pointers.
 * 2020-08-24 -- added **getShipstatus** function in cheese.cs
 * 2020-08-20 -- fix wrong data structure
 * 2020-08-20 -- added auto structure generator xml based
 
   
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
 
 

## For Net Framework 4.5

[Here](https://github.com/shlifedev/AmongUsMemory/tree/net-framework-4.5)
 

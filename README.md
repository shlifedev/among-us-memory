# AmongUsMemory

**Working Fine 2020.8.12s**

**It only works with the version downloaded from Steam!**

## How to Use
 1. Download Source Code
 2. Add Reference AmongUsMemory Your Project 
 3. End
 
 
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
        
        
        static void Main(string[] args)
        {
            // Cheat Init
            if (HamsterCheese.AmongUsMemory.Cheese.Init())
            { 
                // Update Player Data When Join New Map.
                HamsterCheese.AmongUsMemory.Cheese.ObserveShipStatus((x) =>
                {
                    playerDatas = HamsterCheese.AmongUsMemory.Cheese.GetAllPlayers();
                });

                // Start Your Cheat 
                CancellationTokenSource cts = new CancellationTokenSource();
                Task.Factory.StartNew(
                    UpdateCheat();
                , cts.Token); 
            }

            System.Threading.Thread.Sleep(1000000);
        }
        
 ```
 
 

### Read String Pointer
 Call Utils.ReadString(offset);
 
### How to Detect New Game  
  ```cs
        public ShipStatus cur_shipStatus;
        public ShipStatus prev_shipStatus;
         
        void CheckNewGame()
        {
            cur_shipStatus = Cheese.GetShipStatus();
            if (prev_shipStatus.OwnerId != cur_shipStatus.OwnerId)
            {
                 prev_shipStatus = cur_shipStatus;
                 OnNewGame(cur_shipStatus.Type);
            }
        }
        
        void OnNewGame(uint mapId)
        {
             Console.WriteLine("New Map Id : " + mapId);
        }
  ```       

 
You can get all player info ( IsImposter / IsDead / Position .. etc )

## Example Cheat

 this is my private cheat
 ![](https://github.com/shlifedev/AmongUsPublic/blob/master/Example.PNG) 
 https://www.youtube.com/watch?v=Cfk9_wNjEto&feature=youtu.be
 
 
Just use it for study purposes only.


## TODO

 full automatic cheating (detect map, detect new game.. etc)
 currently require restart cheat every game.
 
## PatchNotes

 * 2020-08-26 -- added **readString** function in Utils.cs. now you can read string pointers.
 * 2020-08-24 -- added **getShipstatus** function in cheese.cs
 * 2020-08-20 -- fix wrong data structure
 * 2020-08-20 -- added auto structure generator xml based
 

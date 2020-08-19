# AmongUsPublic

 ![](https://github.com/shlifedev/AmongUsPublic/blob/master/111111111111111.PNG)
 
 https://www.youtube.com/watch?v=Cfk9_wNjEto&feature=youtu.be
## Feature
 
  - 2D Radar (Dead Player / Imposter / Player Pos)
  - Write Memory (Set Imposter / Revive&Die)

## Source Code

 I do not share the cheat source code.  
 However, i share the offset and structure used in the game.

### Important Offset & Pattern
 ```cs
 
             /// <summary>
            /// Get PlayerControl 
            /// </summary>
            public static string PlayerControl_Pointer = "GameAssembly.dll+E22AE8";  
            /// <summary>
            /// Get PlayerControl.Get_Data(); 
            /// </summary>
            public static string PlayerControl_GetData = "55 8B EC 80 3D 58 06 ??";  //it's method. return PlayerInfo Class.
            
 
 ```
 
 ### Find Player Aob Pattern
 ```cs
 
 byte[] playerAoB = Memory.ReadBytes(EngineOffset.Pattern.PlayerControl_Pointer, PlayerControll.SizeOf());
  int cnt = 0;
            // aob pattern
            string aobData = "";

            // read 4byte aob pattern.
            foreach (var _byte in playerAoB)
            { 
                if(_byte < 16) 
                    aobData += "0"+_byte.ToString("X"); 
                else 
                    aobData += _byte.ToString("X"); 
              
                if(cnt+1 != 4) 
                    aobData += " ";

                cnt++;
                if (cnt == 4)
                {
                    aobData += " ?? ?? ?? ??"; 
                    break;
                } 
            } 
            // get result 
            var result = AoBScan(aobData);  

   
            foreach (var x in result)
            {
                var bytes = Memory.ReadBytes(x.GetAddress(), PlayerControll.SizeOf());
                var playerControll = PlayerControll.FromBytes(bytes);
                // among us real instanced player ownerid is 257 :)
                if (playerControll.OwnerId == 257 && playerControll.netId != 0)
                { 
                    list.Add(new CachedPlayerControllInfo()
                    {
                        Instance = playerControll,
                        offset = x.GetAddress(),
                        offset_ptr = new IntPtr((int)x)
                    });
                } 
            } 
 ```
 


## Structure

```cs  
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PlayerControll
    { 
        public UIntPtr m_cachedPtr;
        public uint spawnId;
        public uint netId;
        public uint DirtyBits;
        public byte SpawnFlags;
        public bool sendMode;
        public uint OwnerId;
        public byte DespawnOnDestroy;
        public Int32 LastStartCounter;
        public byte PlayerId;
        public Single MaxReportDistance;
        public bool moveable;
        public byte inVent;
        public UIntPtr _cachedData;
        public UIntPtr FootSteps;
        public UIntPtr KillSfx;
        public UIntPtr KillAnimations;
        public Single killTimer;
        public Int32 RemainingEmergencies;
        public UIntPtr nameText;
        public UIntPtr LightPrefab;
        public UIntPtr myLight;
        public UIntPtr Collider;
        public UIntPtr MyPhysics;
        public UIntPtr NetTransform;
        public UIntPtr CurrentPet;
        public UIntPtr HatRenderer;
        public UIntPtr myRend;
        public UIntPtr hitBuffer;
        public UIntPtr myTasks;
        public UIntPtr ScannerAnims;
        public UIntPtr ScannersImages;
        public UIntPtr closest;
        public Boolean isNew;
        public UIntPtr cache;
        public UIntPtr itemsInRange;
        public UIntPtr newItemsInRange;
        public Byte scannerCount;
        public Boolean infectedSet;

    } 
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PlayerInfo
    {  
        public IntPtr test,test2;
        public byte PlayerId;
        public UIntPtr PlayerName;
        public byte ColorId;
        public uint HatId;
        public uint PetId;
        public uint SkinId;
        public byte Disconnected;
        public UIntPtr Tasks;
        public byte IsImpostor;
        public byte IsDead;
        private UIntPtr _object;
    } 


```

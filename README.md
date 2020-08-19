# AmongUsPublic

 ![](https://github.com/shlifedev/AmongUsPublic/blob/master/111111111111111.PNG)
 
## Feature
 
  - 2D Radar (Dead Player / Imposter / Player Pos)
  - Write Memory (Set Imposter / Revive&Die)

## Source Code
 I Share Offset Only
 
 ```cs
 
             /// <summary>
            /// Get PlayerControl
            /// </summary>
            public static string PlayerControl_Pointer = "GameAssembly.dll+E22AE8";  
            /// <summary>
            /// Get PlayerControl.Get_Data(); 
            /// </summary>
            public static string PlayerControl_GetData = "55 8B EC 80 3D 58 06 ??";  //It's Method
            
 
 ```
 
 ## All Player Find Aob Pattern
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
 

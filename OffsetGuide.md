 Deprecated Docs, But you can use this way any il2cpp games! 


 You want find static pointer in mono compile? (ll2cpp) don't worry! it is very easy.   
 All structure/pointer offset can be found like this with cheat engine!

 ## PlayerControl Static Pointer Offset
 1. Open cheat engine, and select among us proccess.
 2. **Mono -> Activate mono features** In CheatEngine(top menu)
 3. **Mono -> Dissect Mono**   In CheatEngine (top menu)
 4. **Goto Assembly-Chsarp.dll -> PlayerControl and RightClick -> Find Instance Of Class**
 5. Copy any instance and add your cheat table
 ![img](https://github.com/shlifedev/AmongUsMemory/blob/master/guide_01.PNG?raw=true)
 6. Copy value and search 4 bytes value type
 7. Now you can find instance static pointer offset in searched list :)
 ![img](https://github.com/shlifedev/AmongUsMemory/blob/master/guide_02.PNG?raw=true)
 8. Update EngineOffset.cs
 
 
 ## ShipStatus Static Pointer Offset
 
 Same as PlayerControl. 
 
 
 ## PlayerControl.getData 
 (Previously, I used it by dumping with il2cpp, so I use signature search.)
 
 1. open cheat engine, and select among us proccess.
 2. **Mono -> Activate mono features**  (top menu)
 3. **Mono -> Dissect Mono**  (top men
 4. Goto **Assembly-Chsarp.dll -> PlayerControl -> methods -> get_data()** right click and Click **Jit**
 5. Ctrl+C And Copy Address.  
![img](https://github.com/shlifedev/AmongUsMemory/blob/master/guide_03.PNG?raw=true)
 6. Goto Copied Address in HexViewer and **Copy 7bytes**, and Update **EngineOffset.cs**  
![img](https://github.com/shlifedev/AmongUsMemory/blob/master/guide_04.PNG?raw=true)


 ## Result
 
```cs
namespace HamsterCheese.AmongUsMemory
{
    public sealed class Pattern
    {
        /// <summary>
        /// Get PlayerControl
        /// </summary>
        public static string PlayerControl_Pointer = "GameAssembly.dll+DA5A84";  //GameAssembly.dll+E22AE8
        public static string ShipStatus_Pointer = "GameAssembly.dll+DA5A50";
        public static string AmongusClient_Pointer = "GameAssembly.dll+DA5ACC";
        /// <summary>
        /// Get PlayerControl.Get_Data();
        /// </summary>
        public static string PlayerControl_GetData = "55 8B EC 80 3D BD B0 ??";
        
    }
} 
  

 ```

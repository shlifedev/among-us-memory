 
 Finding a static pointer in mono compile (ll2cpp) is very simple.   
 Just use the Cheat Engine mono dissector Feature :)
  
 
 
 
 All structure/pointer offset can be found like this! Very easy!

 ## PlayerControl Static Pointer Offset
 1. open cheat engine, and select among us proccess.
 2. **Mono -> Activate mono features**  (top menu)
 3. **Mono -> Dissect Mono**  (top men
 4. **Goto Assembly-Chsarp.dll -> PlayerControl and RightClick -> Find Instance Of Class**
 5. Copy any instance and add your cheat table
 ![img](https://github.com/shlifedev/AmongUsMemory/blob/master/guide_01.PNG?raw=true)
 6. Copy value and search 4bytes value type
 7. Good, Now you can find instance static pointer offset in searched list :)
 ![img](https://github.com/shlifedev/AmongUsMemory/blob/master/guide_02.PNG?raw=true
 8. Update EngineOffset.cs
 
 
 ## ShipStatus Static Pointer Offset
 
 Same as PlayerControl. Find the ship status on the mono dissector!  
 (But, Must be try ingame. can't find a static offset in the lobby.)
 
 
 ## PlayerControl.getData 
 
 1. open cheat engine, and select among us proccess.
 2. **Mono -> Activate mono features**  (top menu)
 3. **Mono -> Dissect Mono**  (top men
 4. Goto **Assembly-Chsarp.dll -> PlayerControl -> methods -> get_data()** right click and Click **Jit**
 5. Ctrl+C And Copy Address.
 ![img](https://github.com/shlifedev/AmongUsMemory/blob/master/guide_03.PNG?raw=true
 6. Goto Copied Address in HexViewer and **Copy 7bytes**, and Update **EngineOffset.cs**
 ![img](https://github.com/shlifedev/AmongUsMemory/blob/master/guide_04.PNG?raw=true
 
 
 Very Easy :)
 

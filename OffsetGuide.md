
 still writing.
 
 
 ### how to find playercontro.get_data method signature pattern
 
 1. download **il2cppdumper** latest https://github.com/Perfare/Il2CppDumper, and **hxd** https://mh-nexus.de/en/downloads.php?product=HxD20
 2. target binary : GameAssembly.dll dump it.
 3. open dump.cs notepad++
 4. find (ctrl+f) **public GameData.PlayerInfo get_Data() { }** it
 5. and you can find like this. **copy Offset (212C90)**
 ```cs
 // RVA: 0x214090 Offset: 0x212C90** VA: 0x10214090
	public GameData.PlayerInfo get_Data() { }
 ```
 6. open hxd and drag and drop **GameAssembly.dll** your Game Install Folder
 7. ctrl+g in **hxd** and find **Offset** without 0x 
 8. copy **7bytes** (00 00 00 00 00 00 00)
 9. open EngineOffset.cs and copy it (00 00 00 00 00 00 00 ??)
 
 

# BLRPC
Discord Rich Presence for BONELAB

## JeviLib
* You do not actually need it installed.
* You just need to have opened the game once with JeviLib installed so it can fix a few things within MelonLoader.
* Don't remove it if you have other mods that require it's actual functions though, this mod only needs the fixes that it applies and you don't need JeviLib installed for the fixes to persist.

## Installation
1. Download the latest release from [here](https://bonelab.thunderstore.io/package/CarrionAndOn/BonelabRichPresence/)
2. Extract the zip file into your BONELAB directory

## Customization
* Edit the "UserEntries.txt" file in the "UserData/BLRPC" folder and add stuff on new lines (This file is generated when you open the game with this mod installed.)
* For image customization, open MelonPrefs and change the application ID to your own application ID.
* Your art assets have to be named the same as they are for me. Check Main.cs's CheckMap method for the names. The names are after the return statements.
* You can change the details mode to either Entries, GunShots, NPCDeaths, SDKMods, SpawnablesPlaced, or Extraes.
* Entries grabs a random line from UserEntries.txt, GunShots shows the times you've shot a gun, NPC deaths shows the total NPCs killed, SDKMods shows the amount of mods you have loaded, SpawnablesPlaced shows the amount of objects you've spawned, Extraes :)
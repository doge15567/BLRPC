# BLRPC
Discord Rich Presence for BONELAB

## Mod.io Mod Networker Support - doge15567's Addition
* If networker is installed, custom maps and avatars will use the mod's thumbnail.

## Fusion Support
* If Fusion is installed, you can invite people to your Fusion servers through Discord.
* It will show the current players in the server, then the max players allowed in the server in the status.

## Installation
1. Download the latest release from [here](https://bonelab.thunderstore.io/package/CarrionAndOn/BonelabRichPresence/)
2. Extract the zip file into your BONELAB directory

## Customization
* Edit the "UserEntries.txt" file in the "UserData/BLRPC" folder and add stuff on new lines (This file is generated when you open the game with this mod installed.)
* For image customization, open MelonPrefs and change the application ID to your own application ID.
* Your art assets have to be named the same as they are for me. Check Main.cs's CheckMap method for the names. The names are after the return statements.
* You can change the details mode to either Entries, GunShots, NPCDeaths, SDKMods, SpawnablesPlaced, or Extraes.
* Entries grabs a random line from UserEntries.txt, GunShots shows the times you've shot a gun, NPC deaths shows the total NPCs killed, SDKMods shows the amount of mods you have loaded, SpawnablesPlaced shows the amount of objects you've spawned, Extraes :)
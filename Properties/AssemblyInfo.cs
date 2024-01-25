using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(BLRPC.Main.Description)]
[assembly: AssemblyDescription(BLRPC.Main.Description)]
[assembly: AssemblyCompany(BLRPC.Main.Company)]
[assembly: AssemblyProduct(BLRPC.Main.Name)]
[assembly: AssemblyCopyright("Developed by " + BLRPC.Main.Author)]
[assembly: AssemblyTrademark(BLRPC.Main.Company)]
[assembly: AssemblyVersion(BLRPC.Main.Version)]
[assembly: AssemblyFileVersion(BLRPC.Main.Version)]
[assembly:
    MelonInfo(typeof(BLRPC.Main), BLRPC.Main.Name, BLRPC.Main.Version,
        BLRPC.Main.Author, BLRPC.Main.DownloadLink)]
[assembly: MelonColor(System.ConsoleColor.White)]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonOptionalDependencies("DOOMLAB", "JeviLib")]
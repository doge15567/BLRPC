using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;
using SLZ.Marrow.SceneStreaming;
using UnityEngine;

namespace BLRPC.Melon;

internal static class BoneMenu
{
    private static MenuCategory _mainCategory;
    private static MenuCategory _menuCategory;
    private static FunctionElement _reloadButton;
    private static readonly MelonEvent<DetailsMode, DetailsMode> EntryValueChanged = Preferences.DetailsMode.OnEntryValueChanged;
    public static void Setup()
    {
        _mainCategory = MenuManager.CreateCategory("Weather Electric", "#6FBDFF");
        _menuCategory = _mainCategory.CreateCategory("BLRPC", "#5769ec");
        _menuCategory.CreateEnumPreference("Details Mode", Color.white, Preferences.DetailsMode, Preferences.Category);
            
        #region NPC Deaths Settings
            
        var npcDeaths = _menuCategory.CreateSubPanel("NPC Deaths Settings", Color.green);
        npcDeaths.CreateBoolPreference("Count DOOMLAB Kills", "#840000", Preferences.CountDoomlabDeaths, Preferences.Category);
        npcDeaths.CreateBoolPreference("Reset Kills On Level Load", Color.green, Preferences.ResetKillsOnLevelLoad, Preferences.Category);
            
        #endregion
            
        #region Player Deaths Settings
            
        var playerDeaths = _menuCategory.CreateSubPanel("Player Deaths Settings", Color.red);
        playerDeaths.CreateBoolPreference("Reset Deaths On Level Load", "#840000", Preferences.ResetDeathsOnLevelLoad, Preferences.Category);
            
        #endregion
            
        #region Gun Shots Settings
            
        var gunShots = _menuCategory.CreateSubPanel("Gun Shots Settings", Color.blue);
        gunShots.CreateBoolPreference("Reset Gun Shots On Level Load", "#840000", Preferences.ResetGunShotsOnLevelLoad, Preferences.Category);
            
        #endregion
            
        EntryValueChanged.Subscribe(AddReloadButton);
    }

    private static void AddReloadButton(DetailsMode oldval, DetailsMode newval)
    {
            if (_reloadButton != null) return;
            _reloadButton = _menuCategory.CreateFunctionElement("Reload Level", Color.red, () =>
            {
                SceneStreamer.Reload();
                _menuCategory.Elements.Remove(_reloadButton);
                _reloadButton = null;
            });
        }
}
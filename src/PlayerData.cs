using Il2CppAssets.Scripts.Database;
using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppAssets.Scripts.PeroTools.Managers;

namespace MuseDashMirror;

/// <summary>
///     Player data
/// </summary>
public static class PlayerData
{
    /// <summary>
    ///     Player Level
    /// </summary>
    public static int PlayerLevel => DataHelper.Level;

    /// <summary>
    ///     Player Name
    /// </summary>
    public static string PlayerName => DataHelper.nickname;

    /// <summary>
    ///     Player favorite chart list
    /// </summary>
    public static Il2CppSystem.Collections.Generic.List<string> Collections => DataHelper.collections;

    /// <summary>
    ///     Player history list
    /// </summary>
    public static Il2CppSystem.Collections.Generic.List<string> History => DataHelper.history;

    /// <summary>
    ///     Player hide chart list
    /// </summary>
    public static Il2CppSystem.Collections.Generic.List<string> Hides => DataHelper.hides;

    /// <summary>
    ///     Selected elfin index
    /// </summary>
    public static int SelectedElfinIndex => DataHelper.selectedElfinIndex;

    /// <summary>
    ///     Localized elfin name
    /// </summary>
    public static string SelectedElfinName => Singleton<ConfigManager>.instance.GetJson("elfin", true)[SelectedElfinIndex]["name"].ToString();

    /// <summary>
    ///     Selected character index
    /// </summary>
    public static int SelectedCharacterIndex => DataHelper.selectedRoleIndex;

    /// <summary>
    ///     Localized character name
    /// </summary>
    public static string SelectedCharacterName =>
        Singleton<ConfigManager>.instance.GetJson("character", true)[SelectedCharacterIndex]["cosName"].ToString();

    /// <summary>
    ///     Offset
    /// </summary>
    public static int Offset => DataHelper.offset;

    /// <summary>
    ///     Auto fever
    /// </summary>
    public static bool IsAutoFever => DataHelper.isAutoFever;

    /// <summary>
    ///     Set character with index
    /// </summary>
    public static void SetCharacter(int characterIndex) => DataHelper.selectedRoleIndex = characterIndex;

    /// <summary>
    ///     Set elfin with index
    /// </summary>
    public static void SetElfin(int elfinIndex) => DataHelper.selectedElfinIndex = elfinIndex;

    /// <summary>
    ///     Set music offset
    /// </summary>
    public static void SetOffset(int offset) => DataHelper.offset = offset;

    /// <summary>
    ///     Set auto fever
    /// </summary>
    public static void SetAutoFever(bool autoFever) => DataHelper.isAutoFever = autoFever;
}
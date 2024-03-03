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
    public static Il2CppStringList Collections => DataHelper.collections;

    /// <summary>
    ///     Player history list
    /// </summary>
    public static Il2CppStringList History => DataHelper.history;

    /// <summary>
    ///     Player hide chart list
    /// </summary>
    public static Il2CppStringList Hides => DataHelper.hides;

    /// <summary>
    ///     Selected elfin index
    /// </summary>
    public static int SelectedElfinIndex => DataHelper.selectedElfinIndex;

    /// <summary>
    ///     Selected character index
    /// </summary>
    public static int SelectedCharacterIndex => DataHelper.selectedRoleIndex;

    /// <summary>
    ///     Music offset
    /// </summary>
    public static int Offset => DataHelper.offset;

    /// <summary>
    ///     Auto fever
    /// </summary>
    public static bool IsAutoFever => DataHelper.isAutoFever;

    /// <summary>
    ///     Get elfin name
    /// </summary>
    public static string GetSelectedElfinName(bool localized = true) =>
        Singleton<ConfigManager>.instance.GetJson("elfin", localized)[SelectedElfinIndex]["name"].ToString();

    /// <summary>
    ///     Get character name
    /// </summary>
    public static string GetSelectedCharacterName(bool localized = true) =>
        Singleton<ConfigManager>.instance.GetJson("character", localized)[SelectedCharacterIndex]["cosName"].ToString();

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
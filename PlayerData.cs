using Assets.Scripts.Database;
using Assets.Scripts.PeroTools.Commons;
using Assets.Scripts.PeroTools.Managers;
using Assets.Scripts.PeroTools.Nice.Datas;
using Il2CppSystem.Collections.Generic;

namespace MuseDashMirror
{
    public static class PlayerData
    {
        /// <summary>
        /// Player Level
        /// </summary>
        public static int PlayerLevel { get => DataHelper.Level; }

        /// <summary>
        /// Player Name
        /// </summary>
        public static string PlayerName { get => DataHelper.nickname; }

        /// <summary>
        /// Player favorite chart list
        /// </summary>
        public static List<string> Collections { get => DataHelper.collections; }

        /// <summary>
        /// Player history list
        /// </summary>
        public static List<string> History { get => DataHelper.history; }

        /// <summary>
        /// Player hide chart list
        /// </summary>
        public static List<string> Hides { get => DataHelper.hides; }

        /// <summary>
        /// Selected elfin index
        /// </summary>
        public static int SelectedElfinIndex { get => DataHelper.selectedElfinIndex; }

        /// <summary>
        /// Localized elfin name
        /// </summary>
        public static string SelectedElfinName { get => Singleton<ConfigManager>.instance.GetJson("elfin", true)[SelectedElfinIndex]["name"].ToString(); }

        /// <summary>
        /// Selected character index
        /// </summary>
        public static int SelectedCharacterIndex { get => DataHelper.selectedRoleIndex; }

        /// <summary>
        /// Localized character name
        /// </summary>
        public static string SelectedCharacterName { get => Singleton<ConfigManager>.instance.GetJson("character", true)[SelectedCharacterIndex]["cosName"].ToString(); }

        public static int Offset { get => DataHelper.offset; }

        /// <summary>
        /// Set character with index
        /// </summary>
        public static void SetCharacter(int characterIndex) => Singleton<DataManager>.instance["Account"]["SelectedRoleIndex"].Set(new Il2CppSystem.Int32() { m_value = characterIndex }.BoxIl2CppObject());

        /// <summary>
        /// Set elfin with index
        /// </summary>
        public static void SetElfin(int elfinIndex) => Singleton<DataManager>.instance["Account"]["SelectedElfinIndex"].Set(new Il2CppSystem.Int32() { m_value = elfinIndex }.BoxIl2CppObject());

        /// <summary>
        /// Set music offset
        /// </summary>
        public static void SetOffset(int offset) => Singleton<DataManager>.instance["GameConfig"]["Offset"].Set(new Il2CppSystem.Int32() { m_value = offset }.BoxIl2CppObject());

        /// <summary>
        /// Set auto fever
        /// </summary>
        public static void SetAutoFever(bool autoFever) => Singleton<DataManager>.instance["Account"]["IsAutoFever"].Set(new Il2CppSystem.Boolean() { m_value = autoFever }.BoxIl2CppObject());
    }
}
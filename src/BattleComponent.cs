using Assets.Scripts.Database;
using Assets.Scripts.GameCore.HostComponent;
using Assets.Scripts.PeroTools.Commons;
using FormulaBase;
using GameLogic;
using MuseDashMirror.Patch;
using System.Collections.Generic;

namespace MuseDashMirror
{
    public static class BattleComponent
    {
        private static TaskStageTarget tst = Singleton<TaskStageTarget>.instance;

        #region ChartInfo

        /// <summary>
        /// Chart name, only changed when entering the chart
        /// </summary>
        public static string ChartName { get => GetLocalPatch.chartName; }

        /// <summary>
        /// Level of the chart, only changed when entering the chart
        /// </summary>
        public static string ChartLevel { get => HideBmsCheckPatch.chartLevel; }

        /// <summary>
        /// Difficulty of the chart (easy, hard, master, hidden, touhou), only changed when entering the chart
        /// </summary>
        public static int Difficulty { get => HideBmsCheckPatch.difficulty; }

        /// <summary>
        /// Music author, only changed when entering the chart
        /// </summary>
        public static string MusicAuthor { get => HideBmsCheckPatch.musicAuthor; }

        /// <summary>
        /// Charter, only changed when entering the chart
        /// </summary>
        public static string Charter { get => HideBmsCheckPatch.charter; }

        /// <summary>
        /// The name for album package
        /// </summary>
        public static string SelectedAlbumUid { get => DataHelper.selectedAlbumUid; }

        /// <summary>
        /// The index of selected music in its own album package
        /// </summary>
        public static int SelectedMusicIndex { get => DataHelper.selectedMusicIndex; }

        /// <summary>
        /// The index of selected music in current category
        /// </summary>
        public static int SelectedMusicIndexInCurrent { get => DataHelper.selectedMusicIndexInAllList; }

        /// <summary>
        /// The full path for selected music (album+music index)
        /// </summary>
        public static string SelectedMusicUid { get => DataHelper.selectedMusicUid; }

        #endregion ChartInfo

        #region GameInfo

        /// <summary>
        /// Becomes true when "ready go" finished
        /// </summary>
        public static bool IsInGame { get => Singleton<StageBattleComponent>.instance.isInGame; }

        /// <summary>
        /// 3 decimal places number for game play time, start when "ready go" finished
        /// </summary>
        public static float Tick { get => Singleton<StageBattleComponent>.instance.realTimeTick; }

        /// <summary>
        /// Perfect count
        /// </summary>
        public static int Perfect { get => tst.m_PerfectResult; }

        /// <summary>
        /// Great count
        /// </summary>
        public static int Great { get => tst.m_GreatResult; }

        /// <summary>
        /// Miss count (hurt miss)
        /// </summary>
        public static int Miss { get => tst.m_MissResult; }

        /// <summary>
        /// Blue collectable notes count
        /// </summary>
        public static int Get { get => tst.m_CoolResult; }

        /// <summary>
        /// Heart count
        /// </summary>
        public static int Heart { get => tst.m_Blood; }

        /// <summary>
        /// Jumpover count
        /// </summary>
        public static int JumpOver { get => tst.m_JumpOverResult; }

        /// <summary>
        /// Music datas for the chart, only changed when entering the chart
        /// </summary>
        public static List<MusicData> MusicDatas { get => StageBattleComponentPatch.musicDatas; }

        #endregion GameInfo

        /// <summary>
        /// Restart the current chart
        /// </summary>
        public static void Restart()
        {
            BattleHelper.GameRestart();
        }

        /// <summary>
        /// Exit the current chart
        /// </summary>
        public static void Exit()
        {
            BattleHelper.GameFinish();
        }
    }
}
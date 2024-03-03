using Il2CppAssets.Scripts.Database;
using Il2CppAssets.Scripts.GameCore.HostComponent;
using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppFormulaBase;
using Il2CppGameLogic;

namespace MuseDashMirror;

/// <summary>
///     Data inside game scene for battle
/// </summary>
public static partial class BattleComponent
{
    /// <summary>
    ///     Restart the current chart
    /// </summary>
    public static void Restart() => BattleHelper.GameRestart();

    /// <summary>
    ///     Exit the current chart
    /// </summary>
    public static void Exit() => BattleHelper.GameFinish();

    #region ChartInfo

    /// <summary>
    ///     Chart name, only changed when entering the chart
    /// </summary>
    public static string ChartName { get; internal set; }

    /// <summary>
    ///     Level of the chart, only changed when entering the chart
    /// </summary>
    public static string ChartLevel { get; internal set; }

    /// <summary>
    ///     Difficulty of the chart (easy, hard, master, hidden, touhou), only changed when entering the chart
    /// </summary>
    public static int Difficulty { get; internal set; }

    /// <summary>
    ///     Music author, only changed when entering the chart
    /// </summary>
    public static string MusicAuthor { get; internal set; }

    /// <summary>
    ///     Charter, only changed when entering the chart
    /// </summary>
    public static string Charter { get; internal set; }

    /// <summary>
    ///     The name for album package
    /// </summary>
    public static string SelectedAlbumUid => DataHelper.selectedAlbumUid;

    /// <summary>
    ///     The index of selected music in its own album package
    /// </summary>
    public static int SelectedMusicIndex => DataHelper.selectedMusicIndex;

    /// <summary>
    ///     The index of selected music in current category
    /// </summary>
    public static int SelectedMusicIndexInCurrent => DataHelper.selectedMusicIndexInAllList;

    /// <summary>
    ///     The full path for selected music (album+music index)
    /// </summary>
    public static string SelectedMusicUid => DataHelper.selectedMusicUid;

    #endregion ChartInfo

    #region GameInfo

    /// <summary>
    ///     Becomes true when "ready go" finished
    /// </summary>
    public static bool IsInGame => Singleton<StageBattleComponent>.instance.isInGame;

    /// <summary>
    ///     3 decimal places number for game play time, start after isInGame
    /// </summary>
    public static float Tick => Singleton<StageBattleComponent>.instance.realTimeTick;

    /// <summary>
    ///     Perfect count
    /// </summary>
    public static int PerfectCount => Singleton<TaskStageTarget>.instance.m_PerfectResult;

    /// <summary>
    ///     Great count
    /// </summary>
    public static int GreatCount => Singleton<TaskStageTarget>.instance.m_GreatResult;

    /// <summary>
    ///     Normal miss count (without ghost and collectable note miss)
    /// </summary>
    public static int NormalMissCount { get; internal set; }

    /// <summary>
    ///     Ghost miss count
    /// </summary>
    public static int GhostMissCount { get; internal set; }

    /// <summary>
    ///     Collectable note miss count
    /// </summary>
    public static int CollectableNoteMissCount { get; internal set; }

    /// <summary>
    ///     Blue collectable notes count
    /// </summary>
    public static int Get => Singleton<TaskStageTarget>.instance.m_MusicCount + 1;

    /// <summary>
    ///     Heart count
    /// </summary>
    public static int Heart => Singleton<TaskStageTarget>.instance.m_Blood;

    /// <summary>
    ///     Jump over count
    /// </summary>
    public static int JumpOver => Singleton<TaskStageTarget>.instance.m_JumpOverResult;

    /// <summary>
    ///     Music data for the chart, only changed when entering the chart
    /// </summary>
    public static List<MusicData> MusicDataList { get; } = [];

    [ExitGameScene]
    private static void Reset(object e, SceneEventArgs args)
    {
        NormalMissCount = 0;
        GhostMissCount = 0;
        CollectableNoteMissCount = 0;
        MusicDataList?.Clear();
    }

    #endregion GameInfo
}
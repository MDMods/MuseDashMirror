using System;
using System.Collections.Generic;
using Assets.Scripts.Database;
using Assets.Scripts.GameCore.HostComponent;
using Assets.Scripts.PeroTools.Commons;
using FormulaBase;
using GameLogic;
using MuseDashMirror.Patch;

namespace MuseDashMirror;

public static class BattleComponent
{
    /// <summary>
    /// Restart the current chart
    /// </summary>
    public static void Restart() => BattleHelper.GameRestart();

    /// <summary>
    /// Exit the current chart
    /// </summary>
    public static void Exit() => BattleHelper.GameFinish();

    #region ChartInfo

    /// <summary>
    /// Chart name, only changed when entering the chart
    /// </summary>
    public static string ChartName => GetLocalPatch.ChartName;

    /// <summary>
    /// Level of the chart, only changed when entering the chart
    /// </summary>
    public static string ChartLevel => HideBmsCheckPatch.ChartLevel;

    /// <summary>
    /// Difficulty of the chart (easy, hard, master, hidden, touhou), only changed when entering the chart
    /// </summary>
    public static int Difficulty => HideBmsCheckPatch.Difficulty;

    /// <summary>
    /// Music author, only changed when entering the chart
    /// </summary>
    public static string MusicAuthor => HideBmsCheckPatch.MusicAuthor;

    /// <summary>
    /// Charter, only changed when entering the chart
    /// </summary>
    public static string Charter => HideBmsCheckPatch.Charter;

    /// <summary>
    /// The name for album package
    /// </summary>
    public static string SelectedAlbumUid => DataHelper.selectedAlbumUid;

    /// <summary>
    /// The index of selected music in its own album package
    /// </summary>
    public static int SelectedMusicIndex => DataHelper.selectedMusicIndex;

    /// <summary>
    /// The index of selected music in current category
    /// </summary>
    public static int SelectedMusicIndexInCurrent => DataHelper.selectedMusicIndexInAllList;

    /// <summary>
    /// The full path for selected music (album+music index)
    /// </summary>
    public static string SelectedMusicUid => DataHelper.selectedMusicUid;

    #endregion ChartInfo

    #region GameInfo

    /// <summary>
    /// An event to invoke methods when game starts
    /// </summary>
    public static event Action GameStartEvent;

    /// <summary>
    /// An event to invoke methods on victory screen
    /// </summary>
    public static event Action OnVictoryEvent;

    /// <summary>
    /// An event to invoke methods when adding score
    /// </summary>
    public static event Action AddScoreEvent;

    /// <summary>
    /// An event to invoke methods when missing notes
    /// </summary>
    public static event Action MissCubeEvent;

    internal static void GameStartEventInvoke() => GameStartEvent?.Invoke();
    internal static void OnVictoryEventInvoke() => OnVictoryEvent?.Invoke();
    internal static void AddScoreEventInvoke() => AddScoreEvent?.Invoke();
    internal static void MissCubeEventInvoke() => MissCubeEvent?.Invoke();

    /// <summary>
    /// Becomes true when "ready go" finished
    /// </summary>
    public static bool IsInGame => Singleton<StageBattleComponent>.instance.isInGame;

    /// <summary>
    /// 3 decimal places number for game play time, start after isInGame
    /// </summary>
    public static float Tick => Singleton<StageBattleComponent>.instance.realTimeTick;

    /// <summary>
    /// Perfect count
    /// </summary>
    public static int PerfectNum => Singleton<TaskStageTarget>.instance.m_PerfectResult;

    /// <summary>
    /// Great count
    /// </summary>
    public static int GreatNum => Singleton<TaskStageTarget>.instance.m_GreatResult;

    /// <summary>
    /// Normal miss count (without ghost and collectable note miss)
    /// </summary>
    public static int NormalMissNum => GameMissPlayPatch.NormalMiss;

    /// <summary>
    /// Ghost miss count
    /// </summary>
    public static int GhostMissNum => GameMissPlayPatch.GhostMiss;

    /// <summary>
    /// Collectable note miss count
    /// </summary>
    public static int CollectableNoteMissNum => GameMissPlayPatch.CollectableNoteMiss;

    /// <summary>
    /// Total miss count
    /// </summary>
    public static int TotalMissNum => GameMissPlayPatch.TotalMiss;

    /// <summary>
    /// Blue collectable notes count
    /// </summary>
    public static int Get => Singleton<TaskStageTarget>.instance.m_MusicCount + 1;

    /// <summary>
    /// Heart count
    /// </summary>
    public static int Heart => Singleton<TaskStageTarget>.instance.m_Blood;

    /// <summary>
    /// Jump over count
    /// </summary>
    public static int JumpOver => Singleton<TaskStageTarget>.instance.m_JumpOverResult;

    /// <summary>
    /// Music data for the chart, only changed when entering the chart
    /// </summary>
    public static List<MusicData> MusicDatas => StageBattleComponentPatch.MusicDatas;

    #endregion GameInfo
}
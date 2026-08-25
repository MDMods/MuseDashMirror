# Game Data

`PlayerData` exposes persistent player selections and preferences. `BattleComponent` exposes values for the selected chart and the active gameplay scene.

These APIs mirror game-owned state; they do not create an independent data model. Read them only after the corresponding game systems have initialized.

## Player data

`PlayerData` provides the following read-only views:

| Member | Value |
| --- | --- |
| `PlayerLevel` | Current player level. |
| `PlayerName` | Current nickname. |
| `Collections` | Favorite-chart identifiers. |
| `History` | Play-history identifiers. |
| `Hides` | Hidden-chart identifiers. |
| `SelectedCharacterIndex` | Selected character/costume index. |
| `SelectedElfinIndex` | Selected Elfin index. |
| `Offset` | Music offset. |
| `IsAutoFever` | Auto-fever preference. |

`GetSelectedCharacterName` and `GetSelectedElfinName` resolve the selected index through the game's configuration data. Pass `false` to request the non-localized value.

The matching setter methods update the game fields directly:

```csharp
using MuseDashMirror;

PlayerData.SetCharacter(characterIndex: 0);
PlayerData.SetElfin(elfinIndex: 0);
PlayerData.SetOffset(offset: 0);
PlayerData.SetAutoFever(autoFever: true);
```

> [!IMPORTANT]
> These methods do not validate indexes, present confirmation UI, or explicitly save the database. Call them from a lifecycle point where the game data is initialized and only with values accepted by the current game version.

## Selected chart information

`BattleComponent` exposes metadata collected during chart selection:

| Member | Description |
| --- | --- |
| `ChartName` | Selected chart name. |
| `ChartLevel` | Displayed chart level. |
| `Difficulty` | Selected difficulty index. |
| `MusicAuthor` | Music author. |
| `Charter` | Level designer. |
| `SelectedAlbumUid` | Selected album-package identifier. |
| `SelectedMusicIndex` | Music index inside its album. |
| `SelectedMusicIndexInCurrent` | Music index inside the current category. |
| `SelectedMusicUid` | Full selected-music identifier. |

The chart-name and difficulty metadata is updated by patches in the selection flow. It is intended to be read when entering or starting a chart, not during early mod initialization.

## Active battle information

During gameplay, the following values are read from the live `StageBattleComponent` and `TaskStageTarget` singletons:

- `IsInGame` becomes true after the ready/go sequence.
- `Tick` is the current real gameplay time.
- `PerfectCount`, `GreatCount`, `Get`, `Heart`, and `JumpOver` expose current result counters.
- `MusicDataList` contains the chart's `MusicData` items after they have been copied from the stage component.

Use a chart callback before reading these values:

```csharp
using MelonLoader;
using MuseDashMirror;
using MuseDashMirror.Attributes.EventAttributes.PatchEvents;
using MuseDashMirror.EventArguments;

namespace ExampleMod;

internal static partial class BattleHooks
{
    [GameStartPatch]
    private static void OnGameStart(object _, GameStartEventArgs args)
    {
        MelonLogger.Msg($"Starting {BattleComponent.ChartName} at level {BattleComponent.ChartLevel}");
    }

    [AddScorePatch]
    private static void OnAddScore(object _, AddScoreEventArgs args)
    {
        MelonLogger.Msg($"Note {args.Id}: +{args.Value}");
    }
}
```

> [!WARNING]
> `MusicDataList` is currently filled on a background task after `GameStartPatch` has been raised. Do not assume it is complete inside the `GameStartPatch` handler, and do not enumerate it concurrently while it is being populated.

> [!WARNING]
> `NormalMissCount`, `GhostMissCount`, and `CollectableNoteMissCount` are reset when the game scene exits, but the current library contains no update path for them. Do not use those three properties as live miss counters until that runtime tracking is implemented.

## Battle actions and cleanup

Call `BattleComponent.Restart()` or `BattleComponent.Exit()` only while the battle systems are active. They forward directly to the game's restart and finish helpers.

When the gameplay scene exits, MuseDashMirror clears `MusicDataList` and resets its stored miss-count fields. Values backed directly by game singletons should no longer be read after that scene has been torn down.

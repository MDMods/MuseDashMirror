# Events and Scenes

MuseDashMirror exposes each supported callback in two forms:

- Add an attribute to a static method and let the source generator register it.
- Subscribe directly to the corresponding public event in `SceneInfo` or `PatchEvents`.

Attributes are concise and do not perform runtime reflection. Direct subscriptions are useful when your mod needs explicit subscription lifetime or dynamic handlers.

## Scene callbacks

A generated scene handler must return `void`, be static, belong to a `partial` class, and accept exactly `object` followed by `SceneEventArgs`:

```csharp
using MuseDashMirror.Attributes.EventAttributes.SceneEvents;
using MuseDashMirror.EventArguments;

namespace ExampleMod;

internal static partial class SceneHooks
{
    [EnterGameScene]
    private static void OnEnterGame(object _, SceneEventArgs args)
    {
        // The GameMain scene has finished loading.
    }

    [ExitGameScene]
    private static void OnExitGame(object _, SceneEventArgs args)
    {
        // The GameMain scene has been unloaded.
    }
}
```

The following attributes and events are available:

| Scene | Enter attribute / event | Exit attribute / event | Unity scene name |
| --- | --- | --- | --- |
| Any scene | `EnterScene` / `SceneInfo.OnEnterScene` | `ExitScene` / `SceneInfo.OnExitScene` | Any name |
| Main menu | `EnterMainScene` / `SceneInfo.OnEnterMainScene` | `ExitMainScene` / `SceneInfo.OnExitMainScene` | `UISystem_PC` |
| Gameplay | `EnterGameScene` / `SceneInfo.OnEnterGameScene` | `ExitGameScene` / `SceneInfo.OnExitGameScene` | `GameMain` |
| Loading | `EnterLoadingScene` / `SceneInfo.OnEnterLoadingScene` | `ExitLoadingScene` / `SceneInfo.OnExitLoadingScene` | `Loading` |
| Welcome | `EnterWelcomeScene` / `SceneInfo.OnEnterWelcomeScene` | `ExitWelcomeScene` / `SceneInfo.OnExitWelcomeScene` | `Welcome` |

`SceneEventArgs.BuildIndex` and `SceneEventArgs.SceneName` identify the scene. `SceneInfo` also exposes `IsMainScene`, `IsGameScene`, `IsLoadingScene`, and `IsWelcomeScene` for state checks.

> [!NOTE]
> The general enter event runs before the matching specialized flag is set and before the specialized enter event. The general exit event runs before the matching flag is cleared and before the specialized exit event. In a specialized enter handler the flag is already `true`; in a specialized exit handler it is already `false`.

## Game patch callbacks

Game patch events run from Harmony postfixes, after the corresponding game method. A generated patch handler follows the same rules as a scene handler but uses the event-specific argument type:

```csharp
using MuseDashMirror.Attributes.EventAttributes.PatchEvents;
using MuseDashMirror.EventArguments;

namespace ExampleMod;

internal static partial class GameHooks
{
    [GameStartPatch]
    private static void OnGameStart(object _, GameStartEventArgs args)
    {
        var stageBattleComponent = args.StageBattleComponent;
    }

    [AddScorePatch]
    private static void OnAddScore(object _, AddScoreEventArgs args)
    {
        var noteId = args.Id;
        var scoreValue = args.Value;
    }
}
```

| Attribute | Public event | Runs after | Event arguments |
| --- | --- | --- | --- |
| `GameInitPatch` | `PatchEvents.GameInitPatch` | `GameInit.Awake` | `GameInitEventArgs` |
| `PnlMenuPatch` | `PatchEvents.PnlMenuPatch` | `PnlMenu.Awake` | `PnlMenuEventArgs` |
| `PnlStagePatch` | `PatchEvents.PnlStagePatch` | `PnlStage.Awake` | `PnlStageEventArgs` |
| `MenuSelectPatch` | `PatchEvents.MenuSelectPatch` | `MenuSelect.OnToggleChanged` | `MenuSelectEventArgs` |
| `PnlVictoryPatch` | `PatchEvents.PnlVictoryPatch` | `PnlVictory.OnVictory` | `PnlVictoryEventArgs` |
| `GameStartPatch` | `PatchEvents.GameStartPatch` | `StageBattleComponent.GameStart` | `GameStartEventArgs` |
| `AddScorePatch` | `PatchEvents.AddScorePatch` | `TaskStageTarget.AddScore` | `AddScoreEventArgs` |
| Direct event only | `PatchEvents.SwitchLanguagesPatch` | `SwitchLanguages.OnClick` | `SwitchLanguagesEventArgs` |

All current event invocations pass `null` as `sender`; use the strongly typed event arguments instead of depending on `sender`.

### Language-switch event

Subscribe to the language-switch event directly:

```csharp
using MuseDashMirror;
using MuseDashMirror.EventArguments;

PatchEvents.SwitchLanguagesPatch += (_, args) =>
{
    var languageArgs = (SwitchLanguagesEventArgs)args;
    var switchLanguages = languageArgs.SwitchLanguages;
};
```

> [!WARNING]
> The current `SwitchLanguagePatchAttribute` name does not match the plural public event and event-argument names, so its generated subscription is invalid. Use `PatchEvents.SwitchLanguagesPatch` until those API names are aligned.

## Choosing the right callback

- Use `EnterMainScene` or `PnlMenuPatch` to build option-menu UI. `PnlMenuPatch` also gives direct access to the `PnlMenu` instance and runs after MuseDashMirror caches the menu objects used by `ToggleUtils`.
- Use `GameStartPatch` for chart-start logic and access to `StageBattleComponent`.
- Use `AddScorePatch` for individual scoring calls. It supplies the note id, score value, note type, air-note flag, and time.
- Use `EnterGameScene` and `ExitGameScene` for scene-level allocation and cleanup that is not tied to a patched game method.
- Use `EnterScene` and `ExitScene` only when the logic truly applies to every scene.

See [Game data](game-data.md) for the availability of `BattleComponent` values and [Source generators](source-generators.md) for registration requirements and diagnostics.

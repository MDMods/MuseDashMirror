# MuseDashMirror

A mod that makes it easier to access PlayerData, BattleComponents and Create UI

## To Access and Change PlayerData

Instead of using Singleton, you can use PlayerData class to access the data you needed.

For example, the way to access selected elfin name.

```C#	
// Normal way to do
Singleton<ConfigManager>.instance.GetJson("elfin", true)[SelectedElfinIndex]["name"].ToString();

// New way to do
PlayerData.SelectedElfinName;
```

Also supports change some settings like changing characters.

```c#
// Normal way to do
Singleton<DataManager>.instance["Account"]["SelectedRoleIndex"].Set(new Il2CppSystem.Int32() { m_value = characterIndex }.BoxIl2CppObject());

// New way to do
PlayerData.SetCharacter(characterIndex);
```

---

## To Access Battle Component

Instead of using harmony patching on your own, you can use BattleComponent class to access many data when in the chart.

For example, the chart name.

```C#
// Normal way to do
[HarmonyPatch(typeof(MusicInfo), "GetLocal")]
    internal static class GetLocalPatch
    {
        internal static string ChartName { get; set; }

        private static void Postfix(LocalALBUMInfo __result)
        {
            ChartName = __result.name;
        }
    }

// New way to do
BattleComponent.ChartName;
```

Also supports some in game data like in game, note count, music data

```C#	
// Normal way to do
Singleton<StageBattleComponent>.instance.isInGame;
Singleton<TaskStageTarget>.instance.m_PerfectResult;

// New way to do
BattleComponent.IsInGame;
BattleComponent.Perfect;

// Normal way to do 
[HarmonyPatch(typeof(StageBattleComponent), "GameStart")]
    internal static class StageBattleComponentPatch
    {
        internal static List<MusicData> MusicDatas { get; set; } = new List<MusicData>();

        private static void Postfix(StageBattleComponent __instance)
        {
            foreach (var musicdata in __instance.GetMusicData())
            {
                MusicDatas.Add(musicdata);
            }
        }
    }

// New way to do
BattleComponent.MusicDatas;
```

---

## To Better Invoke Patching Methods

Now you can use the default events or manual patching methods in MuseDashMirror.CommonPatches to easier invoke the methods at some common point.

For example, to invoke methods when PnlMenu's Awake method invokes:

```c#
// Normal way to do
[HarmonyPatch(typeof(PnlMenu), "Awake")]
    internal static class PnlMenuPatch
    {
        private static void Postfix(PnlMenu __instance)
        {
            YourMethod();
        }
    }

// New way to do
// Instead, just add your methods to the events in OnInitializeMelon method
public override void OnInitializeMelon()
        {
            PatchEvents.PnlMenuEvent += new Action<PnlMenu>(YourMethod);
        }

// Or you want to use manual patching
ManualPatches.PnlMenuPatch(typeof(YourClass), "MethodName");
```

---

## To Better Invoke Methods When Scene Load/Unload

As with Patching methods, you can also add methods to events in SceneInfo class.

To invoke methods when entering the game scene:

```c#
// Normal way to do
public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "GameMain")
            {
                YourMethod();
            }
        }

// New way to do
// Same with patching events, in the OnInitializeMelon method
public override void OnInitializeMelon()
        {
            SceneInfo.EnterGameScene += new Action(YourMethod);
        }
```

---

## To Create UI Elements

In UICreate namespace there are classes for supporting easier ui create

* Fonts class support 4 default fonts: **SnapsTaste, Normal, SourceHanSansCN-Heavy, MiniSimpleSuperThickBlack**

* Colors class support 5 default colors: **Blue, Silver, ToggleTextColor, ToggleCheckBoxColor, ToggleCheckMarkColor**

* CanvasCreate class has ``CreateCanvas()``method, which has 4 overloads. 

  * If only give the canvas name the canvas will be a ScreenSpaceOverlay canvas.
  * If giving canvas name and corresponding camera name the canvas will be a ScreenSpaceCamera canvas. 
  * You can also use the string to set the canvas parent with the parent name.
  * Or use the final reload, passing an existing GameObject as the parent of the canvas.
  
* TextGameObjectCreate class has ``CreateTextGameObject()`` method, which has 8 overloads.

  * 4 of the overloads use string to find the parent GameObject in scene, and for the other 4 overloads you can pass the existing GameObject as parent.
  
  * The necessary parameters are:  
  
    * The parent for the text GameObject 
    * The GameObject name
    * The text of GameObject
    * The alignment (use TextAnchor enum)
    * The position is local position or not 
    * The position
    * The size delta of the transform
    * The font size
  
  * You can use custom font, custom text color for the gameobject with following 3 overloads
  
* ToggleCreate class offers the general toggle create method `CreateToggle` with 18 overloads

  * The necessary parameters are:
    * The name of the toggle
    * The position of the toggle
    * The bool pointer you want to pass
    * The text of the toggle
    * The parent (with 6 overloads using parent name string, 6 overloads using parent GameObject, 6 overloads using parent Transform)
  * With the left 5 overloads for each scenario, you could set the custom font size, color and toggle group.

  * The class also offers the specific method `CreatePnlMenuToggle`, without needing set the position of the toggle, all you need is pass the name of the toggle, and bool pointer, and the text of the toggle. 
    * The 2 overloads are for more custom settings, like font size and text color.
    * You can also use the left 2 overloads to set your toggle in ToggleGroup

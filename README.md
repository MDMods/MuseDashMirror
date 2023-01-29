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

Also supports change some settings.

```c#
// Normal way to do
Singleton<DataManager>.instance["Account"]["SelectedRoleIndex"].Set(new Il2CppSystem.Int32() { m_value = characterIndex }.BoxIl2CppObject());

// New way to do
PlayerData.SetCharacter(characterIndex);
```

---

## To Access Battle Component

Instead of using harmony patching on your own, you can use BattleComponent class to access many datas when in the chart.

For Example, the chart name.

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

## To Create UI Elements

* UICreate class support 4 default fonts: **SnapsTaste, Normal, SourceHanSansCN-Heavy, MiniSimpleSuperThickBlack**

  -If you want to use the default font, invoke `` LoadFonts()`` before you use the font. Then after using invoke ``UnloadFonts()``.

* UICreate class also support 5 default colors: **Blue, Silver, ToggleTextColor, ToggleCheckBoxColor, ToggleCheckMarkColor**

* UICreate class has ``CreateCanvas()``method, which has 3 overloads. 

  -If only give the canvas name the canvas will be a ScreenSpaceOverlay canvas.

  -If give canvas name and corresponding camera name the canvas will be a ScreenSpaceCamera canvas. 

  -The left one overload is for custom setting reference resolution.

* UICreate class has ``CreateTextGameObject()`` method, which has 4 overloads.

  -The necessary parameters are the canvas name you want the text gameobject to set parent,  the gameobject name, the gameobject text, the alignment, and the position is local position or not, the position, the size delta of the transform, and the font size

  -You can use custom font, custom text color and custom local scale for the gameobject with following 3 overloads


using System.Reflection;
using MelonLoader;
using Main = MuseDashMirror.Main;
using static MuseDashMirror.MelonBuildInfo;

[assembly: AssemblyTitle(Name)]
[assembly: AssemblyDescription(Description)]
[assembly: AssemblyCompany(Name)]
[assembly: AssemblyVersion(ModVersion)]
[assembly: AssemblyFileVersion(ModVersion)]
[assembly: MelonInfo(typeof(Main), Name, ModVersion, Author)]
[assembly: MelonGame("PeroPeroGames", "MuseDash")]
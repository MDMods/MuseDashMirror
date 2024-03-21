using System.Reflection;
using Main = MuseDashMirror.Main;
using static MuseDashMirror.MelonBuildInfo;

[assembly: AssemblyTitle(Name)]
[assembly: AssemblyDescription(Description)]
[assembly: AssemblyCompany(Name)]
[assembly: AssemblyVersion(ModVersion)]
[assembly: AssemblyFileVersion(ModVersion)]
[assembly: AssemblyCopyright("Copyright © lxy 2024")]
[assembly: MelonInfo(typeof(Main), Name, ModVersion, Author)]
[assembly: MelonGame("PeroPeroGames", "MuseDash")]
[assembly: MelonPriority(int.MinValue)]
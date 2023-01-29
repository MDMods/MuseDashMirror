using Assets.Scripts.PeroTools.Nice.Interface;
using Il2CppSystem;

namespace MuseDashMirror
{
    public static class Utils
    {
        public static T Get<T>(this IVariable variable)
        {
            return VariableUtils.GetResult<T>(variable);
        }

        public static void Set(this IVariable variable, Object value)
        {
            VariableUtils.SetResult(variable, value);
        }
    }
}
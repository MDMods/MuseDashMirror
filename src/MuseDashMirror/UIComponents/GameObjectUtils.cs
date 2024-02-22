namespace MuseDashMirror.UIComponents;

/// <summary>
///     Methods related with GameObject
/// </summary>
[Logger]
[RegisterInMuseDashMirror]
public static partial class GameObjectUtils
{
    /// <summary>
    ///     Cache for GameObjects
    /// </summary>
    internal static readonly Dictionary<string, GameObject> GameObjectCache = new();

    /// <summary>
    ///     Get GameObject with specified name
    /// </summary>
    /// <param name="gameObjectName">GameObject name</param>
    /// <returns>GameObject</returns>
    public static GameObject GetGameObject(string gameObjectName)
    {
        if (GameObjectCache.TryGetValue(gameObjectName, out var gameObject))
        {
            return gameObject;
        }

        gameObject = GameObject.Find(gameObjectName);
        if (gameObject == null)
        {
            Logger.Error($"GameObject with name {gameObjectName} is not found");
            return gameObject;
        }

        GameObjectCache[gameObjectName] = gameObject;
        return gameObject;
    }

    [ExitScene]
    private static void ClearCache(object e, SceneEventArgs args) => GameObjectCache.Clear();
}
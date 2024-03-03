namespace MuseDashMirror.Utils;

/// <summary>
///     Methods related with GameObject
/// </summary>
[Logger]
public static partial class GameObjectUtils
{
    /// <summary>
    ///     Cache for GameObjects
    /// </summary>
    internal static readonly Dictionary<string, GameObject> GameObjectCache = new();

    /// <summary>
    ///     Get GameObject with specified path/name
    /// </summary>
    /// <param name="gameObjectPath">GameObject Path</param>
    /// <returns>GameObject</returns>
    public static GameObject GetGameObject(string gameObjectPath)
    {
        var gameObjectName = gameObjectPath.Split('/')[^1];
        if (GameObjectCache.TryGetValue(gameObjectName, out var gameObject))
        {
            return gameObject;
        }

        gameObject = GameObject.Find(gameObjectPath);
        if (gameObject == null)
        {
            Logger.Error($"GameObject with path {gameObjectPath} is not found");
            return gameObject;
        }

        GameObjectCache[gameObjectName] = gameObject;
        return gameObject;
    }

    [ExitScene]
    private static void ClearCache(object e, SceneEventArgs args) => GameObjectCache.Clear();
}
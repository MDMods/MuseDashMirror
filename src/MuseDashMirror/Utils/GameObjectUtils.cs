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
            if (!TryGetInactiveGameObject(gameObjectName, ref gameObject))
            {
                Logger.Error($"GameObject with path {gameObjectPath} is not found");
                return gameObject;
            }

            gameObject.SetActive(true);
        }

        GameObjectCache[gameObjectName] = gameObject;
        return gameObject;
    }

    private static bool TryGetInactiveGameObject(string gameObjectname, ref GameObject gameObject)
    {
        var gameObjectsTransforms = Resources.FindObjectsOfTypeAll<Transform>().ToArray();
        var transform = Array.Find(gameObjectsTransforms, transform => transform.name.Equals(gameObjectname));

        if (Equals(transform, default(Transform)))
        {
            gameObject = null;
            return false;
        }

        gameObject = transform.gameObject;
        return true;
    }

    [ExitScene]
    private static void ClearCache(object e, SceneEventArgs args) => GameObjectCache?.Clear();
}
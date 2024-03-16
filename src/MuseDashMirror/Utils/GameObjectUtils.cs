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
    ///     Get GameObject with specified path/name<br />
    ///     Able to find unactive GameObject with path
    /// </summary>
    /// <param name="gameObjectPath">GameObject Path</param>
    /// <param name="cacheTargetGameObject">Whether to cache the target GameObject or not, default to false</param>
    /// <param name="cacheNodeGameObjects">Whether to cache all the GameObjects in the path or not, default to false</param>
    /// <returns>GameObject</returns>
    public static GameObject GetGameObjectWithPath(string gameObjectPath, bool cacheTargetGameObject = false, bool cacheNodeGameObjects = false)
    {
        var nodePaths = gameObjectPath.Split('/');
        var gameObjectName = nodePaths[^1];

        if (GameObjectCache.TryGetValue(gameObjectName, out var cachedGameObject))
        {
            return cachedGameObject;
        }

        var ancestorGameObjectName = nodePaths[0];
        var ancestorGameObject = GameObject.Find(ancestorGameObjectName);
        if (ancestorGameObject == null)
        {
            Logger.Error($"GameObject with name {ancestorGameObjectName} is not found");
            return null;
        }

        if (cacheTargetGameObject)
        {
            GameObjectCache[ancestorGameObjectName] = ancestorGameObject;
        }

        return nodePaths.Length == 1 ? ancestorGameObject : GetGameObjectWithSplitPath(ancestorGameObject, cacheNodeGameObjects, nodePaths[1..]);
    }

    /// <summary>
    ///     Use Transform.Find to get GameObject with split path
    /// </summary>
    /// <param name="ancestorGameObject">Ancestor GameObject</param>
    /// <param name="cacheNodeGameObjects">Whether to cache all the GameObjects in the path or not, default to false</param>
    /// <param name="nodePaths">Node GameObjects Paths</param>
    /// <returns>GameObject</returns>
    public static GameObject GetGameObjectWithSplitPath(GameObject ancestorGameObject, bool cacheNodeGameObjects = false, params string[] nodePaths)
    {
        var currentGameObject = ancestorGameObject;
        foreach (var nodePath in nodePaths)
        {
            if (GameObjectCache.TryGetValue(nodePath, out var cachedGameObject))
            {
                currentGameObject = cachedGameObject;
                continue;
            }

            var childTransform = currentGameObject.transform.Find(nodePath);
            if (childTransform == null)
            {
                Logger.Error($"GameObject with name {nodePath} is not found");
                return null;
            }

            currentGameObject = childTransform.gameObject;
            if (cacheNodeGameObjects)
            {
                GameObjectCache[nodePath] = currentGameObject;
            }
        }

        return currentGameObject;
    }

    [ExitScene]
    private static void ClearCache(object e, SceneEventArgs args) => GameObjectCache?.Clear();
}
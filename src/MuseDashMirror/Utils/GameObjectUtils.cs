namespace MuseDashMirror.Utils;

/// <summary>
///     Methods related to <see cref="GameObject" />
/// </summary>
[Logger]
public static partial class GameObjectUtils
{
    /// <summary>
    ///     Cache for GameObjects
    /// </summary>
    internal static readonly Dictionary<string, GameObject> GameObjectCache = [];

    /// <summary>
    ///     Get GameObject with specified path/name<br />
    ///     Able to find inactive GameObject with path
    /// </summary>
    /// <param name="gameObjectPath">GameObject Path</param>
    /// <param name="cacheTargetGameObject">Whether to cache the target GameObject or not, default to false</param>
    /// <param name="cacheNodeGameObjects">Whether to cache all the GameObjects in the path or not, default to false</param>
    /// <returns>GameObject</returns>
    public static GameObject GetGameObject(string gameObjectPath, bool cacheTargetGameObject = false, bool cacheNodeGameObjects = false)
    {
        var nodePaths = gameObjectPath.Split('/');
        var targetGameObjectName = nodePaths[^1];

        return GameObjectCache.TryGetValue(targetGameObjectName, out var cachedGameObject)
            ? cachedGameObject
            : GetGameObjectWithSplitPath(cacheTargetGameObject, cacheNodeGameObjects, nodePaths);
    }

    private static GameObject GetGameObjectWithSplitPath(bool cacheTargetGameObject, bool cacheNodeGameObjects, IReadOnlyList<string> nodePaths)
    {
        var ancestorGameObjectName = nodePaths[0];
        var ancestorGameObject = GetGameObjectFromCacheOrFind(ancestorGameObjectName, nodePaths.Count == 1 ? cacheTargetGameObject : cacheNodeGameObjects);

        if (ancestorGameObject == null)
        {
            return null;
        }

        var currentGameObject = ancestorGameObject;
        for (var i = 1; i < nodePaths.Count; i++)
        {
            var nodeName = nodePaths[i];
            var shouldCache = cacheNodeGameObjects && i != nodePaths.Count - 1
                              || cacheTargetGameObject && i == nodePaths.Count - 1;
            currentGameObject = GetGameObjectFromCacheOrFind(currentGameObject, nodeName, shouldCache);

            if (currentGameObject == null)
            {
                return null;
            }
        }

        return currentGameObject;
    }

    private static GameObject GetGameObjectFromCacheOrFind(string gameObjectName, bool shouldCache)
    {
        if (GameObjectCache.TryGetValue(gameObjectName, out var gameObject))
        {
            return gameObject;
        }

        var existed = NormalFindGameObject(gameObjectName, out gameObject);
        if (!existed)
        {
            return null;
        }

        if (shouldCache)
        {
            GameObjectCache[gameObjectName] = gameObject;
        }

        return gameObject;
    }

    private static GameObject GetGameObjectFromCacheOrFind(GameObject currentGameObject, string gameObjectName, bool shouldCache)
    {
        if (GameObjectCache.TryGetValue(gameObjectName, out var gameObject))
        {
            return gameObject;
        }

        var existed = TransformFindChildGameObject(currentGameObject, gameObjectName, out gameObject);
        if (!existed)
        {
            return null;
        }

        if (shouldCache)
        {
            GameObjectCache[gameObjectName] = gameObject;
        }

        return gameObject;
    }

    private static bool NormalFindGameObject(string gameObjectName, out GameObject gameObject)
    {
        gameObject = GameObject.Find(gameObjectName);
        if (gameObject != null)
        {
            return true;
        }

        Logger.Error($"GameObject with name {gameObjectName} is not found");
        return false;
    }

    private static bool TransformFindChildGameObject(GameObject currentGameObject, string childGameObjectName, out GameObject childGameObject)
    {
        var childTransform = currentGameObject.transform.Find(childGameObjectName);
        if (childTransform != null)
        {
            childGameObject = childTransform.gameObject;
            return true;
        }

        childGameObject = null;
        Logger.Error($"GameObject with name {childGameObjectName} is not found");
        return false;
    }

    [ExitScene]
    private static void ClearCache(object sender, SceneEventArgs args) => GameObjectCache?.Clear();
}
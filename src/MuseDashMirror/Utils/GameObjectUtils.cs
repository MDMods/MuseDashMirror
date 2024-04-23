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
    internal static readonly Dictionary<string, GameObject> GameObjectCache = new();

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

        return nodePaths.Length == 1 ? ancestorGameObject : GetGameObjectWithSplitPath(ancestorGameObject, cacheNodeGameObjects, nodePaths);
    }

    private static GameObject GetGameObjectWithSplitPath(GameObject ancestorGameObject, bool cacheNodeGameObjects, IReadOnlyList<string> nodePaths)
    {
        var currentGameObject = ancestorGameObject;
        for (var i = 1; i < nodePaths.Count; i++)
        {
            var nodeName = nodePaths[i];
            if (GameObjectCache.TryGetValue(nodeName, out var cachedGameObject))
            {
                currentGameObject = cachedGameObject;
                continue;
            }

            var childTransform = currentGameObject.transform.Find(nodeName);
            if (childTransform == null)
            {
                Logger.Error($"GameObject with name {nodeName} is not found");
                return null;
            }

            currentGameObject = childTransform.gameObject;
            if (cacheNodeGameObjects)
            {
                GameObjectCache[nodeName] = currentGameObject;
            }
        }

        return currentGameObject;
    }

    [ExitScene]
    private static void ClearCache(object sender, SceneEventArgs args) => GameObjectCache?.Clear();
}
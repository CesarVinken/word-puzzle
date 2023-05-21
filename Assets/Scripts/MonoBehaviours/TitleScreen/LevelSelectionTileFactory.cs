using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelSelectionTileFactory
{
    private static AssetReferenceGameObject _levelSelectionTilePrefabReference;

    public static void Create(Transform parentTransform, LevelDataModel levelData)
    {
        AssetReferenceGameObject prefabReference = GetPrefabReference();

        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, parentTransform);

        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                OnAssetLoaded(handle, levelData);
            }
            else
            {
                Debug.LogError($"Failed to instantiate tile active event");
            }
        };
    }

    private static AssetReferenceGameObject GetPrefabReference()
    {
        if (_levelSelectionTilePrefabReference != null)
        {
            return _levelSelectionTilePrefabReference;
        }

        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject($"LevelSelectionTile.prefab");

        if (prefabReference == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find an asset for _levelSelectionTilePrefabReference");
        }

        if (_levelSelectionTilePrefabReference != null)
        {
            return _levelSelectionTilePrefabReference;
        }

        _levelSelectionTilePrefabReference = prefabReference;

        return prefabReference;
    }

    private static void OnAssetLoaded(AsyncOperationHandle<GameObject> handle, LevelDataModel levelData)
    {
        LevelSelectionTile levelSelectionTile = handle.Result.GetComponent<LevelSelectionTile>();
        levelSelectionTile.Setup();
        levelSelectionTile.Initialise(levelData);
    }
}

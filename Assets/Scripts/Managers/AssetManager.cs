using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CharacterTileFactory
{
    private static AssetReferenceGameObject _characterTilePrefabReference;

    public static void Create(Transform parentTransform, CharacterTileHandler characterTileHandler, CharacterTileDataModel characterTileData)
    {
        AssetReferenceGameObject prefabReference = GetPrefabReference();

        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(prefabReference, parentTransform);

        handle.Completed += (o) =>
        {
            if (o.Status == AsyncOperationStatus.Succeeded)
            {
                OnAssetLoaded(handle, characterTileHandler, characterTileData);
            }
            else
            {
                Debug.LogError($"Failed to instantiate tile active event");
            }
        };
    }

    private static AssetReferenceGameObject GetPrefabReference()
    {
        if (_characterTilePrefabReference != null)
        {
            return _characterTilePrefabReference;
        }

        AssetReferenceGameObject prefabReference = new AssetReferenceGameObject($"CharacterTile.prefab");

        if (prefabReference == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find an asset for _characterTilePrefabReference");
        }

        if (_characterTilePrefabReference != null)
        {
            return _characterTilePrefabReference;
        }

        _characterTilePrefabReference = prefabReference;

        return prefabReference;
    }

    private static void OnAssetLoaded(AsyncOperationHandle<GameObject> handle, CharacterTileHandler characterTileHandler, CharacterTileDataModel characterTileData)
    {
        RectTransform tileRectTransform = handle.Result.GetComponent<RectTransform>();

        Vector3 tilePosition = new Vector3((characterTileData.TilePosition.x * 12), characterTileData.TilePosition.y * 12, characterTileData.TilePosition.z);

        tileRectTransform.localPosition = tilePosition;

        CharacterTileMono characterTile = handle.Result.GetComponent<CharacterTileMono>();

        characterTile.Setup(characterTileData);
        characterTileHandler.OnCharacterTileCreated(handle.Result, characterTile);
    }
}

using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;

    [SerializeField] private GameObject _characterTilePrefab;

    public void Awake()
    {
        Instance = this;

        if (_characterTilePrefab == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Could not find character tile prefab");
        }
    }

    public GameObject GetCharacterTilePrefab()
    {
        return _characterTilePrefab;
    }
}

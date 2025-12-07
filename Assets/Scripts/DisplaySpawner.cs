using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DisplaySpawner<T> : MonoBehaviour where T : SpawnObject<T>
{
    [SerializeField] protected Spawner<T> _spawner;

    protected TextMeshProUGUI _textMeshPro;

    protected virtual void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        UpdateInfo();
    }

    protected virtual void OnEnable()
    {
        _spawner.ChangedCount += UpdateInfo;
    }

    protected virtual void OnDisable()
    {
        _spawner.ChangedCount -= UpdateInfo;
    }

    protected virtual void UpdateInfo()
    {
        _textMeshPro.text =
            $"{_spawner.gameObject.name}\n" +
            $"Created objects: {_spawner.ValueOfCreatedObjects}\n" +
            $"Spawned objects: {_spawner.ValueOfSpawnedObjects}\n" +
            $"Active objects: {_spawner.ValueOfActiveObjects}";
    }
}

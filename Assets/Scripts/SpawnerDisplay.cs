using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public abstract class SpawnerDisplay<T> : MonoBehaviour where T : SpawnObject
{
    [SerializeField] protected Spawner<T> _spawner;

    protected TextMeshProUGUI _textMeshPro;

    protected void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        UpdateInfo();
    }

    private void OnEnable()
    {
        _spawner.ChangedCount += UpdateInfo;
    }

    private void OnDisable()
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

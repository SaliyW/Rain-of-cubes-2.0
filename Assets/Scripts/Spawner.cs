using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : SpawnObject<T>
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _maxSize = 10;

    public UnityAction ChangedCount;
    protected ObjectPool<T> _pool;

    public int ValueOfCreatedObjects { get; private set; }
    public int ValueOfSpawnedObjects { get; private set; }
    public int ValueOfActiveObjects { get; private set; }

    protected void Awake()
    {
        _pool = new ObjectPool<T>(
        createFunc: () => ActionOnCreate(),
        actionOnGet: (obj) => ActionOnGet(obj),
        actionOnRelease: (obj) => ActionOnRelease(obj),
        actionOnDestroy: (obj) => ActionOnDestroy(obj),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _maxSize);
    }

    protected void GetObject()
    {
        _pool.Get();

        ValueOfSpawnedObjects++;
        ValueOfCreatedObjects = _pool.CountAll;
        ValueOfActiveObjects = _pool.CountActive;

        ChangedCount?.Invoke();
    }

    protected virtual T ActionOnCreate()
    {
        return Instantiate(_prefab);
    }

    protected virtual void ActionOnGet(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    protected virtual void ActionOnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    protected virtual void ActionOnDestroy(T obj)
    {
        Destroy(obj.gameObject);
    }
}
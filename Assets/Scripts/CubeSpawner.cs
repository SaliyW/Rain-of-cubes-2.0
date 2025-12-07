using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Platform _mainPlatform;
    [SerializeField] private float _delay;

    public event UnityAction<Cube> Spawned;
    private Coroutine _coroutine;

    private void Start()
    {
        _coroutine = StartCoroutine(GenerateCubes(_delay));
    }

    private IEnumerator GenerateCubes(float delay)
    {
        while (true)
        {
            GetObject();

            yield return new WaitForSecondsRealtime(delay);
        }
    }

    protected override void ActionOnGet(Cube cube)
    {
        base.ActionOnGet(cube);

        cube.transform.position = GetSpawnPosition();
        Spawned?.Invoke(cube);
        cube.ReadyToRelease += _pool.Release;
    }

    protected override void ActionOnRelease(Cube cube)
    {
        base.ActionOnRelease(cube);

        cube.ReadyToRelease -= _pool.Release;
    }

    private Vector3 GetSpawnPosition()
    {
        float minPositionX = _mainPlatform.Bounds.min.x;
        float maxPositionX = _mainPlatform.Bounds.max.x;
        float minPositionZ = _mainPlatform.Bounds.min.z;
        float maxPositionZ = _mainPlatform.Bounds.max.z;
        float randomPositionX = Random.Range(minPositionX, maxPositionX);
        float randomPositionZ = Random.Range(minPositionZ, maxPositionZ);
        float positionY = 18;

        return new Vector3(randomPositionX, positionY, randomPositionZ);
    }
}
using UnityEngine;
using UnityEngine.Events;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Platform _mainPlatform;
    [SerializeField] private float _repeatRate;

    public event UnityAction<Cube> Spawned;

    private void Start()
    {
        InvokeRepeating(nameof(GetObject), 0f, _repeatRate);
    }

    protected override void ActionOnGet(Cube cube)
    {
        base.ActionOnGet(cube);

        cube.transform.position = GetSpawnPosition();
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Spawned?.Invoke(cube);
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
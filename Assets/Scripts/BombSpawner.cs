using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Vector3 _bombPosition;

    private void OnEnable()
    {
        _cubeSpawner.Spawned += SubscribeToEvent;
    }

    private void OnDisable()
    {
        _cubeSpawner.Spawned -= SubscribeToEvent;
    }

    private void SubscribeToEvent(Cube cube)
    {
        cube.ReadyToRelease += GetBomb;
    }

    private void GetBomb(Cube cube)
    {
        _bombPosition = cube.transform.position;

        GetObject();

        cube.ReadyToRelease -= GetBomb;
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        base.ActionOnGet(bomb);

        bomb.transform.position = _bombPosition;
        bomb.ReadyToRelease += _pool.Release;
    }

    protected override void ActionOnRelease(Bomb bomb)
    {
        base.ActionOnRelease(bomb);

        bomb.ReadyToRelease -= _pool.Release;
    }
}
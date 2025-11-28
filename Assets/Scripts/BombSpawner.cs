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
        cube.Destroyed += GetBomb;
    }

    private void GetBomb(Cube cube)
    {
        _bombPosition = cube.transform.position;

        GetObject();

        cube.Destroyed -= GetBomb;
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        base.ActionOnGet(bomb);

        bomb.transform.position = _bombPosition;
        bomb.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
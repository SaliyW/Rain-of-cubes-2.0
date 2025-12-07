using UnityEngine;
using UnityEngine.Events;

public abstract class SpawnObject<T> : MonoBehaviour where T : SpawnObject<T>
{
    [SerializeField] protected float _minRandomTime = 2;
    [SerializeField] protected float _maxRandomTime = 5;

    public UnityAction<T> ReadyToRelease;
    protected Renderer _renderer;
    protected Rigidbody _rigidbody;
    protected Coroutine _coroutine;
    protected Color _awakeColor;
    protected Quaternion _awakeRotation;

    protected virtual void Awake()
    {
        SetDefaultParameters();
    }

    protected virtual void OnDisable()
    {
        ReturnDefaultParameters();
    }

    protected virtual void SetDefaultParameters()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _awakeColor = _renderer.material.color;
        _awakeRotation = transform.rotation;
    }

    protected virtual void ReturnDefaultParameters()
    {
        _renderer.material.color = _awakeColor;
        transform.rotation = _awakeRotation;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }    
}
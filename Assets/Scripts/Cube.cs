using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : SpawnObject<Cube>
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>())
        {
            _coroutine = StartCoroutine(LifeCounter());
        }
    }

    private IEnumerator LifeCounter()
    {
        float lifeTime = Random.Range(_minRandomTime, _maxRandomTime);

        _renderer.material.color = Color.red;

        yield return new WaitForSecondsRealtime(lifeTime);

        ReadyToRelease?.Invoke(this);
    }
}
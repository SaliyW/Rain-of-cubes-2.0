using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Cube : SpawnObject
{
    public UnityAction<Cube> Destroyed;
    private Coroutine _coroutine;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>())
        {
            _coroutine = StartCoroutine(Destruction());
        }
    }

    private IEnumerator Destruction()
    {
        float minTimeOfDestroy = 2;
        float maxTimeOfDestroy = 5;
        float elapsedTime = 0;
        float destructionTime = Random.Range(minTimeOfDestroy, maxTimeOfDestroy);

        GetComponent<Renderer>().material.color = Color.red;

        while (elapsedTime < destructionTime)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
        Destroyed?.Invoke(this);
    }
}
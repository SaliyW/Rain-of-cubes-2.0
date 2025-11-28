using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : SpawnObject
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    public UnityAction Exploded;
    private Renderer _renderer;
    private Coroutine _coroutine;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        _coroutine = StartCoroutine(Dissolve());
    }

    private IEnumerator Dissolve()
    {
        float dissolutionTime = SetDissolutionTime();
        float elapsedTime = 0;
        float targetAlpha = 0;
        Color startColor = _renderer.material.color;
        Color endColor = new(startColor.r, startColor.g, startColor.b, targetAlpha);

        while (elapsedTime < dissolutionTime)
        {
            elapsedTime += Time.deltaTime;
            float interpolationParameter = elapsedTime / dissolutionTime;
            _renderer.material.color = Color.Lerp(startColor, endColor, interpolationParameter);

            yield return null;
        }

        Explode();
    }

    private void Explode()
    {
        Collider[] hits;
        List<Rigidbody> cubes = new();
        float scale = transform.localScale.x;
        float upwardsModifier = 2f;

        hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        foreach (Rigidbody explodableCube in cubes)
        {
            explodableCube.AddExplosionForce(_explosionForce / scale, transform.position, _explosionRadius / scale, upwardsModifier);
        }

        Destroy(gameObject);
        Exploded?.Invoke();
    }

    private float SetDissolutionTime()
    {
        float minTimeOfDissolution = 2;
        float maxTimeOfDissolution = 5;

        return Random.Range(minTimeOfDissolution, maxTimeOfDissolution);
    }
}
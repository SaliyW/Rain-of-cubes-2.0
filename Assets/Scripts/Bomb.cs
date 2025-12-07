using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : SpawnObject<Bomb>
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Dissolve());
    }

    private IEnumerator Dissolve()
    {
        float dissolutionTime;
        float elapsedTime = 0;
        float targetAlpha = 0;
        Color endColor = new(_awakeColor.r, _awakeColor.g, _awakeColor.b, targetAlpha);

        dissolutionTime = Random.Range(_minRandomTime, _maxRandomTime);

        while (elapsedTime < dissolutionTime)
        {
            elapsedTime += Time.deltaTime;
            float interpolationParameter = elapsedTime / dissolutionTime;
            _renderer.material.color = Color.Lerp(_awakeColor, endColor, interpolationParameter);

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

        ReadyToRelease?.Invoke(this);
    }
}
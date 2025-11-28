using UnityEngine;

public class Platform : MonoBehaviour
{
    public Bounds Bounds { get; private set; }

    private void Start()
    {
        Bounds = GetComponent<Renderer>().bounds;
    }
}
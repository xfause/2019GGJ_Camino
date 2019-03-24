using UnityEngine;
using System.Collections;

public class KMEffPlayerOuterCircle : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    public float rotationSpeed = 50f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero/* transform.position*/, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}

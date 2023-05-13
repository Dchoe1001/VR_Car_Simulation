using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    public float rayLength = 10.0f;
    public Vector3[] rayDirections = { Vector3.forward, Vector3.right, Vector3.left };
    public float forwardOffset = 0.0f;
    public float upwardOffset = 0.5f;
    public Color rayColor = Color.yellow;

    public LayerMask layerMask;

    void Update()
    {
        RaycastHit hit;

        foreach (Vector3 direction in rayDirections)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * upwardOffset + transform.forward * forwardOffset;
            if (Physics.Raycast(rayOrigin, transform.TransformDirection(direction), out hit, rayLength, layerMask))
            {
                Debug.DrawRay(rayOrigin, transform.TransformDirection(direction) * hit.distance, rayColor);
                Debug.Log("Hit " + hit.collider.gameObject.name + " at distance " + hit.distance.ToString("F2"));
            }
            else
            {
                Debug.DrawRay(rayOrigin, transform.TransformDirection(direction) * rayLength, Color.white);
            }
        }
    }
}

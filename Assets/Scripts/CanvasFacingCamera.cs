using UnityEngine;

public class CanvasFacingCamera : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Vector3 direction = transform.position - _camera.transform.position;
        float dot = Vector3.Dot(transform.forward, direction.normalized);

        if (dot > 0.9f) // Use a threshold value to determine when the canvas should appear
        {
            // The front of the canvas is facing towards the camera, make it visible
            GetComponent<Canvas>().enabled = true;
        }
        else
        {
            // The front of the canvas is not facing towards the camera, hide it
            GetComponent<Canvas>().enabled = false;
        }
    }
}

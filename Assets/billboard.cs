using UnityEngine;

public class billboard : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        // Find the camera (assumes there's only one main camera)
        cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Rotate the object to face the camera
        transform.LookAt(transform.position + cameraTransform.forward);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowLookAtAndMove : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] private GameObject targetTransform;
    [SerializeField] private float speed = 1.0f;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0, 180, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.transform.position, speed * Time.deltaTime);
    }
}

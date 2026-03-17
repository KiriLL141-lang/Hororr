using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _min;
    [SerializeField] private float _max;

    private float _rotX;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        _rotX -= Input.GetAxis("Mouse Y") * _rotSpeed;
        _rotX = Mathf.Clamp(_rotX, _min, _max);

        transform.localEulerAngles = Vector3.right * _rotX;
    }
}

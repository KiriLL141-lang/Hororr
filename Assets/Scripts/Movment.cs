using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    [SerializeField] private float _walk;
    [SerializeField] private float _run;
    [SerializeField] private float _crouch;
    [SerializeField] private float _gravity;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private int _health;

    private CharacterController _controller;
    private Camera _camera;
    private Vector3 _motion;
    private Vector3 _camForward;
    private float _currentSpeed;
    private float _rotY;
    private AudioSource _audio;
    private bool _isEmpty;
    private Transform _sphera;
 
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _camera = Camera.main;
        _audio = GetComponent<AudioSource>();
        _isEmpty = true;
    }

    void Update()
    {
        _motion.x = Input.GetAxis("Horizontal");
        _motion.z = Input.GetAxis("Vertical");

        _camForward = _camera.transform.forward;
        _camForward.y = 0;
        _motion = Quaternion.LookRotation(_camForward) * _motion;

        _rotY += _rotSpeed * Input.GetAxis("Mouse X");
        transform.localEulerAngles = Vector3.up * _rotY;

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? _run : _walk;
            transform.localScale = Vector3.one;
        }
        else
        {
            _currentSpeed = _crouch;
            transform.localScale = new Vector3(1, 0.5f, 1);
        }

        _controller.Move(_motion * _currentSpeed * Time.deltaTime);

        _motion.y = _controller.isGrounded ? 0 : _motion.y - _gravity * Time.deltaTime;

        if (_motion.x != 0 || _motion.z != 0)
        {
            if (!_audio.isPlaying)
               _audio.Play();
        }
        else
            _audio.Stop();
                
    }

    public void TakeDamage()
    {
        _health--;

        if (_health <= 0)
            Dead();
    }

    public void Dead()
    {
        _camera.GetComponent<CameraController>().enabled = false;
        GameMenu.loose.Invoke();
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphera") && _isEmpty)
        {
            _isEmpty = false;
            other.transform.parent = transform;
            other.transform.position = Vector3.zero;
            _sphera = other.transform;
        }
    }

    public Transform TakeSphera()
    {
        if (!_isEmpty)
        {
            _isEmpty = true;
            _sphera.parent = null;
            return _sphera;
        }
        else
            return null;
    }
}
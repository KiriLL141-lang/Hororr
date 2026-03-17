using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private int _currentPoint;
    private Transform _sphera;

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Movment player))
        {
            if (Input.GetKey(KeyCode.E))
            {

                _sphera = player.TakeSphera();
                if (_sphera != null)
                {
                    _sphera.position = _points[_currentPoint].position;
                    _sphera.parent = transform;
                    _sphera.GetComponent<Collider>().enabled = false;
                    _currentPoint++;

                    if (_currentPoint == 5)
                    {
                        Victory();
                    }
                }
            }
        }
    }

    private void Victory()
    {
        GameMenu.win.Invoke();
        Time.timeScale = 0;
    }
} 
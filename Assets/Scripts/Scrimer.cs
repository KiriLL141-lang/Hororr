using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrimer : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private float _delay;
     
    private AudioSource _audio;
    private Image _image;
    private bool _canScream;

    void Start()
    {
        _canScream = true;
        _image = _panel.GetComponent<Image>();
        _image.sprite = _sprite;
        _image.color = _color;
        _audio = GetComponent<AudioSource>();
        _panel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Movment _) && _canScream)
        {
            StartCoroutine(Scream());
        }   
    }

    private IEnumerator Scream()
    {
        _canScream = false;
        _panel.SetActive(true);
        _audio.Play();
        yield return new WaitForSeconds(_audio.clip.length);
        _panel.SetActive(false);

        yield return new WaitForSeconds(_delay);
        _canScream = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlenderMan : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private float _distance;

    private AudioSource _source;

    void Start()
    {
        _source = _model.GetComponent<AudioSource>();
        _model.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Movment player))
        {
            StartCoroutine(Scream(player.transform));
        }
    }
    
    private IEnumerator Scream(Transform player)
    {
        _model.transform.SetParent(transform);
        _model.SetActive(true);
        _model.transform.localPosition = player.forward * _distance;
        _source.Play();
        _model.transform.LookAt(player);
        yield return new WaitForSeconds(2);
        _model.SetActive(false);
    }
}

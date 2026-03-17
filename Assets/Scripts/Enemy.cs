using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Movment _player;
    [SerializeField] private float _walk;
    [SerializeField] private float _run;
    [SerializeField] private float _range;
    [SerializeField] private Transform[] _points;

    private NavMeshAgent _agent;
    private Animator _animator;
    private AudioSource _audio;
    [SerializeField] private bool _inHunt;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Move", _agent.speed);
        SetPoint();
    }

    void Update()
    {
        if (_agent.remainingDistance < 1)
            SetPoint();

        if (_player.enabled)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) < _range)
            {
                if (!_inHunt)
                    _audio.PlayOneShot(_audio.clip);

                _inHunt = true;
                _agent.destination = _player.transform.position;
                _agent.speed = _run;
                _animator.SetFloat("Move", _agent.speed);
                if (Vector3.Distance(transform.position, _player.transform.position) < _agent.stoppingDistance + 0.1f)
                {
                    if (_player.enabled)
                        StartCoroutine(Attack());
                }
            }
            else
            {
                _agent.speed = _walk;
                _inHunt = false;
                _animator.SetFloat("Move", _agent.speed);
            }
        }
        //else
        //{
        //    _inHunt = false;
        //    _agent.speed = _walk;
        //    _animator.SetFloat("Move", _agent.speed);
        //}
    }

    private void SetPoint()
    {
        int point = Random.Range(0, _points.Length);
        _agent.SetDestination(_points[point].position);
    }

    private IEnumerator Attack()
    {
        float speed = _agent.speed;
        _agent.speed = 0;
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(3.1f);
        _agent.speed = speed;
    }

    public void Hit()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 3)
        {
            _player.TakeDamage();
        }
    }
}

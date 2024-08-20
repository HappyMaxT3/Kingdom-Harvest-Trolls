using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollAnimationController : MonoBehaviour
{
    TrollController trollController;

    private Animator _animator;
    private Rigidbody2D _body;

    private void Start()
    {
        trollController = GetComponent<TrollController>();
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _animator.SetFloat("velocity", Mathf.Abs(_body.velocity.x) + Mathf.Abs(_body.velocity.y));
        _animator.SetBool("is_attacking", trollController.is_attacing);
    }
}

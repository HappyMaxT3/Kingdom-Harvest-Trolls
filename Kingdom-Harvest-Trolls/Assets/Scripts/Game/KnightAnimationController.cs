using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    KnightController knightController;

    private Animator _animator;
    private Rigidbody2D _body;

    private void Start()
    {
        knightController = GetComponent<KnightController>();
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _animator.SetFloat("velocity", Mathf.Abs(_body.velocity.x) + Mathf.Abs(_body.velocity.y));
        _animator.SetBool("is_attacking", knightController.is_attacing);
    }
}

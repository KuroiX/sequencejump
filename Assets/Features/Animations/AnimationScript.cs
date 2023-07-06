using System;
using System.Collections;
using System.Collections.Generic;
using Features.Player.Controller;
using Features.Player.Controller.ControllerParts;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Animator _animator;
    private GroundedController _groundedController;

    private float prevX;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _groundedController = GetComponentInParent<CharControllerBehaviour>().Grounded;
        prevX = transform.parent.position.x;
    }


    private void Update()
    {
        double var1 = Math.Round(prevX, 3);
        double var2 = Math.Round(transform.parent.position.x, 3);
        
        
        Debug.Log("Prev: "+var1+", Current: "+var2);

        if (!_groundedController.IsGrounded)
        {
            _animator.SetBool("IsAirborn", true);
        }
        else
        {
            _animator.SetBool("IsAirborn", false);
        }
        
        

        if (Math.Abs(var1 - var2) > 0.01f)
        {
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }
        
        
        if (var1 > var2)
        {
            _sr.flipX = true;
        }
        
        if (var1 < var2)
        {
            _sr.flipX = false;
        }


        prevX = transform.parent.position.x;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpScript : MonoBehaviour
{
    private Animator _animator;
    public Transform playerTransform;
    public ParticleSystem trail;
    private float _prefX;

    public bool enableParticleEffect;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _prefX = playerTransform.position.x;
    }

    private void Update()
    {
        if (Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            _animator.SetTrigger("Jump");
        }

        float test = Math.Abs(playerTransform.position.x - _prefX);

        //Debug.Log("Value: "+test);
        

        if (enableParticleEffect)
        {
            if (test > 0.001)
            {
                Debug.Log("Playing");
                if(!trail.isPlaying)
                    trail.Play();
            }
            else
            {
                Debug.Log("STOPPPP");
                trail.Stop();
            }

            _prefX = playerTransform.transform.position.x;
        }
    }
}

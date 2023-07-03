using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Animator _animator;

    private float prevX;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        prevX = transform.parent.position.x;
    }


    private void Update()
    {
        double var1 = Math.Round(prevX, 3);
        double var2 = Math.Round(transform.parent.position.x, 3);
        
        
        Debug.Log("Prev: "+var1+", Current: "+var2);
        
        
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

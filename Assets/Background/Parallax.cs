using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _lenght;
    private float _startpos;
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        _startpos = transform.position.x;
        _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(_startpos + distance, transform.position.y, transform.position.z);

        if (temp > _startpos + _lenght)
            _startpos += _lenght;
        else if (temp < _startpos - _lenght)
            _startpos -= _lenght;
    }
}

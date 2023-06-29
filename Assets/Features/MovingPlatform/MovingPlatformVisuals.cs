using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformVisuals : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Transform[] _points;
    private Vector3[] _pointPositions;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        
        _points = _lineRenderer.GetComponentsInChildren<Transform>();

        _pointPositions = new Vector3[_points.Length - 1];

        for (int i = 0; i < _pointPositions.Length; i++)
        {
            _pointPositions[i] = _points[i + 1].position;
        }


        _lineRenderer.positionCount = _pointPositions.Length;
        _lineRenderer.SetPositions(_pointPositions);
    }
}

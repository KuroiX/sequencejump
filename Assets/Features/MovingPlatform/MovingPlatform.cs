using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    
    [SerializeField] private float platformSpeed;
    [SerializeField] private List<Transform> platformPoints;
    
    private List<bool> stoppablePoints;
    private Transform _currentTransform;
    private Transform _nextPoint;
    private int _direction;
    private int _currentIndex;

    private void Start()
    {
        _direction = 1;
        _currentIndex = 0;
        SetNextPosition();

        //TODO auf null checken?
        transform.position = platformPoints[0].position;
        stoppablePoints = new List<bool>();
        foreach (var point in platformPoints)
        {
            stoppablePoints.Add(point.GetComponent<StoppablePoint>().stoppable);
        }
    }

    // Sets the next position the platform should move to
    private void SetNextPosition()
    {
        if(platformPoints.Count == 1)
            return;
        
        if (platformPoints.Count <= _currentIndex + 1 * _direction)
        {
            _direction = -1;
        }

        else if (_currentIndex + 1 * _direction < 0)
        {
            _direction = 1;
        }

        _currentIndex += 1 * _direction;
        _nextPoint = platformPoints[_currentIndex];
    }
    

    // Resets Platfrom to first point in list
    [ContextMenu("Reset Platform")]
    public void ResetPlatform()
    {
        transform.position = platformPoints[0].position;
        _currentIndex = 0;
        _direction = 1;
        SetNextPosition();
    }

    // Moves Platform to next point in list
    [ContextMenu("Move to next point")]
    public void MoveToNextPoint()
    {
        StartCoroutine(MoveToNextPointCoroutine());
    }

    private IEnumerator MoveToNextPointCoroutine()
    {
        if(platformPoints.Count == 1)
            yield return null;
        
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, _nextPoint.position, platformSpeed);

            if (transform.position == _nextPoint.position)
            {
                bool stop = stoppablePoints[_currentIndex];
                SetNextPosition();
                if(stop)
                    break;
            }

            yield return null;
        }
    }
}

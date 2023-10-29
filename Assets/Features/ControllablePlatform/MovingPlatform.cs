using System.Collections;
using System.Collections.Generic;
using Features.StationLogic;
using UnityEngine;

namespace Features.ControllablePlatform
{
    public class MovingPlatform : StationEnteredReceiverBehaviour
    {
    
        [SerializeField] private float platformSpeed;
        [SerializeField] private List<Transform> platformPoints;
        [SerializeField] private bool loop;
        [SerializeField] private LineRenderer lineRenderer;
    
        private List<bool> stoppablePoints;
        private Transform _currentTransform;
        private Transform _nextPoint;
        private int _direction;
        private int _currentIndex;

        private Coroutine _coroutine;

        private void Start()
        {
            _direction = 1;
            _currentIndex = 0;
            SetNextPosition();

            if (loop)
            {
                lineRenderer.loop = true;
            }
            else
            {
                lineRenderer.loop = false;
            }
        
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
            if (loop)
            {
                _currentIndex = (_currentIndex + 1) % platformPoints.Count;
                _nextPoint = platformPoints[_currentIndex];
                return;
            }

            if(platformPoints.Count == 1)
                return;
        
            if (platformPoints.Count <= _currentIndex + _direction)
            {
                _direction = -1;
            }

            else if (_currentIndex + _direction < 0)
            {
                _direction = 1;
            }

            _currentIndex += _direction;
            _nextPoint = platformPoints[_currentIndex];
        }
    

        // Resets Platfrom to first point in list
        [ContextMenu("Reset Platform")]
        public void ResetPlatform()
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            
            transform.position = platformPoints[0].position;
            _currentIndex = 0;
            _direction = 1;
            SetNextPosition();
        }

        // Moves Platform to next point in list
        [ContextMenu("Move to next point")]
        public void MoveToNextPoint()
        {
            if(_coroutine == null)
                _coroutine = StartCoroutine(MoveToNextPointCoroutine());
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
                    {
                        _coroutine = null;
                        break;
                    }
                }

                yield return null;
            }
        }

        public override void ReceiveStationEntered()
        {
            ResetPlatform();
        }
    }
}

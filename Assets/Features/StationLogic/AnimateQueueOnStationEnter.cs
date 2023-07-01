using System;
using System.Collections;
using UnityEngine;

namespace Features.StationLogic
{
    public class AnimateQueueOnStationEnter : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int StationClosed = Animator.StringToHash("stationClosed");
        private static readonly int StationOpened = Animator.StringToHash("stationOpened");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void OnEnable()
        {
            Station.StationOpened += MoveToOpenedPosition;
            Station.StationClosed += MoveToClosedPosition;
        }

        private void OnDisable()
        {
            Station.StationOpened -= MoveToOpenedPosition;
            Station.StationClosed -= MoveToClosedPosition;
        }
        
        private void MoveToOpenedPosition(object sender, EventArgs e)
        {
            Debug.Log("opened");
            _animator.enabled = true;
            _animator.SetTrigger(StationOpened);
        }

        private void MoveToClosedPosition(object sender, EventArgs e)
        {
            Debug.Log("closed");
            _animator.enabled = true;
            _animator.SetTrigger(StationClosed);
        }

        private IEnumerator SetDeactiveRoutine()
        {
            yield return new WaitForSeconds(1);
            _animator.enabled = false;
        }
    }
}
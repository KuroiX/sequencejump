using System;
using System.Collections;
using UnityEngine;

namespace SequenceJump.StationLogic
{
    public class MoveQueueOnStationEnter : MonoBehaviour
    {
        [SerializeField] private RectTransform stationOpenedTransform;
        [SerializeField] private RectTransform stationClosedTransform;
        [SerializeField] private float moveTime;

        private RectTransform _transform;
        
        private void Awake()
        {
            _transform = (RectTransform)transform;
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
            _transform.anchoredPosition = stationOpenedTransform.anchoredPosition;
            _transform.pivot = stationOpenedTransform.pivot;
            _transform.anchorMin = stationOpenedTransform.anchorMin;
            _transform.anchorMax = stationOpenedTransform.anchorMax;
            _transform.sizeDelta = stationOpenedTransform.sizeDelta;
            //StartCoroutine(MoveToRoutine(stationOpenedPosition.position));
        }

        private void MoveToClosedPosition(object sender, EventArgs e)
        {
            Debug.Log("closed");
            _transform.anchoredPosition = stationClosedTransform.anchoredPosition;
            _transform.pivot = stationClosedTransform.pivot;
            _transform.anchorMin = stationClosedTransform.anchorMin;
            _transform.anchorMax = stationClosedTransform.anchorMax;
            _transform.sizeDelta = stationClosedTransform.sizeDelta;
        }

        private IEnumerator MoveToRoutine(RectTransform newTransform)
        {
            float currentTime = 0f;

            _transform.pivot = newTransform.pivot;
            _transform.anchorMin = newTransform.anchorMin;
            _transform.anchorMax = newTransform.anchorMax;
            _transform.sizeDelta = newTransform.sizeDelta;
            
            while (true)
            {
                currentTime += Time.deltaTime;

                _transform.anchoredPosition *= (newTransform.anchoredPosition - _transform.anchoredPosition) *
                                               Mathf.Clamp(currentTime / moveTime, 0, 1);
                
                if (currentTime > moveTime) break;
                
                yield return null;
            }
            Debug.Log("arrived");
        }
    }
}
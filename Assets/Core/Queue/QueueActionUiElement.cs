using System;
using System.Collections;
using NSubstitute;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Queue
{
    public class QueueActionUiElement : MonoBehaviour
    {
        public Sprite Sprite
        {
            set
            {
                image.sprite = value;
                image.enabled = !ReferenceEquals(value, null);
            }
        }

        [SerializeField] private Image image;
        [SerializeField] private int index;

        private static Vector2[] _targetPositions;
        private static Vector2[] _targetSizes;
        private static float[] _targetAlpha;

        private RectTransform _transform;

        private void Awake()
        {
            _transform = (RectTransform) transform;
            
            _targetPositions ??= new Vector2[6];
            _targetSizes ??= new Vector2[6];
            _targetAlpha ??= new float[6];
            
            _targetPositions[index] = _transform.anchoredPosition;
            _targetSizes[index] = _transform.sizeDelta;
            _targetAlpha[index] = image.color.a;
        }

        public void AnimateDequeue(int from, int to)
        {
            StopAllCoroutines();
            StartCoroutine(
                AnimationRoutines.LinearInterpolateRoutine(_targetPositions[from], _targetPositions[to], 
                    0.2f,
                    result => { _transform.anchoredPosition = result; }));
            StartCoroutine(
                AnimationRoutines.LinearInterpolateRoutine(_targetSizes[from], _targetSizes[to], 
                    0.2f,
                    result => { _transform.sizeDelta = result; }));
            StartCoroutine(
                AnimationRoutines.LinearInterpolateRoutine(_targetAlpha[from], _targetAlpha[to], 
                    0.2f,
                    result => { image.color = new Color(1, 1, 1, result); }));
        }

        public void AnimateEnqueue(int at, Sprite sprite)
        {
            Sprite = sprite;
            StartCoroutine(AnimationRoutines.LinearInterpolateRoutine(Vector2.zero, _targetSizes[at], 
                0.1f,
                result => { _transform.sizeDelta = result;}));
        }

        public void AnimateClear(int at)
        {
            StopAllCoroutines();
            StartCoroutine(
                AnimationRoutines.LinearInterpolateRoutine(_targetSizes[at], Vector2.zero, 0.1f, 
                result => { _transform.sizeDelta = result;}, 
                onFinishedCallback: () => {_transform.sizeDelta = _targetSizes[at];
                    Sprite = null; }
                )
            );
        }
    }
}
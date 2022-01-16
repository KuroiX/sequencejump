using Core.Tools;
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

        public static QueueActionUiElement[] QueueActions { get; set; }

        private RectTransform _transform;
        private Vector2 _position;
        private Vector2 _size;
        private float _alpha;

        private void Start()
        {
            _transform = (RectTransform) transform;

            _position = _transform.anchoredPosition;
            _size = _transform.sizeDelta;
            _alpha = image.color.a;
        }

        public void AnimateDequeue(int from, int to)
        {
            StopAllCoroutines();
            StartCoroutine(
                TweenRoutines.Linear(QueueActions[from]._position, QueueActions[to]._position, 
                    0.2f,
                    result => { _transform.anchoredPosition = result; }));
            StartCoroutine(
                TweenRoutines.Linear(QueueActions[from]._size, QueueActions[to]._size, 
                    0.2f,
                    result => { _transform.sizeDelta = result; }));
            StartCoroutine(
                TweenRoutines.Linear(QueueActions[from]._alpha, QueueActions[to]._alpha, 
                    0.2f,
                    result => { image.color = new Color(1, 1, 1, result); }));
        }

        public void AnimateEnqueue(int at, Sprite sprite)
        {
            Sprite = sprite;
            StartCoroutine(TweenRoutines.Linear(Vector2.zero, QueueActions[at]._size, 
                0.1f,
                result => { _transform.sizeDelta = result;}));
        }

        public void AnimateClear(int at)
        {
            StopAllCoroutines();
            StartCoroutine(
                TweenRoutines.Linear(QueueActions[at]._size, Vector2.zero, 0.1f, 
                result => { _transform.sizeDelta = result;}, 
                onFinishedCallback: () => { _transform.sizeDelta = QueueActions[at]._size;
                    Sprite = null; }
                )
            );
        }
    }
}
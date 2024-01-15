using UnityEngine;

namespace Features.Player
{
    public class SpriteFollowBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject[] spriteObjects;
        
        [SerializeField] private float time;

        private const int Framerate = 60;
        
        private Vector3[] _positions;
        private int _length;
        private int _index;

        private void Awake()
        {
            _length = (int)(Framerate * time);
            _positions = new Vector3[_length];
        }

        private void Update()
        {
            _positions[_index] = transform.position;
            _index = Index(_index + 1);

            int distance = _length / spriteObjects.Length;
            
            for (int i = 0; i < spriteObjects.Length; i++)
            {
                spriteObjects[i].transform.position = _positions[Index(_index + (i + 1) * distance)];
            }
        }

        private int Index(int i)
        {
            return i % _length;
        }
    }
}
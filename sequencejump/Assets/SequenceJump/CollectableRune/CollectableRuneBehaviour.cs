using SequenceJump.Abilities;
using SequenceJump.Queue;
using SequenceJump.StationLogic;
using UnityEngine;

namespace SequenceJump.CollectableRune
{
    public class CollectableRuneBehaviour : StationEnteredReceiverBehaviour
    {
        [SerializeField] private ActionType actionType;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider2D boxCollider2D;
        
        private ResettableQueue<ICharacterAction> _queue;

        private ICharacterAction _characterAction;
        
        private void Start()
        {
            _queue = FindObjectOfType<QueueHolder>().Queue;

            if (actionType == ActionType.None) return;
            
            _characterAction = CharacterAction.CharacterActions[actionType];
                
            spriteRenderer.sprite = _characterAction.Sprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _queue.Enqueue(_characterAction);
            SetActive(false);
        }

        private void SetActive(bool isActive)
        {
            spriteRenderer.enabled = isActive;
            boxCollider2D.enabled = isActive;
        }

        public override void ReceiveStationEntered()
        {
            SetActive(true);
        }
    }
}

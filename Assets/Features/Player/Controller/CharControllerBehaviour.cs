using Features.Player.Controller.ControllerParts;
using UnityEngine;

namespace Features.Player.Controller
{
    public class CharControllerBehaviour : MonoBehaviour
    {
        private ICharacterInput _characterInput;
        private Rigidbody2D _rb;
        private BoxCollider2D _collider;

        [SerializeField] private MovementSettings movementSettings;
        
        [Header("Jump Settings")]
        [SerializeField] private float jumpHeight;
        [SerializeField] private bool shortHoppable;
        
        [Header("Dash Settings")]
        [SerializeField] private float dashDistance;
        [SerializeField] private int iterations;

        [Header("Other Settings")]
        [SerializeField] private float maxFallSpeed;
        [SerializeField] private LayerMask environmentLayerMask;
        [SerializeField] private bool useStandardInput;

        private float _direction;

        private JumpController _jump;
        private DashController _dash;
        private GroundedController _grounded;
        private MovementController _movement;

        private CharController _charController;

        #region MonoBehaviour
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            
            int i = useStandardInput ? 0 : 1;
#if UNITY_ANDROID && !UNITY_EDITOR
            i = 2;
#endif
            _characterInput = GetComponents<ICharacterInput>()[2];

            _jump = new JumpController(_rb, jumpHeight);
            _dash = new DashController(_rb, iterations, dashDistance);
            _grounded = new GroundedController(_collider, environmentLayerMask, .1f);
            _movement = new MovementController(_characterInput, _grounded, _rb, movementSettings, maxFallSpeed);

            _charController = new CharController(_grounded, _jump, _dash, _movement, _characterInput);

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            _charController.HandleFixedUpdate();
        }
        
        private void Update()
        {
            _charController.HandleUpdate();
            FlipDirection();
        }

        #endregion MonoBehaviour
        
        #region Move to another script

        private SpriteRenderer _spriteRenderer;

        private void FlipDirection()
        {
            if (_charController.Direction < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (_charController.Direction > 0)
            {
                _spriteRenderer.flipX = false;
            }
        }

        #endregion
    }
}

using Features.Player.Controller.CharacterInput;
using Features.Player.Controller.ControllerParts;
using UnityEngine;

namespace Features.Player.Controller
{
    public class CharControllerBehaviour : MonoBehaviour
    {
        private IControllerInput _controllerInput;
        private Rigidbody2D _rb;
        private BoxCollider2D _collider;

        [SerializeField] private MovementSettings movementSettings;
        
        [Header("Jump Settings")]
        [SerializeField] private float jumpHeight;
        [SerializeField] private bool shortHoppable;
        
        [Header("Dash Settings")]
        [SerializeField] private float dashDistance;
        [Range(0, 1)]
        [SerializeField] private float breakPoint;
        [SerializeField] private int iterations;

        [Header("Other Settings")]
        [SerializeField] private float maxFallSpeed;
        [SerializeField] private LayerMask environmentLayerMask;
        [SerializeField] private bool useStandardInput;

        private JumpController _jump;
        private JumpController _airJump;
        private DynamicDashController _dash;
        private GroundedController _grounded;
        private MovementController _movement;

        private CharController _charController;

        #region MonoBehaviour
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            
            int i = useStandardInput ? 0 : 1;

            _controllerInput = GetComponents<IInputHolder>()[i].Input;

            _jump = new JumpController(_rb, jumpHeight);
            _airJump = new JumpController(_rb, jumpHeight - 1);
            _dash = new DynamicDashController(_rb, 
                new Ref<int>(() => iterations, value => iterations=value), 
                new Ref<float>(() => dashDistance, value => dashDistance = value), 
                new Ref<float>(() => dashDistance * breakPoint, value => {}));
            _grounded = new GroundedController(_collider, environmentLayerMask, .1f);
            _movement = new MovementController(_controllerInput, _grounded, _rb, movementSettings, 
                new Ref<float>(() => maxFallSpeed, value => maxFallSpeed = value));

            _charController = new CharController(_grounded, _jump, _airJump, _dash, _movement, _controllerInput);

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

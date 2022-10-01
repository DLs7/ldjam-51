using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D;
        public Camera Camera;
    
        // Base movement variables
        public float MovementSpeed = 5f;
        private Vector2 MovementDirection;
        private Vector2 AimDirection;

        // Dash variables
        private float _activeMovementSpeed;
        public float DashSpeed = 10f;
        public float DashLength = 0.5f, DashCooldown = 1f;
        private float _dashCounter;
        private float _dashCoolCounter;

        // Go back in time variables
        public TrailRenderer TrailRenderer;
        private bool _goBackInTimePositionSet;
        [HideInInspector] public bool GoingBackInTime;
        private Vector2 _goBackInTimePosition;
        private float _goBackInTimeTimer = 10f;

        public float LerpTime = 0.75f;
        private float _currentLerpTime = 0f;
        private void Start()
        {
            _activeMovementSpeed = MovementSpeed;
        }

        void Update()
        {
            if(!GoingBackInTime) GetInputs();
        }

        void FixedUpdate()
        {
            if (GoingBackInTime)
            {
                GoBackInTime();
            }
            else
            {
                SetBackInTime();
                Move();
            }
        }

        void SetBackInTime()
        {
            _goBackInTimeTimer -= Time.fixedDeltaTime;
        
            if (_goBackInTimeTimer <= 5 && !_goBackInTimePositionSet )
            {
                _goBackInTimePosition = Rigidbody2D.position;
                _goBackInTimePositionSet = true;
                TrailRenderer.enabled = true;
            }

            if (_goBackInTimeTimer <= 0)
            {
                GoingBackInTime = true;
            }
        }

        void GoBackInTime()
        {
            _currentLerpTime += Time.deltaTime;
            if (_currentLerpTime > LerpTime)
            {
                _currentLerpTime = LerpTime;
            }
        
            float xAxis = _currentLerpTime / LerpTime;
            Vector2 position = Vector2.Lerp(Rigidbody2D.position, _goBackInTimePosition, xAxis);
            Rigidbody2D.MovePosition(position);

            if (_goBackInTimePosition == Rigidbody2D.position)
            {
                _goBackInTimeTimer = 10f;
                _goBackInTimePositionSet = false;
                GoingBackInTime = false;
                _currentLerpTime = 0f;

                TrailRenderer.Clear();
                TrailRenderer.enabled = false;
            }
        
        }

        void GetInputs()
        {
            MovementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            AimDirection = Camera.ScreenToWorldPoint(Input.mousePosition);
        
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_dashCounter <= 0 && _dashCoolCounter <= 0)
                {
                    _activeMovementSpeed = DashSpeed;
                    _dashCounter = DashLength;
                }
            }
        }

        void Move()
        {
            Rigidbody2D.velocity = MovementDirection * _activeMovementSpeed;

            Vector2 firepointDirection = AimDirection - Rigidbody2D.position;
            float angle = Mathf.Atan2(firepointDirection.y, firepointDirection.x) * Mathf.Rad2Deg - 90f;
            Rigidbody2D.rotation = angle;
        
            if (_dashCounter > 0)
            {
                _dashCounter -= Time.fixedDeltaTime;
                if (_dashCounter <= 0)
                {
                    _activeMovementSpeed = MovementSpeed;
                    _dashCoolCounter = DashCooldown;
                }
            }

            if (_dashCoolCounter > 0)
            {
                _dashCoolCounter -= Time.fixedDeltaTime;
            }
        }
    }
}

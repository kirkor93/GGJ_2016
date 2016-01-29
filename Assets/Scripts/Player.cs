using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Player : MonoBehaviour
    {
        [Range(0.0f, 100.0f)]
        public float Acceleration;
        [Range(0.0f, 1000.0f)]
        public float MaxMovementSpeed;

        public Collider2D GroundTestCollider;

        private Rigidbody2D _rigidbody;
        private float _currentMovementSpeed;
        private bool _flying;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public virtual void OnMove(Vector2 direction)
        {
            float targetMovementSpeed = _currentMovementSpeed + Acceleration * direction.x;
            _currentMovementSpeed = Mathf.Lerp(_currentMovementSpeed, targetMovementSpeed, 0.5f);

            _rigidbody.velocity = Vector2.right * _currentMovementSpeed * Time.deltaTime;
        }

        public virtual void OnJumpStart()
        {
            if (_flying)
            {
                return;
            }
        }

        public virtual void OnJumpRelease()
        {
            
        }
        public abstract void OnActionStart();
        public abstract void OnActionRelease();
    }
}

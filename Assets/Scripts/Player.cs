using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Player : MonoBehaviour
    {
        [Range(0.0f, 100.0f)]
        public float Acceleration;
        [Range(0.0f, 1000.0f)]
        public float MaxMovementSpeed;
        [Range(0.0f, 1.0f)]
        public float Friction;
        [Range(0.0f, 1000.0f)]
        public float JumpSpeed;

        public Collider2D GroundTestCollider;

        private Rigidbody2D _rigidbody;
        private float _currentMovementSpeed;
        private bool _flying;

        private Coroutine _jumpCoroutine;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        protected virtual void FixedUpdate()
        {
            _flying = !GroundTestCollider.IsTouchingLayers(LayerMask.NameToLayer("Level"));
        }
        public virtual void OnMove(Vector2 direction)
        {
            if (direction.magnitude > 0.0f)
            {
                float targetMovementSpeed = _currentMovementSpeed + Acceleration * direction.x;
                _currentMovementSpeed = Mathf.Lerp(_currentMovementSpeed, targetMovementSpeed, 0.5f);
            }
            else
            {
                _currentMovementSpeed = (1.0f - Friction) * _currentMovementSpeed;
            }
            _rigidbody.velocity = Vector2.right * _currentMovementSpeed * Time.deltaTime;
        }

        public virtual void OnJumpStart()
        {
            if (_flying)
            {
                return;
            }

            _jumpCoroutine = StartCoroutine(Jump());
        }

        private IEnumerator Jump()
        {
            while (_rigidbody.velocity.y < JumpSpeed)
            {
                _rigidbody.velocity += Vector2.up*JumpSpeed;

                yield return null;
            }
        }

        public virtual void OnJumpRelease()
        {
            if (_jumpCoroutine != null)
            {
                StopCoroutine(_jumpCoroutine);
            }
        }
        public abstract void OnActionStart();
        public abstract void OnActionRelease();
    }
}

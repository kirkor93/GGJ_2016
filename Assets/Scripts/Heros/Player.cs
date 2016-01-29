using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Player : MonoBehaviour
    {
        [Range(0.0f, 10000.0f)]
        public float Acceleration;
        [Range(0.0f, 10000.0f)]
        public float MaxMovementSpeed;
        [Range(0.0f, 1.0f)]
        public float Friction;
        [Range(0.0f, 10000.0f)]
        public float JumpSpeed;

        public Collider2D GroundTestCollider;

        protected Rigidbody2D Rigidbody;

        private float _currentMovementSpeed;
        private bool _flying;

        private Coroutine _jumpCoroutine;

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        protected virtual void FixedUpdate()
        {
            _flying = !GroundTestCollider.IsTouchingLayers(LayerMask.NameToLayer("Level"));
            Debug.Log(gameObject.name + _flying);
        }
        public virtual void OnMove(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) > 0.0f)
            {
                float targetMovementSpeed = _currentMovementSpeed + Acceleration * direction.x;
                _currentMovementSpeed = Mathf.Lerp(_currentMovementSpeed, targetMovementSpeed, 0.5f);
            }
            else
            {
                _currentMovementSpeed = (1.0f - Friction) * _currentMovementSpeed;
            }
            Rigidbody.velocity = Vector2.right * _currentMovementSpeed * Time.deltaTime;
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
            while (Rigidbody.velocity.y < JumpSpeed)
            {
                Rigidbody.velocity += Vector2.up*JumpSpeed;

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

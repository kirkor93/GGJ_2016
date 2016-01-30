using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Player : MonoBehaviour
    {
        [Range(0.0f, 10000.0f)]
        public float Acceleration;
        [Range(0.0f, 100000.0f)]
        public float MaxMovementSpeed;
        [Range(0.0f, 1.0f)]
        public float Friction;

        [Range(0.0f, 10000.0f)]
        public float JumpSpeed;
        [Range(0.0f, 10.0f)]
        public float JumpTime;

        public Collider2D GroundTestCollider;

        protected Rigidbody2D Rigidbody;

        private float _currentMovementSpeed;
        private bool _flying;

        private Coroutine _jumpCoroutine;
        private int _currentScore;

        public int CurrentScore
        {
            get { return _currentScore; }
        }

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Player[] g = FindObjectsOfType<Player>();
            foreach (Player player in g)
            {
                foreach (Collider2D componentsInChild in player.gameObject.GetComponentsInChildren<Collider2D>())
                {
                    Physics2D.IgnoreCollision(componentsInChild, GroundTestCollider);
                }
            }
        }

        protected virtual void FixedUpdate()
        {
            _flying = !GroundTestCollider.IsTouchingLayers(int.MaxValue);
        }
        public virtual void OnMove(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) > 0.0f)
            {
                float targetMovementSpeed = _currentMovementSpeed + Acceleration * direction.x;
                if (Math.Abs(Mathf.Sign(targetMovementSpeed) - Mathf.Sign(_currentMovementSpeed)) > 0.1f)
                {
                    _currentMovementSpeed = (1.0f - Friction) * _currentMovementSpeed;
                }
                _currentMovementSpeed = Mathf.Clamp(Mathf.Lerp(_currentMovementSpeed, targetMovementSpeed, 0.5f), -MaxMovementSpeed, MaxMovementSpeed);
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
            float time = 0.0f;
            while (time < JumpTime)
            {
                Rigidbody.velocity += Vector2.up*JumpSpeed*(1.0f - time / JumpTime);
                time += Time.deltaTime;
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

        public void ChangeScore(int value)
        {
            _currentScore += value;
            Debug.Log(_currentScore);
        }

        public abstract void OnActionStart();
        public abstract void OnActionRelease();
    }
}

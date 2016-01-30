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

        [Range(0.0f, 10000.0f)]
        public float GravityForce;

        public bool SequenceMode;

        protected Rigidbody2D Rigidbody;

        private float _currentMovementSpeed;
        private bool _flying;

        private int _currentScore;

        public int CurrentScore
        {
            get { return _currentScore; }
        }

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        protected virtual void FixedUpdate()
        {
            Vector2 vel = Rigidbody.velocity;
            vel.y -= GravityForce * Time.deltaTime;
            Rigidbody.velocity = vel;

        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            _flying = false;
        }

        protected virtual void OnCollisionExit2D(Collision2D other)
        {
            _flying = true;
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
            Vector2 vel = Rigidbody.velocity;
            vel.x = _currentMovementSpeed * Time.deltaTime;
            Rigidbody.velocity = vel;
        }

        public virtual void OnJumpStart()
        {
            if (SequenceMode || _flying)
            { 
                return;
            }
            Rigidbody.velocity = Rigidbody.velocity + Vector2.up*JumpSpeed;
        }

        public virtual void OnJumpRelease()
        {
            if (Rigidbody.velocity.y > 0.0f)
            {
                Vector2 vel = Rigidbody.velocity;
                vel.y = 0.0f;
                Rigidbody.velocity = vel;
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

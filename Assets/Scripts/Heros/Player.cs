using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameOverEventArgs : EventArgs
    {
        public bool IsWon { get; private set; }

        public GameOverEventArgs(bool isWon)
        {
            IsWon = isWon;
        }
    }

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

        [Range(0.0f, 20.0f)]
        public int MaxHitPoints;

        public bool SequenceMode;

        public bool Water;

        public bool Attack;

        protected Rigidbody2D Rigidbody;

        private float _currentMovementSpeed;
        private bool _flying;

        private int _currentScore;
        private int _hitPoints;

        public event EventHandler<GameOverEventArgs> OnGameOver; 

        public bool Flying
		{
			get { return _flying; }
		}

        public int CurrentScore
        {
            get { return _currentScore; }
        }

        public int HitPoints
        {
            get { return _hitPoints; }
            set
            {
                _hitPoints = value;
                if (_hitPoints <= 0)
                {
                    EndGame(false);
                }
            }
        }

        public void EndGame(bool isWon)
        {
//            Debug.Log("GAME OVER!!!");
            if (OnGameOver != null)
            {
                OnGameOver(this, new GameOverEventArgs(isWon));
            }
        }

        protected virtual void Awake()
        {
            HitPoints = MaxHitPoints;
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

        protected virtual void OnCollisionStay2D(Collision2D other)
        {
            _flying = false;
        }

        protected virtual void OnCollisionExit2D(Collision2D other)
        {
            _flying = true;
        }

        public virtual void OnMove(Vector2 direction)
        {
//            _currentMovementSpeed = Rigidbody.velocity.x;
            float targetMovementSpeed;
            if (Mathf.Abs(direction.x) > 0.0f)
            {
                targetMovementSpeed = _currentMovementSpeed + Acceleration * direction.x;
                if (Mathf.Abs(targetMovementSpeed / _currentMovementSpeed) < 1.0f)
                {
                    targetMovementSpeed *= (1.0f - Friction);
                }
            }
            else
            {
                targetMovementSpeed = (1.0f - Friction) * _currentMovementSpeed;
            }
            _currentMovementSpeed = Mathf.Clamp(Mathf.Lerp(_currentMovementSpeed, targetMovementSpeed, 0.5f),
                -MaxMovementSpeed, MaxMovementSpeed);
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
//            Debug.Log(_currentScore);
        }

        public abstract void OnActionStart();
        public abstract void OnActionRelease();
    }
}

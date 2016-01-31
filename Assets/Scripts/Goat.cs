using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Goat : Player
    {
        public float HitTime;

        public Collider2D[] HitColliders;
        public Transform ChickenPosition;
        public float ThrowForce;

        private Coroutine _actionCoroutine;
        private Chicken _caughtChicken;
        private Vector2 _lastInputDirection;
        private Rigidbody2D _caughtChickenRigidbody;
        private Animator _animator;

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        protected void Start()
        {
            SequenceMode = false;
            Water = false;
        }

        public override void OnActionStart()
        {
            if (!SequenceMode)
            {
                if (_caughtChicken != null)
                {
                    //                    StartCoroutine(Throw());
//                    InputManager.InputEnabled = false;
//                    _caughtChickenRigidbody.Velo;
                    _caughtChickenRigidbody.velocity = _lastInputDirection*ThrowForce;

//                    _caughtChicken.Invoke("UnblockMovement", 1.3f);
                    _caughtChicken = null;
                    _caughtChickenRigidbody = null;
					_animator.SetBool("kick", true);
					//                    Debug.Break();
				}
				else if (_actionCoroutine == null)
                {
                    _actionCoroutine = StartCoroutine(HitCoroutine());
					_animator.SetBool("attack", true);

                    Attack = true;
				}
			}
        }

        private IEnumerator Throw()
        {
            foreach (Collider2D c in _caughtChicken.GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            
        }

        private IEnumerator HitCoroutine()
        {
            foreach (Collider2D hitCollider in HitColliders)
            {
                hitCollider.enabled = true;
            }
            yield return new WaitForSeconds(HitTime);
            foreach (Collider2D hitCollider in HitColliders)
            {
                hitCollider.enabled = false;
            }
            _actionCoroutine = null;
        }

        public override void OnActionRelease()
        {
            if (!SequenceMode)
            {

            }
            _animator.SetBool("attack", false);
			_animator.SetBool("kick", false);

            Attack = false;
		}

		public override void OnMove(Vector2 direction)
        {
            base.OnMove(direction);
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
            _lastInputDirection = direction;

			if(direction.x < 0.1f && direction.x > -0.1f)
                _animator.SetBool("moving", false);
			else
                _animator.SetBool("moving", true);
		}

		protected void Update()
        {
            if (_caughtChickenRigidbody != null)
            {
                _caughtChickenRigidbody.velocity = Vector2.zero ;
                _caughtChickenRigidbody.position = ChickenPosition.position;
            }

            if (!Water)
            {
                if (Flying)
                    _animator.SetBool("jumping", true);
                else
                    _animator.SetBool("jumping", false);
            }
            else
                _animator.SetBool("jumping", true);

        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            Chicken c = other.GetComponent<Chicken>();
            if (c != null)
            {
                c.BlockMovement();
                _caughtChickenRigidbody = c.GetComponent<Rigidbody2D>();
                c.transform.position = ChickenPosition.position;
                _caughtChicken = c;
            }
        }
    }
}

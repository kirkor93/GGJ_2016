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

        void Start()
        {
            SequenceMode = false;
        }

        public override void OnActionStart()
        {
            if (!SequenceMode)
            {
                if (_caughtChicken != null)
                {
                    _caughtChickenRigidbody.AddForce(_lastInputDirection * ThrowForce);
                    _caughtChicken.UnblockMovement();
                    _caughtChicken.transform.parent = null;
                    _caughtChicken = null;
                    _caughtChickenRigidbody = null;
                }
                else if (_actionCoroutine == null)
                {
                    _actionCoroutine = StartCoroutine(HitCoroutine());
                }
            }
			GetComponent<Animator>().SetBool("attack", true);
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
			GetComponent<Animator>().SetBool("attack", false);
		}

		public override void OnMove(Vector2 direction)
        {
            base.OnMove(direction);
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
            _lastInputDirection = direction;

			if(direction.x < 0.1f && direction.x > -0.1f)
				GetComponent<Animator>().SetBool("moving", false);
			else
				GetComponent<Animator>().SetBool("moving", true);
		}

		protected void Update()
        {
            if (_caughtChickenRigidbody != null)
            {
                _caughtChickenRigidbody.velocity = Vector2.up * _caughtChickenRigidbody.velocity.y ;
                _caughtChickenRigidbody.position = ChickenPosition.position;
            }

			if (Flying)
				GetComponent<Animator>().SetBool("jumping", true);
			else
				GetComponent<Animator>().SetBool("jumping", false);
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

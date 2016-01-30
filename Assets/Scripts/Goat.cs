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

        public override void OnActionStart()
        {
            if (_caughtChicken != null)
            {
                _caughtChickenRigidbody.AddForce(_lastInputDirection*ThrowForce);
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

        }

        public override void OnMove(Vector2 direction)
        {
            base.OnMove(direction);
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
            _lastInputDirection = direction;
        }

        protected void Update()
        {
            if (_caughtChickenRigidbody != null)
            {
                _caughtChickenRigidbody.velocity = Vector2.zero;
                _caughtChickenRigidbody.position = ChickenPosition.position;
            }
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            Chicken c = other.GetComponent<Chicken>();
            if (c != null)
            {
                c.BlockMovement();
                _caughtChickenRigidbody = c.GetComponent<Rigidbody2D>();
                c.transform.parent = transform;
                c.transform.position = ChickenPosition.position;
                _caughtChicken = c;
            }

        }
    }
}

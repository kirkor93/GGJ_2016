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

        private Coroutine _actionCoroutine;
        private Chicken _caughtChicken;
        private Vector2 _lastInputDirection;

        public override void OnActionStart()
        {
            if (_caughtChicken != null)
            {
                //throw chicken
                _caughtChicken.transform.parent = null;
                _caughtChicken = null;
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
            scale.x = Mathf.Sign(direction.x)*Mathf.Abs(scale.x);
            transform.localScale = scale;
            _lastInputDirection = direction;
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("hero"))
            {
                Chicken c = other.GetComponent<Chicken>();
                if (c != null)
                {
                    c.transform.parent = transform;
                    c.transform.position = ChickenPosition.position;
                    _caughtChicken = c;
                }
            }
        }
    }
}

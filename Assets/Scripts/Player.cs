using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Player : MonoBehaviour
    {
        public abstract void OnMove(Vector2 direction);
        public abstract void OnJumpStart();
        public abstract void OnJumpRelease();
        public abstract void OnActionStart();
        public abstract void OnActionRelease();
    }
}

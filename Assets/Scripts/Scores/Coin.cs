using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Coin : MonoBehaviour
{
    public int ScoreIncrease = 1;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.ChangeScore(ScoreIncrease);
            }
            Destroy(gameObject);
        }
    }
}

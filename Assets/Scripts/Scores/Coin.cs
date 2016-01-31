using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Coin : MonoBehaviour
{
    public int ScoreIncrease = 1;
    private Collider2D _collider;
    private SpriteRenderer _renderer;
    private ParticleSystem _particle;

    protected void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _particle = GetComponentInChildren<ParticleSystem>(true);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.ChangeScore(ScoreIncrease);
            }
			GetComponent<AudioSource>().Play();
            _renderer.enabled = false;
            _collider.enabled = false;
            _particle.gameObject.SetActive(true);
            StartCoroutine(DestroyOnParticleFinish());
        }
    }

    private IEnumerator DestroyOnParticleFinish()
    {
        yield return new WaitForSeconds(_particle.duration);
        Destroy(gameObject);
    }
}

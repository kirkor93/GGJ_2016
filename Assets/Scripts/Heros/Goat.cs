using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Goat : Player
{
    public float HitTime;

    public Collider2D[] HitColliders;
    public Transform ChickenPosition;
    public float ThrowForce;
    public float ThrowAdjustmentFrequency = 1.0f;
    public AudioClip walkClip;
    public AudioClip jumpClip1;
    public AudioClip jumpClip2;
    public AudioClip jumpClip3;
    public AudioClip jumpClip4;

    private Coroutine _actionCoroutine;
    private Chicken _caughtChicken;
    private Vector2 _lastInputDirection;
    private Rigidbody2D _caughtChickenRigidbody;
    private Animator _animator;
    private AudioSource _audioSource;
    private float _currentThrowForce;
    private Coroutine _throwAdjustCoroutine;
    private bool jumping;

    public float CurrentThrowForce
    {
        get { return _currentThrowForce; }
    }

    public bool HoldingChicken
    {
        get { return _caughtChicken != null; }
    }

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected void Start()
    {
        SequenceMode = false;
        Water = false;
        StartCoroutine(AdjustThrowSpeed());
    }

    public override void OnActionStart()
    {
        if (!SequenceMode)
        {
            if (_caughtChicken != null)
            {
                _caughtChickenRigidbody.velocity = _lastInputDirection * _currentThrowForce;
                _currentThrowForce = 0.0f;
                _caughtChicken = null;
                _caughtChickenRigidbody = null;
                _animator.SetBool("kick", true);
            }
            else if (_actionCoroutine == null)
            {
                _actionCoroutine = StartCoroutine(HitCoroutine());
                _animator.SetBool("attack", true);

                Attack = true;
            }
        }
    }

    private IEnumerator AdjustThrowSpeed()
    {
        float time = 0.0f;
        while (true)
        {
            float lerpFactor = Mathf.Sin(time * ThrowAdjustmentFrequency * 2.0f * Mathf.PI) / 2.0f + 0.5f;
            _currentThrowForce = Mathf.Lerp(0.001f, ThrowForce, lerpFactor);
            time += Time.deltaTime;
            //                Debug.Log(_currentThrowForce);
            yield return null;
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
        if (!SequenceMode)
        {

        }
        _animator.SetBool("attack", false);
        _animator.SetBool("kick", false);

        Attack = false;
    }

    public override void OnJumpStart()
    {
        base.OnJumpStart();
        if (_audioSource.clip == walkClip)
        {
            _audioSource.loop = false;
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0: _audioSource.clip = jumpClip1; break;
                case 1: _audioSource.clip = jumpClip2; break;
                case 2: _audioSource.clip = jumpClip3; break;
                case 3: _audioSource.clip = jumpClip4; break;
            }
        }
        if (!_audioSource.isPlaying)
            _audioSource.Play();

    }

    public override void OnMove(Vector2 direction)
    {
        base.OnMove(direction);
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
        transform.localScale = scale;
        _lastInputDirection = direction;

        if (direction.x < 0.1f && direction.x > -0.1f)
        {
            _animator.SetBool("moving", false);
            if (_audioSource.clip == walkClip)
                _audioSource.Stop();
        }
        else if (!jumping)
        {
            _animator.SetBool("moving", true);

            if (_audioSource.clip != walkClip && !_audioSource.isPlaying)
                _audioSource.clip = walkClip;

            if (!_audioSource.loop && !_audioSource.isPlaying)
                _audioSource.loop = true;

            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else if (jumping)
        {
            _animator.SetBool("moving", false);
            if (!_audioSource.clip == walkClip)
                _audioSource.Stop();
        }
    }

    protected void Update()
    {
        if (_caughtChickenRigidbody != null)
        {
            _caughtChickenRigidbody.velocity = Vector2.zero;
            _caughtChickenRigidbody.position = ChickenPosition.position;
        }

        if (!Water)
        {
            if (Flying)
            {
                jumping = true;
                _animator.SetBool("jumping", jumping);
            }
            else
            {
                jumping = false;
                _animator.SetBool("jumping", jumping);
            }
        }
        else
        {
            jumping = true;
            _animator.SetBool("jumping", jumping);
        }

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


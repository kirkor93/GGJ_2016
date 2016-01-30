using UnityEngine;
using System.Collections;
using DG.Tweening;
using Assets.Scripts;

public class Enemy : MonoBehaviour
{

    public float MovementTime = 2.0f;

    float leftX, rightX, timeHero1, timeHero2;
    SpriteRenderer sprRen;
    Collider2D col;
    int life;
    bool catchHero1, catchHero2;

	// Use this for initialization
	void Start () {

        life = 3;
        catchHero1 = false;
        catchHero2 = false;
        timeHero1 = -10;
        timeHero2 = -10;
        sprRen = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        leftX = transform.position.x;
        rightX = transform.FindChild("endPoint").transform.position.x;

        Vector3[] checkPoints = new Vector3[]
        {
            new Vector3(leftX, transform.position.y, transform.position.z),
            new Vector3(rightX, transform.position.y, transform.position.z)
        };

        transform.DOLocalPath(checkPoints, MovementTime, PathType.Linear, PathMode.Ignore).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > rightX - sprRen.bounds.size.x/12 && transform.localScale.x == 1.6f)
            transform.localScale = new Vector3(-1, 1, 1) * 1.6f;
        else if (transform.position.x < leftX + sprRen.bounds.size.x /12 && transform.localScale.x == -1.6f)
            transform.localScale = Vector3.one * 1.6f;

        if (catchHero1)
        {
            if (timeHero1 > 0)
                timeHero1 -= Time.deltaTime;
            else
                catchHero1 = false;
        }

        if (catchHero2)
        {
            if (timeHero2 > 0)
                timeHero2 -= Time.deltaTime;
            else
                catchHero2 = false;
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("egg"))
        {
            --life;

            if (life == 2)
            {
                sprRen.DOFade(0f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InExpo);
            }
            else if (life == 1)
            {
                sprRen.DOKill();
                sprRen.color = new Color(1, 1, 1, 1);
                sprRen.DOFade(0f, 0.25f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InExpo);
            }
            else
            {
                transform.DOScale(Vector3.zero, 0.15f);
                StartCoroutine("FuckYou");
            }
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            if (col.gameObject.name.Contains("chicken") && !catchHero1)
            {
                catchHero1 = true;
                timeHero1 = 3;

                --col.gameObject.GetComponent<Player>().HitPoints;

                col.gameObject.GetComponent<SpriteRenderer>().color = new Color (1,1,1,1);
                col.gameObject.GetComponent<SpriteRenderer>().DOFade(0,0.3f).SetEase(Ease.InExpo).SetLoops(6,LoopType.Yoyo);
            }
            else if (col.gameObject.name.Contains("Goat") && !catchHero2)
            {
                catchHero2 = true;
                timeHero2 = 3;

                --col.gameObject.GetComponent<Player>().HitPoints;

                col.gameObject.GetComponent<SpriteRenderer>().color = new Color (1,1,1,1);
                col.gameObject.GetComponent<SpriteRenderer>().DOFade(0,0.3f).SetEase(Ease.InExpo).SetLoops(6,LoopType.Yoyo);
            }
        }
    }


    IEnumerator FuckYou()
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);

    }
}

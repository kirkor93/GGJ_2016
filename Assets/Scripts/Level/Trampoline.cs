using UnityEngine;
using System.Collections;
using DG.Tweening;
using Assets.Scripts;

public class Trampoline : MonoBehaviour {

    public float jumpSpeed1, jumpSpeed2;
	public AudioClip trampolineClip1;
	public AudioClip trampolineClip2;
	public AudioClip trampolineClip3;
    Player player1, player2;
    bool p1, p2;
    Transform sprezyna, top;
    Vector3 orgPosTop;

	// Use this for initialization
	void Start () {
        sprezyna = transform.FindChild("sprezyna").transform;
        top = transform.FindChild("top").transform;
        orgPosTop = top.position;
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero") && top.position.y < col.transform.position.y )
        {
			int rand = Random.Range(0, 3);
			switch(rand)
			{
				case 0: GetComponent<AudioSource>().clip = trampolineClip1; break;
				case 1: GetComponent<AudioSource>().clip = trampolineClip2; break;
				case 2: GetComponent<AudioSource>().clip = trampolineClip3; break;
			}
			GetComponent<AudioSource>().Play();

			if (col.gameObject.name.Contains("chicken") && !p1)
            {
                StartCoroutine("ReturnSpeed");

                player1 = col.gameObject.GetComponent<Player>();
                p1 = true;

                col.gameObject.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity + Vector2.up * jumpSpeed1;

                sprezyna.DOScaleY(0.8f, 0.2f).SetLoops(2, LoopType.Yoyo);
                top.DOMoveY(top.position.y - top.GetComponent<SpriteRenderer>().bounds.size.y/2, 0.2f).SetLoops(2,LoopType.Yoyo);
            }

            if (col.gameObject.name.Contains("Goat") && !p2 && top.position.y < col.transform.position.y)
            {
                StartCoroutine("ReturnSpeed");

                player2 = col.gameObject.GetComponent<Player>();
                p2 = true;

                col.gameObject.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity + Vector2.up * jumpSpeed2;

                if (!p1)
                {
                    sprezyna.DOScaleY(0.7f, 0.2f).SetLoops(2, LoopType.Yoyo);
                    top.DOMoveY(top.position.y - top.GetComponent<SpriteRenderer>().bounds.size.y, 0.2f).SetLoops(2, LoopType.Yoyo);
                }
            }

        }
    }

    IEnumerator ReturnSpeed()
    {
        yield return new WaitForSeconds(0.5f);
        
        if (p1)
        {
            p1 = false;
        }

        if (p2)
        {
            p2 = false;
        }

        sprezyna.DOKill();
        top.DOKill();

        sprezyna.localScale = Vector3.one;
        top.position = orgPosTop;
    }
}

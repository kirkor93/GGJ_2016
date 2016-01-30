using UnityEngine;
using System.Collections;
using DG.Tweening;
using Assets.Scripts;

public class Trampoline : MonoBehaviour {

    public float jumpSpeed1, jumpSpeed2;
    float orgSpeed1, orgSpeed2;
    Player player1, player2;
    bool p1, p2;
    Transform sprezyna, top;

	// Use this for initialization
	void Start () {
        sprezyna = transform.FindChild("sprezyna").transform;
        top = transform.FindChild("top").transform;
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero") && top.position.y < col.transform.position.y )
        {
            if (col.gameObject.name.Contains("chicken") && !p1)
            {
                StartCoroutine("ReturnSpeed");

                orgSpeed1 = col.gameObject.GetComponent<Player>().JumpSpeed;
                player1 = col.gameObject.GetComponent<Player>();
                p1 = true;

                col.gameObject.GetComponent<Player>().JumpSpeed = jumpSpeed1;
                col.gameObject.GetComponent<Player>().OnJumpStart();

                sprezyna.DOScaleY(0.8f, 0.2f).SetLoops(2, LoopType.Yoyo);
                top.DOMoveY(top.position.y - top.GetComponent<SpriteRenderer>().bounds.size.y/2, 0.2f).SetLoops(2,LoopType.Yoyo);
            }

            if (col.gameObject.name.Contains("Goat") && !p2 && top.position.y < col.transform.position.y)
            {
                StartCoroutine("ReturnSpeed");

                orgSpeed2 = col.gameObject.GetComponent<Player>().JumpSpeed;
                player2 = col.gameObject.GetComponent<Player>();
                p2 = true;

                col.gameObject.GetComponent<Player>().JumpSpeed = jumpSpeed2;
                col.gameObject.GetComponent<Player>().OnJumpStart();

                if (!p1)
                {
                    sprezyna.DOScaleY(0.7f, 0.3f).SetLoops(2, LoopType.Yoyo);
                    top.DOMoveY(top.position.y - top.GetComponent<SpriteRenderer>().bounds.size.y, 0.3f).SetLoops(2, LoopType.Yoyo);
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
            player1.JumpSpeed = orgSpeed1;
        }

        if (p2)
        {
            p2 = false;
            player2.JumpSpeed = orgSpeed2;
        }

        sprezyna.DOKill();
        top.DOKill();
    }
}

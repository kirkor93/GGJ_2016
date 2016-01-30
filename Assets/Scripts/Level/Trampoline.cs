using UnityEngine;
using System.Collections;
using DG.Tweening;
using Assets.Scripts;

public class Trampoline : MonoBehaviour {

    public float jumpSpeed;
    float orgSpeed1, orgSpeed2;
    Player player1, player2;
    bool p1, p2;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
       
	}

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero") && col.transform.position.y > transform.position.y )
        {
           

            if (col.gameObject.name.Contains("chicken") && !p1)
            {
                StartCoroutine("ReturnSpeed");

                orgSpeed1 = col.gameObject.GetComponent<Player>().JumpSpeed;
                player1 = col.gameObject.GetComponent<Player>();
                p1 = true;

                col.gameObject.GetComponent<Player>().JumpSpeed = jumpSpeed;
                col.gameObject.GetComponent<Player>().OnJumpStart();
            }

            if (col.gameObject.name.Contains("Goat") && !p2)
            {
                StartCoroutine("ReturnSpeed");

                orgSpeed2 = col.gameObject.GetComponent<Player>().JumpSpeed;
                player2 = col.gameObject.GetComponent<Player>();
                p2 = true;

                col.gameObject.GetComponent<Player>().JumpSpeed = jumpSpeed;
                col.gameObject.GetComponent<Player>().OnJumpStart();
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
    }
}

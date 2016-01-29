using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class Button : MonoBehaviour {

    public List<string> keyIds;
    public float timeToRotate;
    public int playersCount;
    public List<GameObject> icons;

    internal bool heroIn;
    GameObject icon, platform;
    SpriteRenderer iconSprite;
    SpriteRenderer player1, player2;

    float angleToRotate;


	// Use this for initialization
	void Start () {
        heroIn = false;

        int id = 0;

        foreach (GameObject obj in icons)
        {
            obj.SetActive(false);
            obj.transform.GetChild(0).GetComponent<TextMesh>().text = keyIds[id];
            obj.transform.GetChild(0).GetComponent<MeshRenderer>().sortingLayerName = "Foreground";
            obj.transform.GetChild(0).GetComponent<MeshRenderer>().sortingOrder = 1;

            if (id != 0)
                obj.transform.position = new Vector2(icons[id - 1].transform.position.x + obj.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2.5f, obj.transform.position.y);

            ++id;
        }

        platform = transform.FindChild("platform").gameObject;

        if ( platform.transform.eulerAngles.z >= 0 )
            angleToRotate = 360-platform.transform.eulerAngles.z;
        else
            angleToRotate = platform.transform.eulerAngles.z + (360 - platform.transform.eulerAngles.z);

        player1 = transform.FindChild("player1").GetComponent<SpriteRenderer>();
        player2 = transform.FindChild("player2").GetComponent<SpriteRenderer>();

        if (playersCount > 1)
        {
            player1.enabled = true;
            player2.enabled = true;
        }
        else
        {
            player1.enabled = false;
            player2.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    

        if ( Input.GetMouseButtonDown(0) && heroIn )
        {
            heroIn = false;
            ShowIcons(heroIn);
            Platform();
        }

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            heroIn = true;
            ShowIcons(heroIn);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            heroIn = false;
            ShowIcons(heroIn);
        }
    }

    //-----------------------------------  icon animation to show which button should be pressed
    void ShowIcons(bool show)
    {
        if (show)
        {
            foreach(GameObject obj in icons)
            {
             
              obj.transform.localScale = Vector3.one * 0.8f;
              obj.transform.DOScale(Vector3.one * 1.1f, 0.6f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutExpo);
              obj.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
              obj.GetComponent<SpriteRenderer>().DOFade(1, 1);
              obj.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject obj in icons)
            {
                obj.SetActive(false);
            }
        }
    }


    void Platform()
    {
        platform.transform.DORotate(new Vector3(0,0,angleToRotate), timeToRotate);
    }



    
}

using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class Button : MonoBehaviour {

    public List<string> keyIds1;
    public List<string> keyIds2;
    public float timeToRotate;
    public int playersCount;
    public List<KeyCode> keyCods1;
    public List<KeyCode> keyCods2;
    public Sprite buttonSprite;
    public float timeToPress;
    public Sprite iconChicken;
    public Sprite iconGoat;

    internal bool heroIn1, heroIn2;
    GameObject icon, platform;
    GameObject player1, player2;
    SpriteRenderer iconSprite;
    SpriteRenderer player1_SR, player2_SR;

    float angleToRotate, timePlayer1, timePlayer2;
    public List<KeyCode> keyCodsPlayer1;
    List<KeyCode> keyCodsPlayer2;
    List<GameObject> icons1;
    List<GameObject> icons2;

    bool sequencePlayer1;
    bool sequencePlayer2;



	// Use this for initialization
	void Start () {
        heroIn1 = false;
        heroIn2 = false;

        player1 = transform.FindChild("player1").gameObject;
        player2 = transform.FindChild("player2").gameObject;

        icons1 = new List<GameObject>();
        icons2 = new List<GameObject>();

        keyCodsPlayer1 = new List<KeyCode>();
        keyCodsPlayer2 = new List<KeyCode>();


        int id = 0;

        foreach (Transform obj in player1.transform)
        {
            if (obj.name.Contains("tick"))
                obj.GetComponent<SpriteRenderer>().enabled = false;
            else
            {
                icons1.Add(obj.gameObject);
                obj.gameObject.SetActive(false);
                obj.GetChild(0).GetComponent<TextMesh>().text = keyIds1[id];
                obj.GetChild(0).GetComponent<MeshRenderer>().sortingLayerName = "Foreground";
                obj.GetChild(0).GetComponent<MeshRenderer>().sortingOrder = 1;

                if (id != 0)
                    obj.transform.position = new Vector2(player1.transform.GetChild(id - 1).position.x + obj.GetComponent<SpriteRenderer>().sprite.bounds.size.x + 10, player1.transform.GetChild(id - 1).position.y);
            }

            ++id;
        }

        id = 0;

        foreach (Transform obj in player2.transform)
        {
            if (obj.name.Contains("tick"))
                obj.GetComponent<SpriteRenderer>().enabled = false;
            else
            {
                icons2.Add(obj.gameObject);
                obj.gameObject.SetActive(false);
                obj.GetChild(0).GetComponent<TextMesh>().text = keyIds2[id];
                obj.GetChild(0).GetComponent<MeshRenderer>().sortingLayerName = "Foreground";
                obj.GetChild(0).GetComponent<MeshRenderer>().sortingOrder = 1;

                if (id != 0)
                    obj.transform.position = new Vector2(player2.transform.GetChild(id - 1).position.x + obj.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 6f, player2.transform.GetChild(id - 1).position.y);
            }

            ++id;
        }

        platform = transform.FindChild("platform").gameObject;

        if ( platform.transform.eulerAngles.z >= 0 )
            angleToRotate = 360-platform.transform.eulerAngles.z;
        else
            angleToRotate = platform.transform.eulerAngles.z + (360 - platform.transform.eulerAngles.z);

        player1_SR = transform.FindChild("player1").GetComponent<SpriteRenderer>();
        player2_SR = transform.FindChild("player2").GetComponent<SpriteRenderer>();


        player1_SR.enabled = false;
        player2_SR.enabled = false;

        timePlayer1 = -10;
        timePlayer2 = -10;

        sequencePlayer1 = false;
        sequencePlayer2 = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
        /*
        if ( Input.GetMouseButtonDown(0) && heroIn )
        {
            heroIn = false;
            ShowIcons(heroIn);
            Platform();
        }*/

            if (playersCount == 1)
            {
                if (heroIn1)
                {
                    //------------------------------------------- odejmowanie czasu, Resetowanie, jeśli się nie wyrobiliśmy
                    if (timePlayer1 > 0)
                    {
                         timePlayer1 -= Time.deltaTime;
                    }
                    else if ( timePlayer1 > -10 )
                    {
                         //ResetPlayer1();
                    }

                    //------------------------------------------- Input controller
                    if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.JoystickButton3))
                    {
                        // ------------------------------------ jeśli pierwszy przycisk
                        if (keyCodsPlayer1.Count == 0)
                        {
                            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                            {
                                if (Input.GetKey(vKey) && vKey.ToString().Contains("JoystickB"))
                                {
                                     keyCodsPlayer1.Add(vKey);
                                }
                            }
                        }
                        else
                        {
                            // ------------------------------------ nastepne przyciski
                            if (timePlayer1 > 0)
                            {
                                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                                {
                                    if (Input.GetKey(vKey) && vKey.ToString().Contains("JoystickB"))
                                    {
                                        keyCodsPlayer1.Add(vKey);
                                    }
                                }
                            }
                         }

                        
                        if ( keyCodsPlayer1[keyCodsPlayer1.Count-1] == keyCods1[keyCodsPlayer1.Count-1] )
                        {
                            icons1[keyCodsPlayer1.Count - 1].transform.DOKill();
                            icons1[keyCodsPlayer1.Count - 1].transform.localScale = Vector3.one;
                            icons1[keyCodsPlayer1.Count - 1].GetComponent<SpriteRenderer>().color = new Vector4(0.5f,0.5f,0.5f,1);

                            timePlayer1 = timeToPress;
                        }
                        else
                        {
                            //ResetPlayer1();
                        }
                     }
                }
            }
                //////////////////////////------------------------------ TO DO
            else
            {
                /*
                if (heroIn1)
                {
                    if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick1Button3))
                    {

                        Event e = Event.current;

                        keyCodsPlayer1.Add(e.keyCode);

                        if (keyCodsPlayer1.Count == 1)
                        {
                            timePlayer1 = timeToPress;
                        }
                    }
                }

                if (heroIn2)
                {
                    if (Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.Joystick2Button1) || Input.GetKeyDown(KeyCode.Joystick2Button2) || Input.GetKeyDown(KeyCode.Joystick2Button3))
                    {

                        Event e = Event.current;

                        keyCodsPlayer2.Add(e.keyCode);

                        if (keyCodsPlayer2.Count == 1)
                        {
                            timePlayer2 = timeToPress;
                        }
                    }
                }
                 * */
            }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            if (playersCount == 1)
            {
                heroIn1 = true;

                if (col.gameObject.name.Contains("chicken"))
                    player1_SR.sprite = iconChicken;
                else
                    player1_SR.sprite = iconGoat;
            }
            else
            {
                if (col.gameObject.name.Contains("goat"))
                {
                    heroIn1 = true;
                }
                else
                {
                    heroIn2 = true;
                }

                player1_SR.sprite = iconGoat;
                player2_SR.sprite = iconChicken;
            }

            ShowIcons(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            if (playersCount == 1)
            {
                heroIn1 = false;
                ShowIcons(false);
            }
            else
            {
                if (col.gameObject.name.Contains("goat"))
                {
                    heroIn1 = false;
                }
                else
                {
                    heroIn2 = false;
                }

                if (!heroIn1 && !heroIn2)
                {
                    ShowIcons(false);
                }
            }
        }
    }

    //-----------------------------------  icon animation to show which button should be pressed
    void ShowIcons(bool show)
    {
        if (show)
        {
            player1_SR.enabled = true;

            foreach (Transform obj in player1.transform)
            {

                obj.localScale = Vector3.one * 0.8f;
                obj.DOScale(Vector3.one * 1f, 0.6f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutExpo);
                obj.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
                obj.GetComponent<SpriteRenderer>().DOFade(1, 1);
                obj.gameObject.SetActive(true);
            }

            if (playersCount == 2)
            {
                player2_SR.enabled = true;

                foreach (Transform obj in player2.transform)
                {
                    obj.localScale = Vector3.one * 0.8f;
                    obj.DOScale(Vector3.one * 1f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutExpo);
                    obj.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
                    obj.GetComponent<SpriteRenderer>().DOFade(1, 1);
                    obj.gameObject.SetActive(true);
                }
            }

        }
        else
        {
            player1_SR.enabled = false;

            foreach (Transform obj in player1.transform)
            {
                obj.gameObject.SetActive(false);
            }

            if (playersCount == 2)
            {
                player1_SR.enabled = false;

                foreach (Transform obj in player2.transform)
                {
                    obj.gameObject.SetActive(false);
                }
            }
        }
    }

    void Platform()
    {
        platform.transform.DORotate(new Vector3(0,0,angleToRotate), timeToRotate);
    }
 
    void ResetPlayer1()
    {
        timePlayer1 = -10;
        keyCodsPlayer1.Clear();

        Debug.Log("RESET");
        if (playersCount == 1)
        {
            if (heroIn1)
                ShowIcons(true);
            else
                ShowIcons(false);
        }
        else
        {
            if (!heroIn1 && !heroIn2)
                ShowIcons(false);
            else
                ShowIcons(true);

        }
    }

    void ResetPlayer2()
    {
        timePlayer2 = -10;
    }
}

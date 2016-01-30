using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using Assets.Scripts;

public class Button : MonoBehaviour {

    public List<string> keyIds1;
    public List<string> keyIds2;
    public float timeToRotate;
    public int playersCount;
    public List<KeyCode> keyCods1;
    public List<KeyCode> keyCods2;
    public List<Color> keyColors;
    public Sprite buttonSprite;
    public float timeToPress;
    public Sprite iconChicken;
    public Sprite iconGoat;
    public GameObject actionObj;

    internal bool heroIn1, heroIn2, done;
    GameObject icon, platform;
    GameObject player1, player2;
    SpriteRenderer iconSprite;
    SpriteRenderer player1_SR, player2_SR;

    float angleToRotate, timePlayer1, timePlayer2;
    public List<KeyCode> keyCodsPlayer1;
    public List<KeyCode> keyCodsPlayer2;
    List<GameObject> icons1;
    List<GameObject> icons2;

    bool done1;
    bool done2;



	// Use this for initialization
	void Start () {
        heroIn1 = false;
        heroIn2 = false;

        done1 = false;
        done2 = false;

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
                    obj.transform.position = new Vector2(player1.transform.GetChild(id - 1).position.x + obj.GetComponent<SpriteRenderer>().sprite.bounds.size.x/0.8f, player1.transform.GetChild(id - 1).position.y);
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
                    obj.transform.position = new Vector2(player2.transform.GetChild(id - 1).position.x + obj.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 0.8f, player2.transform.GetChild(id - 1).position.y);
            }

            ++id;
        }

        /*
        platform = transform.FindChild("platform").gameObject;

        if ( platform.transform.eulerAngles.z >= 0 )
            angleToRotate = 360-platform.transform.eulerAngles.z;
        else
            angleToRotate = platform.transform.eulerAngles.z + (360 - platform.transform.eulerAngles.z);
        */


        player1_SR = transform.FindChild("player1").GetComponent<SpriteRenderer>();
        player2_SR = transform.FindChild("player2").GetComponent<SpriteRenderer>();


        player1_SR.enabled = false;
        player2_SR.enabled = false;

        timePlayer1 = -10;
        timePlayer2 = -10;
	}
	
	// Update is called once per frame
	void Update () {

        if (!done)
        {

            if (playersCount == 1)
            {
                if (heroIn1 && !done1)
                {
                    //------------------------------------------- odejmowanie czasu, Resetowanie, jeśli się nie wyrobiliśmy
                    if (timePlayer1 > 0)
                    {
                        timePlayer1 -= Time.deltaTime;
                    }
                    else if (timePlayer1 > -10)
                    {
                        ResetPlayer1();
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


                        if (keyCodsPlayer1[keyCodsPlayer1.Count - 1] == keyCods1[keyCodsPlayer1.Count - 1])
                        {
                            icons1[keyCodsPlayer1.Count - 1].transform.DOKill();
                            icons1[keyCodsPlayer1.Count - 1].transform.localScale = Vector3.one;
                            icons1[keyCodsPlayer1.Count - 1].GetComponent<SpriteRenderer>().color = new Vector4(0.5f, 0.5f, 0.5f, 1);

                            if (keyCodsPlayer1.Count == keyCods1.Count)
                            {
                                done1 = true;
                                timePlayer1 = -10;
                                player1.transform.FindChild("tick").GetComponent<SpriteRenderer>().enabled = true;
                                player1.transform.FindChild("tick").DOScale(Vector3.one, 0.6f).SetEase(Ease.OutExpo);

                                ShowIcons(false);
                                done = true;

                                actionObj.GetComponent<Barrier>().startAction = true;

                                StartCoroutine("DeactivateAll");
                            }

                            timePlayer1 = timeToPress;
                        }
                        else
                        {
                            ResetPlayer1();
                        }
                    }
                }
            }
            //////////////////////////------------------------------ TO DO
            else
            {

                //----------------------------------------------------------------  HERO 1

                if (heroIn1 && !done1)
                {
                    //------------------------------------------- odejmowanie czasu, Resetowanie, jeśli się nie wyrobiliśmy
                    if (timePlayer1 > 0)
                    {
                        timePlayer1 -= Time.deltaTime;
                    }
                    else if (timePlayer1 > -10)
                    {
                        ResetPlayer1();
                    }

                    //------------------------------------------- Input controller
                    if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick1Button3))
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


                        if (keyCodsPlayer1[keyCodsPlayer1.Count - 1] == keyCods1[keyCodsPlayer1.Count - 1])
                        {
                            icons1[keyCodsPlayer1.Count - 1].transform.DOKill();
                            icons1[keyCodsPlayer1.Count - 1].transform.localScale = Vector3.one;
                            icons1[keyCodsPlayer1.Count - 1].GetComponent<SpriteRenderer>().color = new Vector4(0.5f, 0.5f, 0.5f, 1);

                            if (keyCodsPlayer1.Count == keyCods1.Count)
                            {
                                done1 = true;
                                timePlayer1 = -10;
                                player1.transform.FindChild("tick").gameObject.SetActive(true);
                                player1.transform.FindChild("tick").GetComponent<SpriteRenderer>().enabled = true;
                                player1.transform.FindChild("tick").GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                                player1.transform.FindChild("tick").DOScale(Vector3.one, 0.6f).SetEase(Ease.OutExpo);
                            }

                            timePlayer1 = timeToPress;
                        }
                        else
                        {
                            ResetPlayer1();
                        }
                    }
                }


                //----------------------------------------------------------------  HERO 2

                if (heroIn2 && !done2)
                {
                    //------------------------------------------- odejmowanie czasu, Resetowanie, jeśli się nie wyrobiliśmy
                    if (timePlayer2 > 0)
                    {
                        timePlayer2 -= Time.deltaTime;
                    }
                    else if (timePlayer2 > -10)
                    {
                        ResetPlayer1();
                    }

                    //------------------------------------------- Input controller
                    if (Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.Joystick2Button1) || Input.GetKeyDown(KeyCode.Joystick2Button2) || Input.GetKeyDown(KeyCode.Joystick2Button3))
                    {
                        // ------------------------------------ jeśli pierwszy przycisk
                        if (keyCodsPlayer2.Count == 0)
                        {
                            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                            {
                                if (Input.GetKey(vKey) && vKey.ToString().Contains("JoystickB"))
                                {
                                    keyCodsPlayer2.Add(vKey);
                                }
                            }
                        }
                        else
                        {
                            // ------------------------------------ nastepne przyciski
                            if (timePlayer2 > 0)
                            {
                                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                                {
                                    if (Input.GetKey(vKey) && vKey.ToString().Contains("JoystickB"))
                                    {
                                        keyCodsPlayer2.Add(vKey);
                                    }
                                }
                            }
                        }


                        if (keyCodsPlayer2[keyCodsPlayer2.Count - 1] == keyCods2[keyCodsPlayer2.Count - 1])
                        {
                            icons2[keyCodsPlayer2.Count - 1].transform.DOKill();
                            icons2[keyCodsPlayer2.Count - 1].transform.localScale = Vector3.one;
                            icons2[keyCodsPlayer2.Count - 1].GetComponent<SpriteRenderer>().color = new Vector4(0.5f, 0.5f, 0.5f, 1);


                            if (keyCodsPlayer2.Count == keyCods2.Count)
                            {
                                done2 = true;
                                timePlayer2 = -10;
                                player2.transform.FindChild("tick").gameObject.SetActive(true);
                                player2.transform.FindChild("tick").GetComponent<SpriteRenderer>().enabled = true;
                                player2.transform.FindChild("tick").GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                                player2.transform.FindChild("tick").DOScale(Vector3.one, 0.6f).SetEase(Ease.OutExpo);
                            }

                            timePlayer2 = timeToPress;
                        }
                        else
                        {
                            ResetPlayer1();
                        }
                    }
                }

                if (done1 && done2)
                {
                    ShowIcons(false);
                    StartCoroutine("DeactivateAll");
                    actionObj.GetComponent<Barrier>().startAction = true;
                    done = true;
                }


            }
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero") )
        {
            col.gameObject.GetComponent<Player>().SequenceMode = true;

            if (playersCount == 1)
            {
                heroIn1 = true;

                if (col.gameObject.name.Contains("chicken"))
                    player1_SR.sprite = iconChicken;
                else
                    player1_SR.sprite = iconGoat;

                ShowIcons(true);

            }
            else
            {

                player2_SR.sprite = iconGoat;
                player1_SR.sprite = iconChicken;

                if (col.gameObject.name.Contains("chicken"))
                {
                    heroIn1 = true;
                }

                if (col.gameObject.name.Contains("Goat"))
                {
                    heroIn2 = true;
                }


                if ( (heroIn1 && !heroIn2) || (heroIn2 && !heroIn1) || (heroIn1 && heroIn2 ))
                {
                    ShowIcons(true);
                }
            }


        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero") )
        {
            col.gameObject.GetComponent<Player>().SequenceMode = false;

            if (playersCount == 1)
            {
                heroIn1 = false;
                ShowIcons(false);
            }
            else
            {
                if (col.gameObject.name.Contains("chicken"))
                {
                    heroIn1 = false;
                }
                if (col.gameObject.name.Contains("Goat"))
                {
                    heroIn2 = false;
                }

                if (!heroIn1 && !heroIn2)
                {
                    ShowIcons(false);
                }
                else
                    ShowIcons(true);
            }
        }
    }

    //-----------------------------------  icon animation to show which button should be pressed
    void ShowIcons(bool show)
    {
        if (show)
        {

            if (playersCount == 1)
            {
                player1_SR.color = new Vector4(1, 1, 1, 0);
                player1_SR.DOFade(1, 0.5f);
                player1_SR.enabled = true;

                int id = 0;
                Color color = Color.white;

                foreach (Transform obj in player1.transform)
                {
                    if (!obj.name.Contains("tick"))
                    {
                        if (!done1)
                        {
                            obj.transform.DOKill();
                            obj.localScale = Vector3.one * 0.8f;
                            obj.DOScale(Vector3.one * 1f, 0.6f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutExpo);


                            if (keyIds1[id] == "A")
                                obj.GetComponent<SpriteRenderer>().color = keyColors[0];
                            else if (keyIds1[id] == "B")
                                obj.GetComponent<SpriteRenderer>().color = keyColors[1];
                            else if (keyIds1[id] == "X")
                                obj.GetComponent<SpriteRenderer>().color = keyColors[2];
                            else
                                obj.GetComponent<SpriteRenderer>().color = keyColors[3];

                            color = obj.GetComponent<SpriteRenderer>().color;

                            obj.GetComponent<SpriteRenderer>().color = new Vector4(color.r, color.g, color.b, 0);
                        }
                        else
                            obj.GetComponent<SpriteRenderer>().color = new Vector4(0.5f, 0.5f, 0.5f, 0);

                        obj.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                        obj.gameObject.SetActive(true);
                    }

                    ++id;
                }
            }

            else if (playersCount == 2)
            {
                if (!heroIn1)
                {
                    player1_SR.color = new Vector4(0.5f, 0.5f, 0.5f, 0);
                    player1_SR.DOFade(1, 0.5f);
                    player1_SR.enabled = true;

                    foreach (Transform obj in player1.transform)
                    {
                        if (!obj.name.Contains("tick"))
                        {
                            obj.GetComponent<SpriteRenderer>().color = new Vector4(0.5f, 0.5f, 0.5f, 0);
                            obj.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                            obj.gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    player1_SR.color = new Vector4(1, 1, 1, 0);
                    player1_SR.DOFade(1, 0.5f);
                    player1_SR.enabled = true;

                    int id = 0;
                    Color color = Color.white;

                    foreach (Transform obj in player1.transform)
                    {
                        if (!obj.name.Contains("tick"))
                        {
                            if (!done1)
                            {
                                obj.transform.DOKill();
                                obj.localScale = Vector3.one * 0.8f;
                                obj.DOScale(Vector3.one * 1f, 0.6f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutExpo);


                                if (keyIds1[id] == "A")
                                    obj.GetComponent<SpriteRenderer>().color = keyColors[0];
                                else if (keyIds1[id] == "B")
                                    obj.GetComponent<SpriteRenderer>().color = keyColors[1];
                                else if (keyIds1[id] == "X")
                                    obj.GetComponent<SpriteRenderer>().color = keyColors[2];
                                else
                                    obj.GetComponent<SpriteRenderer>().color = keyColors[3];

                                color = obj.GetComponent<SpriteRenderer>().color;

                                obj.GetComponent<SpriteRenderer>().color = new Vector4(color.r, color.g, color.b, 0);
                            }
                            else
                                obj.GetComponent<SpriteRenderer>().color = new Vector4(0.5f, 0.5f, 0.5f, 0);

                            obj.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                            obj.gameObject.SetActive(true);
                        }

                        ++id;
                    }
                }

                if (!heroIn2)
                {
                    player2_SR.color = new Vector4(0.5f, 0.5f, 0.5f, 0);
                    player2_SR.DOFade(1, 0.5f);
                    player2_SR.enabled = true;

                    foreach (Transform obj in player2.transform)
                    {
                        if (!obj.name.Contains("tick"))
                        {
                            obj.GetComponent<SpriteRenderer>().color = new Vector4(0.5f, 0.5f, 0.5f, 0);

                            obj.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                            obj.gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    player2_SR.color = new Vector4(1, 1, 1, 0);
                    player2_SR.DOFade(1, 0.5f);
                    player2_SR.enabled = true;

                    int id = 0;
                    Color color = Color.white;

                    foreach (Transform obj in player2.transform)
                    {
                        if (!obj.name.Contains("tick"))
                        {
                            if (!done2)
                            {
                                obj.transform.DOKill();
                                obj.localScale = Vector3.one * 0.8f;
                                obj.DOScale(Vector3.one * 1f, 0.6f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutExpo);


                                if (keyIds2[id] == "A")
                                    obj.GetComponent<SpriteRenderer>().color = keyColors[0];
                                else if (keyIds2[id] == "B")
                                    obj.GetComponent<SpriteRenderer>().color = keyColors[1];
                                else if (keyIds2[id] == "X")
                                    obj.GetComponent<SpriteRenderer>().color = keyColors[2];
                                else
                                    obj.GetComponent<SpriteRenderer>().color = keyColors[3];

                                color = obj.GetComponent<SpriteRenderer>().color;

                                obj.GetComponent<SpriteRenderer>().color = new Vector4(color.r, color.g, color.b, 0);
                            }
                            else
                                obj.GetComponent<SpriteRenderer>().color = new Vector4(0.5f, 0.5f, 0.5f, 0);

                            obj.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                            obj.gameObject.SetActive(true);
                        }

                        ++id;
                    }
                }

                if (done1)
                {
                    player1.transform.FindChild("tick").GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                    player1.transform.FindChild("tick").gameObject.SetActive(true);
                }
                
                if (done2)
                {
                    player2.transform.FindChild("tick").GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                    player2.transform.FindChild("tick").gameObject.SetActive(true);
                }

            }
        }
        else
        {
           player1_SR.DOFade(0, 0.5f);

           foreach (Transform obj in player1.transform)
           {
               obj.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
           }

           if (playersCount == 2)
           {
               player2_SR.DOFade(0, 0.5f);

               foreach (Transform obj in player2.transform)
               {
                   obj.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
               }
           }

          StartCoroutine("DeactivateAll");
       }
    }

    /*
    void Platform()
    {
        platform.transform.DORotate(new Vector3(0,0,angleToRotate), timeToRotate);
    }
     * */
 
    void ResetPlayer1()
    {
        timePlayer1 = -10;
        keyCodsPlayer1.Clear();

        timePlayer2 = -10;
        keyCodsPlayer2.Clear();


        if (playersCount == 1)
        {
            if (heroIn1)
                ShowIcons(true);
            else
                ShowIcons(false);
        }
        else
        {
            if (heroIn1)
                ShowIcons(true);
            else
            {
                if (!heroIn2)
                    ShowIcons(false);
            }

            if (heroIn2)
            {
                if (!heroIn1)
                    ShowIcons(true);
            }
            else
            {
                //if (!heroIn1)
                    //ShowIcons(false);
            }

        }
    }


    void ResetPlayer2()
    {
        timePlayer2 = -10;
    }

    IEnumerator DeactivateAll()
    {
        yield return new WaitForSeconds(0.35f);

        player1_SR.enabled = false;
        player1_SR.color = new Color(1, 1, 1, 1);

        foreach (Transform obj in player1.transform)
        {
            obj.gameObject.SetActive(false);
            obj.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        }


        if (playersCount == 2)
        {
            player2_SR.enabled = false;
            player2_SR.color = new Color(1, 1, 1, 1);

            foreach (Transform obj in player2.transform)
            {
                obj.gameObject.SetActive(false);
                obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }

            if (heroIn1 && heroIn2)
            {
                player1.SetActive(false);
                player2.SetActive(false);
            }
        }
        else
        {
            if (heroIn1)
                player1.SetActive(false);
        }


    }
}

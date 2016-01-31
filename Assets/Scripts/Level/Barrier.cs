using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Barrier : MonoBehaviour {

    //---------------------- 0 : polka rotujaca
    //---------------------  1 : 

    public int idType;
    public bool startAction;
    public float timeToAnim;
    public float angleToRotate;

	// Use this for initialization
	void Start () {

        startAction = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (startAction)
        {
			GetComponent<AudioSource>().Play();
            SetAction();
            startAction = false;
        }
	}

    void SetAction()
    {
        switch (idType)
        {
            case 0:
               // float angleToRotate;
                
                /*
                if (transform.eulerAngles.z >= 90)
                    angleToRotate = transform.eulerAngles.z + (360 - transform.eulerAngles.z);
                else
                    angleToRotate = 360 - transform.eulerAngles.z;
                 * */

                transform.DORotate(new Vector3(0, 0, angleToRotate), timeToAnim);
                break;
            case 1:
                break;
            case 2:
                break;

        }
    }
}

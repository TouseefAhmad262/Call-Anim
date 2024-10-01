using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ToFollow;

    bool CanMove;
    public List<ViewCheck> ClampPonts;

    ViewCheck InViewObject;
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ClampPonts.Count; i++)
        {
            if (ClampPonts[i].isInView)
            {
                CanMove = false;
                InViewObject = ClampPonts[i];
                break;
            }
            else
            {
                CanMove = true;
                InViewObject = null;
            }
        }

        if(InViewObject != null)
        {
            if(InViewObject.transform.position.x < transform.position.x)
            {
                if (ToFollow.gameObject.GetComponent<Movement>().Forward && ToFollow.transform.position.x > transform.position.x)
                {
                    CanMove = true;
                }
                else if(!ToFollow.gameObject.GetComponent<Movement>().Forward && ToFollow.transform.position.x < transform.position.x)
                {
                    CanMove = false;
                }
            }
            else
            {
                if (!ToFollow.gameObject.GetComponent<Movement>().Forward && ToFollow.transform.position.x < transform.position.x)
                {
                    CanMove = true;
                }
                else if (ToFollow.gameObject.GetComponent<Movement>().Forward && ToFollow.transform.position.x > transform.position.x)
                {
                    CanMove = false;
                }
            }
        }
        if(CanMove)
        {
            transform.position = new Vector3(ToFollow.position.x, transform.position.y, transform.position.z);
        }
    }
}
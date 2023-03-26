using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCamera_onoff : MonoBehaviour
{
    //FollowCam onoff; // ÆÈ·Î¿ì Ä·
    void Start()
    {
        //onoff = GameObject.Find("Follow_Camera").GetComponent<FollowCam>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "BatFollower1(Clone)")
        {
            //onoff.enabled = true;
           
        }
        
        if (other.gameObject.name == "BatFollower2(Clone)")
        {
            //onoff.enabled = true;
        }

        if (other.gameObject.name == "BatFollower3(Clone)")
        {
            //onoff.enabled = true;
        }
    }
    private void OnDestroy()
    {
        //onoff.enabled = false;
    }
    void Update()
    {
        
    }
}

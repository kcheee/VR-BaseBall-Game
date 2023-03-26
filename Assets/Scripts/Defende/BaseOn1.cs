using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseOn1 : MonoBehaviour
{

    public static bool hitterSafe = false;
    public static bool Safe_audioclip = false;
    public static bool hitterOnBase = false;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Hitter")
        {
            Defende.BasePos[0] = true;
            Hitter_BaseOn.Hitter1_Base = true;
      
        }
    }


}

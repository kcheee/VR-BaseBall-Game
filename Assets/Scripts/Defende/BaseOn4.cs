using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseOn4 : MonoBehaviour
{


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Hitter")
        {
            Defende.BasePos[3] = true;
            Hitter_BaseOn.Hitter1_Base = true;

        }
    }

}

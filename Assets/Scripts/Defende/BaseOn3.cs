using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseOn3 : MonoBehaviour
{


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Hitter")
        {
            Defende.BasePos[2] = true;
            Hitter_BaseOn.Hitter1_Base = true;

        }
    }

}

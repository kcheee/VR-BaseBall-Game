using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baseball_Kinds : MonoBehaviour
{


    int ran;


    // Start is called before the first frame update
    void Start()
    {
        ran = Random.Range(1, 1);   // 1이상 5 미만

        //switch (ran)
        //{
        //    case 1:
        //        Debug.Log("baseball");
        //        break;
        //    case 2:
        //        Debug.Log("foke_ball");
        //        break;
        //    case 3:
        //        Debug.Log("courve_ball");
        //        break;
        //    case 4:
        //        Debug.Log("screw_ball");
        //        break;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == ("ChangePoint"))
        {
            if (ran == 2)   // 포크볼
            {
                GetComponent<Rigidbody>().AddForce(-transform.up * 50);
            }
            if (ran == 3)   // 커브볼
            {
                GetComponent<Rigidbody>().AddForce(transform.right * 50);
            }
            if (ran == 4)   // 스크류볼
            {
                GetComponent<Rigidbody>().AddForce(-transform.right * 50);
            }
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw_Ball : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    float plus_power = 2.0f;

    void Start()
    {
        

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name==("ChangePoint"))
        {
     
            GetComponent<Rigidbody>().AddForce(transform.right * -150 );     // 홈 플레이트 구간 근처로 가면 공이 왼쪽으로 휨
            GetComponent<Rigidbody>().AddForce(transform.up * 100);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
}

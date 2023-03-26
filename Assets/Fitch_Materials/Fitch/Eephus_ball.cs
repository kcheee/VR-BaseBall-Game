using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eephus_ball : MonoBehaviour
{
    
    [SerializeField] float speed = 20f;
    [SerializeField] float Eephus_speed = 20f;
    bool bLanded = false;  // 공 생성할때 true로 만들기
    float plus_power = 2.0f;

    void Start()
    {
        var velocity = speed * transform.forward * plus_power;
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.AddForce(velocity, ForceMode.VelocityChange);


    }

    void FixedUpdate()
    {
        if (!bLanded)
            GetComponent<Rigidbody>().AddForce(transform.up * Eephus_speed);

    }
    void OnTriggerEnter(Collider other)          //스트라이크 존에 닿으면 사라짐
    {

        if(!bLanded)
        if (other.gameObject.name.Contains("ChangePoint"))
        {
           
            GetComponent<Rigidbody>().AddForce(transform.up * -250);     // 홈 플레이트 구간 근처로 가면 공이 떨어짐
                bLanded = true;
        }
        if (other.gameObject.name.Contains("Ground"))
        {
           
            // GetComponent<Rigidbody>().AddForce(transform.right * -60);

            bLanded = true;
        }
        //Destroy(gameObject);            
    }
   
}

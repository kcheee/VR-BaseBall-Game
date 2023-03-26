using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screwball : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    float plus_power = 2.0f;

    void Start()
    {
        var velocity = speed * transform.forward * plus_power;
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.AddForce(velocity, ForceMode.VelocityChange);

    }
   

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("ChangePoint"))
        {
            Debug.Log("Change1");
            GetComponent<Rigidbody>().AddForce(transform.right * -150);     // 홈 플레이트 구간 근처로 가면 공이 왼쪽으로 휨

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other)       //스트라이크 존에 닿으면 사라짐
    {
        if (other.collider.tag == "Zone")
        {

            Destroy(gameObject);
        }


    }
}

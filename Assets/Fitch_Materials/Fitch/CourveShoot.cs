using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CourveShoot : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float courve_speed = 100f;
    bool bLanded = false;  // 공 생성할때 true로 만들기
    float plus_power = 1.0f;

    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == ("ChangePoint"))
        {
            GetComponent<Rigidbody>().AddForce(transform.right * 300);     // 홈 플레이트 구간 근처로 가면 공이 왼쪽으로 휨
            GetComponent<Rigidbody>().AddForce(-transform.up * 100);
        }
    }
}

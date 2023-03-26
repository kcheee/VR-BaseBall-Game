using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fokeball : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    float plus_power = 2.0f;
    
    void Start()
    {
      
      
    }
    void FixedUpdate()
    {

        /*GetComponent<Rigidbody>().AddForce(transform.up * -90);*/  // 문제점 - 공이 사라질때까지 힘을 준다.
        //Debug.Log(transform.up);
    }
   
    private void OnTriggerEnter(Collider other)     
    {  
        if (other.gameObject.name == ("ChangePoint"))
        {         
            GetComponent<Rigidbody>().AddForce(-transform.up*100);     // 홈 플레이트 구간 근처로 가면 공이 떨어짐
          
        }
       
    }
 
    // Update is called once per frame
    void Update()
    {
       
    }
  
}

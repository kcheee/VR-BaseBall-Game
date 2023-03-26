using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] float angularVelocity = 30f;

    float horizontalAngle = 0f; // 수평 방향의 회전량을 저장
    float verticalAngle = 0f; // 수직 방향의 회전량을 저장



    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 입력에 따라 회전량을 취득

        var horizontalRotation = Input.GetAxis("Horizontal") * angularVelocity * Time.deltaTime;
        var verticalRotation = -Input.GetAxis("Vertical") * angularVelocity * Time.deltaTime;

        // 회전량을 갱신
        horizontalAngle += horizontalRotation;
        verticalAngle +=   verticalRotation;

        // 수직 방향은 너무 회전하지 않게 제한

        verticalAngle = Mathf.Clamp(verticalAngle, -80f, 80f);

        //Transform 컴포넌트에 회전량을 적용
        transform.rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0f);
    }

}

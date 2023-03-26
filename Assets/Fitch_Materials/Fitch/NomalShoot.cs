using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalShoot : MonoBehaviour
{

    [SerializeField] float speed = 9; // 야구공 속도 [m/s]  // 

    float cnt = 0;

    Angle_confirm Angle_cofirm_on_Off;


    Vector3 destination1; // 거리공식에 쓰이는 위치
    Vector3 destination2;
    Vector3 startPosition;


    Vector3 Hit_location;

    /*각, 거리 */
    public float angle1;    //각도
    public float destination; // 최종거리

    public float testdestination; // 전체수평거리 

    float velo; // 공이 배트에 맞고 초기속도
    public float angle;

    bool paul = false;  // 파울 변수
    bool timeset = false;   // 공이 배트에 맞았을 경우



    float GetAngle(Vector3 vS, Vector3 vE)   // -180  ~ 180    y축 각도 조절
    {
        //Vector3 vE = gameObject.transform.position;
        Vector3 v = vE - vS;

        //float getAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg; // Atan2 탄젠트 공식,rad2deg 라디안을 degree로 바꿈

        float getAngle = Mathf.Atan(v.y) * Mathf.Rad2Deg; // Atan 탄젠트 공식 배트와 공 각

        return getAngle;
    }
    public float CalculateAngle()   // -360 ~ 360   //각도가 변하는 이유가 뭘까    // 왼쪽 축 기준으로 0 ~ 360
    {

        Vector3 pos = gameObject.transform.position;
        Vector3 k = new Vector3(0, 0.5f, 0);


        return Quaternion.FromToRotation(-Vector3.forward, k - pos).eulerAngles.y;
        // 쿼터니언 스칼라값을 정할 수 있음,오일러각 각도를 나타내줌
        //fromToRotation(중심축,회전하고싶은 방향벡터)
    }

    private void OnDestroy()
    {
        Angle_cofirm_on_Off.enabled = false;

    }

    void Start()
    {
        Angle_cofirm_on_Off = GameObject.Find("Destination").GetComponent<Angle_confirm>();

        startPosition = new Vector3(-0.3f, 0.5f, 0);

        var velocity = speed * transform.forward;
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.AddForce(velocity, ForceMode.VelocityChange); // 공이 앞쪽으로 나가는 코드

    }
    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.name == "BatFollower1(Clone)")
        {
            GameObject bat1 = GameObject.Find("BatFollower1(Clone)");
            float a = bat1.GetComponent<BatCapsuleFollower>().getVelocity();
            var velocity = speed * transform.position * 3.5f * (a / 10) / 500;     // 야구공 스피트와 휘두르는 힘 배트위치에너지 힘
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(velocity, ForceMode.VelocityChange);
            paul = true;    // 파울체크
            timeset = true;    // 각도와 위치구하는 코드
   
        }

        if (other.gameObject.name == "BatFollower2(Clone)")       // 배트에 공이 맞는 위치에 따라 다른 힘
        {
            GameObject bat2 = GameObject.Find("BatFollower2(Clone)");
            float b = bat2.GetComponent<BatCapsuleFollower>().getVelocity();
            var velocity = speed * transform.position * 1.2f * (b / 10) / 600;
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(velocity, ForceMode.VelocityChange);
            paul = true;
            timeset = true;
        }

        if (other.gameObject.name == "BatFollower3(Clone)")
        {
            GameObject bat3 = GameObject.Find("BatFollower3(Clone)");
            float c = bat3.GetComponent<BatCapsuleFollower>().getVelocity();
            var velocity = speed * transform.position * 1.1f * (c / 10) / 700; // 초기속도
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(velocity, ForceMode.VelocityChange);
            paul = true;
            timeset = true;
        }

        //if (other.gameObject.tag == "Zone")    // 존에 닿으면 사라짐
        //{
        //    Destroy(gameObject);

        //}

        //if (other.gameObject.tag == "Faul" && SendCollision.baseball_Ground == false)    //스트라이크 존에 닿으면 사라짐
        //{
        //    Debug.Log("실행");
        //    Destroy(gameObject);

        //}

    }
    public void FixedUpdate()
    {
       

        if (timeset == true)
        {
            cnt += Time.fixedDeltaTime;  // 1프레임 0.02초
            if (0.00f < cnt && cnt < 0.03f)
            {
                destination1 = gameObject.transform.position;
            }
            if (0.05f < cnt && cnt < 0.08f)
            {
                destination2 = gameObject.transform.position;
            }
            if (0.1f < cnt && cnt < 0.13f)  // y축 각도의 오차를 줄이기 위해
            {
                angle = GetAngle(destination1, destination2);
            }
            if (1f < cnt && cnt < 1.02f)
            {
                /* Debug.Log("첫번째 좌표" + destination1 + "  두번째 좌표 : " + destination2);*/ // y 축 값 오류 뜸

                //Debug.Log("각도"+angle);   // 기준으로 잡았던 vector3 (0,0.5f,0) 보다 x의 위치가 작게 되면 둔각이기때문에 각도가 이상해짐
                velo = Vector3.Distance(destination2, destination1) / 0.04f;
                //Debug.Log("속도" + velo);

                destination = Mathf.Pow(velo, 2) / 9.8f * Mathf.Sin((2 * angle) * Mathf.Deg2Rad);   // 범위 각도 라디안으로

                testdestination = Mathf.Pow(velo, 2) * Mathf.Sin(angle) * Mathf.Cos(angle) / 9.8f;

                //Debug.Log("범위" + destination + "  y축 각도 : " + angle);
                angle1 = CalculateAngle();  //x,z 축 각도

                //Debug.Log("범위" + testdestination);

                Angle_cofirm_on_Off.enabled = true;

                //Debug.Log("각도" + CalculateAngle());

                //Debug.Log("x z 축 각도" + CalculateAngle());
                Hit_location = gameObject.transform.forward;

                /* Debug.Log("Dest" + Dest + Vector3.Magnitude(posi) + rotate); */   // x
            }
        }

        Vector3 drawPoint = Hit_location + gameObject.transform.position;

        Debug.DrawLine(Hit_location, drawPoint, Color.red);

    }
}

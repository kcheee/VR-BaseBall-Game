using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalShoot : MonoBehaviour
{

    [SerializeField] float speed = 9; // �߱��� �ӵ� [m/s]  // 

    float cnt = 0;

    Angle_confirm Angle_cofirm_on_Off;


    Vector3 destination1; // �Ÿ����Ŀ� ���̴� ��ġ
    Vector3 destination2;
    Vector3 startPosition;


    Vector3 Hit_location;

    /*��, �Ÿ� */
    public float angle1;    //����
    public float destination; // �����Ÿ�

    public float testdestination; // ��ü����Ÿ� 

    float velo; // ���� ��Ʈ�� �°� �ʱ�ӵ�
    public float angle;

    bool paul = false;  // �Ŀ� ����
    bool timeset = false;   // ���� ��Ʈ�� �¾��� ���



    float GetAngle(Vector3 vS, Vector3 vE)   // -180  ~ 180    y�� ���� ����
    {
        //Vector3 vE = gameObject.transform.position;
        Vector3 v = vE - vS;

        //float getAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg; // Atan2 ź��Ʈ ����,rad2deg ������ degree�� �ٲ�

        float getAngle = Mathf.Atan(v.y) * Mathf.Rad2Deg; // Atan ź��Ʈ ���� ��Ʈ�� �� ��

        return getAngle;
    }
    public float CalculateAngle()   // -360 ~ 360   //������ ���ϴ� ������ ����    // ���� �� �������� 0 ~ 360
    {

        Vector3 pos = gameObject.transform.position;
        Vector3 k = new Vector3(0, 0.5f, 0);


        return Quaternion.FromToRotation(-Vector3.forward, k - pos).eulerAngles.y;
        // ���ʹϾ� ��Į���� ���� �� ����,���Ϸ��� ������ ��Ÿ����
        //fromToRotation(�߽���,ȸ���ϰ���� ���⺤��)
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

        rigidbody.AddForce(velocity, ForceMode.VelocityChange); // ���� �������� ������ �ڵ�

    }
    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.name == "BatFollower1(Clone)")
        {
            GameObject bat1 = GameObject.Find("BatFollower1(Clone)");
            float a = bat1.GetComponent<BatCapsuleFollower>().getVelocity();
            var velocity = speed * transform.position * 3.5f * (a / 10) / 500;     // �߱��� ����Ʈ�� �ֵθ��� �� ��Ʈ��ġ������ ��
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(velocity, ForceMode.VelocityChange);
            paul = true;    // �Ŀ�üũ
            timeset = true;    // ������ ��ġ���ϴ� �ڵ�
   
        }

        if (other.gameObject.name == "BatFollower2(Clone)")       // ��Ʈ�� ���� �´� ��ġ�� ���� �ٸ� ��
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
            var velocity = speed * transform.position * 1.1f * (c / 10) / 700; // �ʱ�ӵ�
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(velocity, ForceMode.VelocityChange);
            paul = true;
            timeset = true;
        }

        //if (other.gameObject.tag == "Zone")    // ���� ������ �����
        //{
        //    Destroy(gameObject);

        //}

        //if (other.gameObject.tag == "Faul" && SendCollision.baseball_Ground == false)    //��Ʈ����ũ ���� ������ �����
        //{
        //    Debug.Log("����");
        //    Destroy(gameObject);

        //}

    }
    public void FixedUpdate()
    {
       

        if (timeset == true)
        {
            cnt += Time.fixedDeltaTime;  // 1������ 0.02��
            if (0.00f < cnt && cnt < 0.03f)
            {
                destination1 = gameObject.transform.position;
            }
            if (0.05f < cnt && cnt < 0.08f)
            {
                destination2 = gameObject.transform.position;
            }
            if (0.1f < cnt && cnt < 0.13f)  // y�� ������ ������ ���̱� ����
            {
                angle = GetAngle(destination1, destination2);
            }
            if (1f < cnt && cnt < 1.02f)
            {
                /* Debug.Log("ù��° ��ǥ" + destination1 + "  �ι�° ��ǥ : " + destination2);*/ // y �� �� ���� ��

                //Debug.Log("����"+angle);   // �������� ��Ҵ� vector3 (0,0.5f,0) ���� x�� ��ġ�� �۰� �Ǹ� �а��̱⶧���� ������ �̻�����
                velo = Vector3.Distance(destination2, destination1) / 0.04f;
                //Debug.Log("�ӵ�" + velo);

                destination = Mathf.Pow(velo, 2) / 9.8f * Mathf.Sin((2 * angle) * Mathf.Deg2Rad);   // ���� ���� ��������

                testdestination = Mathf.Pow(velo, 2) * Mathf.Sin(angle) * Mathf.Cos(angle) / 9.8f;

                //Debug.Log("����" + destination + "  y�� ���� : " + angle);
                angle1 = CalculateAngle();  //x,z �� ����

                //Debug.Log("����" + testdestination);

                Angle_cofirm_on_Off.enabled = true;

                //Debug.Log("����" + CalculateAngle());

                //Debug.Log("x z �� ����" + CalculateAngle());
                Hit_location = gameObject.transform.forward;

                /* Debug.Log("Dest" + Dest + Vector3.Magnitude(posi) + rotate); */   // x
            }
        }

        Vector3 drawPoint = Hit_location + gameObject.transform.position;

        Debug.DrawLine(Hit_location, drawPoint, Color.red);

    }
}

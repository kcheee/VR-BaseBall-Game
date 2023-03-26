using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Effects : MonoBehaviour
{


    [SerializeField] AudioClip Bat_hit;

    public GameObject cartridge;
    AudioSource audioSource; // ����� ���Ǵ� 
    // Start is called before the first frame update

    void Start()
    {
        audioSource = GetComponent<AudioSource>();   // Audiosoure ������Ʈ ����� ����
    }

    public void OnCollisionEnter(Collision coll)
    {

        if (coll.collider.tag == "BaseBall")
        {

            GameObject gameObject = Instantiate(cartridge);
            gameObject.transform.position = coll.transform.position;
            audioSource.PlayOneShot(Bat_hit);   // Ÿ�ݽ� �Ҹ� ���
            Destroy(gameObject, 1);

            // �� ������ ��Ʈ�� ���Ͱ� ȣ��

        }

    }
}

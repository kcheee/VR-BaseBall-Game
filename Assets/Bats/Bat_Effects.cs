using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Effects : MonoBehaviour
{


    [SerializeField] AudioClip Bat_hit;

    public GameObject cartridge;
    AudioSource audioSource; // 재생에 사용되는 
    // Start is called before the first frame update

    void Start()
    {
        audioSource = GetComponent<AudioSource>();   // Audiosoure 컴포넌트 취득해 놓음
    }

    public void OnCollisionEnter(Collision coll)
    {

        if (coll.collider.tag == "BaseBall")
        {

            GameObject gameObject = Instantiate(cartridge);
            gameObject.transform.position = coll.transform.position;
            audioSource.PlayOneShot(Bat_hit);   // 타격시 소리 재생
            Destroy(gameObject, 1);

            // 공 맞을때 배트의 벡터값 호출

        }

    }
}

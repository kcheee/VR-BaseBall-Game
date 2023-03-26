using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour
{
    [SerializeField] GameObject ReThrowBall;
    [SerializeField] Transform position;
    public GameObject pitcher;
    float speed = 5f;
    private Animator animator;

    IEnumerator Rethrowing()
    {
        yield return new WaitForSecondsRealtime(2.03f);
        rethrow();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void SetCatch(bool receiveCatch) // Zone 스크립트에서 받아옴
    {
        if (receiveCatch == true)
        {
            animator.SetBool("Catch", true);
            StartCoroutine(Rethrowing());
            //Invoke("rethrow", 2.03f);
        }
    } 
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Catcher_throw") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            animator.SetBool("Catch", false);
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    void rethrow() // 공 복사하고 투수한테 신호보냄 
    {
        Instantiate(ReThrowBall, position.position, position.rotation);
    }
}

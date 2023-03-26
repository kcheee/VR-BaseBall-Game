using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Defende_State : MonoBehaviour
{
    Vector3 endpos = new Vector3(-42, 1, -40);
    // Start is called before the first frame update
    void Start()
    {
        transform.DOJump(endpos, 10, 1, 2f, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

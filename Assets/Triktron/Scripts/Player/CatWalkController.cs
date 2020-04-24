using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatWalkController : MonoBehaviour
{
    Animator m_Animator;

    public float IdleTime = 5;
    float CurrentIdleTime;

    public float RotateSpeed = 5;
    public float RunSpeed = 10;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * RunSpeed * Time.deltaTime);


        if (Input.GetAxis("Vertical") != 0)
        {
            m_Animator.ResetTrigger("Sit");
            m_Animator.ResetTrigger("Idle");


            m_Animator.SetTrigger("Walk");
            CurrentIdleTime = 0;
        }
        else if (CurrentIdleTime < IdleTime)
        {
            m_Animator.ResetTrigger("Walk");
            m_Animator.ResetTrigger("Sit");


            m_Animator.SetTrigger("Idle");
            CurrentIdleTime += Time.deltaTime;
        }
        else
        {
            m_Animator.ResetTrigger("Walk");
            m_Animator.ResetTrigger("Idle");


            m_Animator.SetTrigger("Sit");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    Animator m_Animator;
    public Velocety VelocetyScript;

    public float IdleTime = 2;
    float CurrentIdleTime;
    
    public AnimationCurve AnimationSpeedMultiplier = new AnimationCurve(new Keyframe(0f, 0f, 0f, 5f), new Keyframe(1f, 2f, 0f, 0f));

    public float MaxSpeed = 0.5f;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Animator.SetFloat("WalkSpeed", AnimationSpeedMultiplier.Evaluate(VelocetyScript.Speed));


        if (VelocetyScript.Speed > MaxSpeed)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float attackTimeMax;
    private Rigidbody charRigidbody;
    private Animator charAnimator;

    private bool IsAttack;
    private float attackTime;
    private bool bUpdateRotate;

    void Start()
    {
        charRigidbody = GetComponent<Rigidbody>();
        charAnimator = GetComponentInChildren<Animator>();
        IsAttack = false;
        bUpdateRotate = true;
    }

    void Update()
    {
        ProcessMove();
        ProcessAttack();
        ProcessRotate();
    }

    void ProcessMove()
    {
        if (IsAttack)
            return;

        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;

        //transform.position = transform.position + inputDir * moveSpeed * Time.deltaTime;
        charRigidbody.velocity = inputDir * moveSpeed;

        transform.LookAt(transform.position + inputDir);

        bool bPrevRun = charAnimator.GetBool("IsRun");
        charAnimator.SetBool("IsRun", inputDir != Vector3.zero);
        if (bPrevRun != (inputDir != Vector3.zero))
        {
            bUpdateRotate = true;
        }
    }

    void ProcessAttack()
    {
        if (Input.GetButton("Attack"))
            DoAttack();

        if(attackTime > 0.0f)
        {
            attackTime -= Time.deltaTime;
            if( attackTime <= 0.0f )
            {
                attackTime = 0.0f;
                AttackEnd();
            }
        }
    }

    void ProcessRotate()
    {
        if( !IsAttack && bUpdateRotate)
        {
            if (!charAnimator.GetBool("IsRun"))
            {
                Vector3 rotateAngle = transform.rotation.eulerAngles;
                rotateAngle.y += 47.0f;
                transform.rotation = Quaternion.Euler(rotateAngle);
            }
            bUpdateRotate = false;
        }
    }

    void DoAttack()
    {
        if (IsAttack)
            return;

        Debug.Log("DoAttack");

        if (charAnimator.GetBool("IsRun"))
        {
            Vector3 rotateAngle = transform.rotation.eulerAngles;
            rotateAngle.y += 47.0f;
            transform.rotation = Quaternion.Euler(rotateAngle);
        }

        charAnimator.SetTrigger("Attack1");
        charRigidbody.velocity = Vector3.zero;
        IsAttack = true;
        attackTime = attackTimeMax;
    }

    public void AttackEnd()
    {
        Debug.Log("AttackEnd");


        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");
        if (hAxis != 0.0f || vAxis != 0.0f )
        {
            Vector3 rotateAngle = transform.rotation.eulerAngles;
            rotateAngle.y -= 47.0f;
            transform.rotation = Quaternion.Euler(rotateAngle);
        }

        IsAttack = false;
    }
}

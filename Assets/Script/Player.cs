using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody charRigidbody;
    private Animator charAnimator;

    private bool IsAttack;

    void Start()
    {
        charRigidbody = GetComponent<Rigidbody>();
        charAnimator = GetComponentInChildren<Animator>();
        IsAttack = false;
    }

    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;

        //transform.position = transform.position + inputDir * moveSpeed * Time.deltaTime;
        charRigidbody.velocity = inputDir * moveSpeed;

        transform.LookAt(transform.position + inputDir);

        charAnimator.SetBool("IsRun", inputDir != Vector3.zero);

        if (Input.GetButton("Attack"))
            DoAttack();
    }

    void DoAttack()
    {
        charAnimator.SetBool("IsAttack", true);
        charRigidbody.velocity = Vector3.zero;
    }
}

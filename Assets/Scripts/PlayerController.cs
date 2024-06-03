using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private bool isShooting = false;
    private bool wasWalkingBeforeShooting = false;

    void Update()
    {
        bool isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        animator.SetBool("isWalk", isWalking);

        if (!isShooting)
        {
            animator.SetBool("isWalk", isWalking);
        }

        if (Input.GetMouseButton(0))
        {
            StartCoroutine(Shoot());
        }

        if(!isWalking)
        {
            animator.SetTrigger("isIdle");
        }

    }
    IEnumerator Shoot()
    {
        isShooting = true;
        while (Input.GetMouseButton(0))
        {
            animator.SetTrigger("isShoot");
            Debug.Log("Shooting");

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
        isShooting = false;

        animator.SetTrigger("isIdle");
        Debug.Log("Shoot finished");
    }
}

public enum CharacterState
{
    Idle,
    Running,
    Shooting
}
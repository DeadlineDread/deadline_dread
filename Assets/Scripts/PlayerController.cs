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
            if (isWalking)
            {
                wasWalkingBeforeShooting = true;
            }
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            StartCoroutine(Shoot());
        }

        if (animator.GetBool("isWalk"))
        {
            Debug.Log("Walk");
        }

        else
        {
            animator.SetTrigger("isIdle");
        }

    }
    IEnumerator Shoot()
    {
        isShooting = true;
        animator.SetTrigger("isShoot");
        Debug.Log("Shooting");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        isShooting = false;
        animator.SetBool("isWalk", wasWalkingBeforeShooting);

        Debug.Log("shoot");
    }
}

public enum CharacterState
{
    Idle,
    Running,
    Shooting
}
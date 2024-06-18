using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private bool isShooting = false;
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        bool isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        animator.SetBool("isWalk", isWalking);

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Shoot());
        }

        if (!isWalking && !isShooting)
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

            // 총알 생성 및 발사
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * 1000f); // firePoint의 forward 방향으로 힘을 가함

            // 발사 애니메이션이 끝날 때까지 대기
            yield return new WaitForSeconds(0.3f);

            if (!Input.GetMouseButton(0))
            {
                break;
            }
        }

        isShooting = false;
        animator.SetTrigger("isIdle");
        Debug.Log("Shoot finished");
    }
}

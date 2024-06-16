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

            // �Ѿ� ���� �� �߻�
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * 1000f); // firePoint�� forward �������� ���� ����

            // �߻� �ִϸ��̼��� ���� ������ ���
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

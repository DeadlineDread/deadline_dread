using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator; // �ִϸ����� ������Ʈ
    public Transform firePoint; // �߻� ��ġ
    public GameObject bulletPrefab; // �߻�ü ������
    public bool isRunning = false; // �޸��� ������ ����
    public bool isShooting = false; // �߻� ������ ����

    void Update()
    {
        // �÷��̾��� �����ӿ� ���� walk �Ǵ� run �ִϸ��̼� ����
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool isWalking = Mathf.Abs(moveHorizontal) > 0 || Mathf.Abs(moveVertical) > 0;
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && isWalking;

        // �̵� ���¿� ���� �ִϸ��̼� ����
        if (isSprinting)
        {
            animator.SetBool("isRun", true);
            animator.SetBool("isWalk", false);
        }
        else if (isWalking)
        {
            animator.SetBool("isWalk", true);
            animator.SetBool("isRun", false);
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
        }

        // �߻� ���¿� ���� shoot �ִϸ��̼� ����
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            animator.SetBool("isShoot", false);
        }

        // ���� ���¿� ���� dead �ִϸ��̼� ���� (���÷� health�� 0�� �� ���� ���·� ����)
        int health = 100; // ���÷� 100���� ����
        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            // �߰����� ó��: ���� ���¿��� �ʿ��� ���� ����
        }
    }

    void Shoot()
    {
        isShooting = true;
        animator.SetTrigger("isShoot");

        // �Ѿ� ���� �� �߻�
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * 1000f);
    }
}

using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator; // 애니메이터 컴포넌트
    public Transform firePoint; // 발사 위치
    public GameObject bulletPrefab; // 발사체 프리팹
    public bool isRunning = false; // 달리는 중인지 여부
    public bool isShooting = false; // 발사 중인지 여부

    void Update()
    {
        // 플레이어의 움직임에 따라 walk 또는 run 애니메이션 적용
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool isWalking = Mathf.Abs(moveHorizontal) > 0 || Mathf.Abs(moveVertical) > 0;
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && isWalking;

        // 이동 상태에 따라 애니메이션 설정
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

        // 발사 상태에 따라 shoot 애니메이션 설정
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            animator.SetBool("isShoot", false);
        }

        // 죽음 상태에 따라 dead 애니메이션 설정 (예시로 health가 0일 때 죽음 상태로 간주)
        int health = 100; // 예시로 100으로 설정
        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            // 추가적인 처리: 죽음 상태에서 필요한 로직 수행
        }
    }

    void Shoot()
    {
        isShooting = true;
        animator.SetTrigger("isShoot");

        // 총알 생성 및 발사
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * 1000f);
    }
}

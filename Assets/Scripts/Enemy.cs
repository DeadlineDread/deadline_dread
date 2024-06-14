using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator; // 애니메이터 컴포넌트
    public Transform player; // 플레이어의 위치
    public float attackRange = 2f; // 공격 가능한 범위
    public float detectionRange = 10f; // 플레이어 감지 범위
    public float timeBetweenAttacks = 0.05f; // 공격 간격
    public int attackDamage = 15;   // 공격 데미지
    public int health = 100; // 체력
    private float attackTimer; // 다음 공격까지의 시간을 추적
    private bool isDead = false; // 적이 죽었는지 여부
    private bool isGetHit = false; // 적이 피격당했는지 여부'

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // 태그로 플레이어 오브젝트를 찾아 할당
        }

        attackTimer = timeBetweenAttacks; // 초기화: 공격 타이머를 설정된 간격으로 초기화
    }

    void Update()
    {
        if (isDead) // 적이 죽었으면 아무 것도 하지 않음
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // 적과 플레이어 간의 거리 계산

        if (isGetHit) // 적이 피격당했으면 아무 것도 하지 않음
            return;

        if (distanceToPlayer <= attackRange) // 플레이어가 공격 가능한 범위 안에 있으면
        {
            AttackPlayer(); // 공격 실행
        }

        else if (distanceToPlayer <= detectionRange) // 플레이어가 감지 범위 안에 있으면
        {
            animator.SetTrigger("isRoar");
            RunTowardsPlayer();
            //StartCoroutine(ActivateRunAfterDelay()); // 3초 후에 달리기 애니메이션 활성화
        }

        else // 플레이어가 감지 범위 밖에 있으면
        {
            animator.SetBool("isRun", false); // 이동 애니메이션 비활성화
            animator.SetTrigger("isIdle"); // 이동 애니메이션 비활성화
        }
    }

    /*IEnumerator ActivateRunAfterDelay()
    {
        animator.SetTrigger("isRoar");
        yield return new WaitForSeconds(2f); // 3초 대기
        RunTowardsPlayer(); // 달리기 애니메이션 활성화
    }*/

    void RunTowardsPlayer()
    {
        animator.SetBool("isRun", true); // 이동 애니메이션 활성화
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * 6f); // 플레이어 쪽으로 이동
        transform.LookAt(player); // 플레이어를 바라봄
    }

    void AttackPlayer()
    {
        if (isGetHit) // 적이 피격당했으면 공격 중지
        {
            animator.ResetTrigger("isAttack");
            return;
        }

        animator.SetBool("isRun", false); // 이동 애니메이션 비활성화

        if (attackTimer <= 0f) // 공격 타이머가 만료되면
        {
            int attackIndex = Random.Range(0, 3); // 0, 1, 2 중 랜덤한 값을 얻어옴
            animator.SetInteger("AttackIndex", attackIndex); // AttackIndex 파라미터에 랜덤한 값을 설정하여 랜덤한 공격 애니메이션을 재생
            animator.SetTrigger("isAttack");
            attackTimer = timeBetweenAttacks; // 다음 공격을 위해 타이머 재설정

            // 플레이어에게 데미지 입힘
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
        attackTimer -= Time.deltaTime; // 타이머 감소
    }

    public void TakeDamage(int damage)
    {
        if (isDead) // 적이 이미 죽었으면 아무 것도 하지 않음
            return;

        if (!isGetHit) // 적이 피격 상태라면
        {
            animator.SetBool("isRun", false);
            health -= damage; // 적의 체력 감소
            GetHit(); // 피격 애니메이션 재생
        }

        if (health <= 0 && !isDead) // 적의 체력이 0 이하이고 아직 죽지 않았다면
        {
            Die(); // 사망 처리
        }
    }

    void GetHit()
    {
        isGetHit = true; // 피격 상태로 설정
        animator.SetTrigger("isGetHit"); // 피격 애니메이션 재생
        Debug.Log("Enemy GetHit");
        StartCoroutine(ResetHit()); // 피격 상태를 일정 시간 후에 해제
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // 현재 재생 중인 애니메이션의 길이만큼 대기
        isGetHit = false; // 피격 상태 해제
    }

    void Die()
    {
        isDead = true; // 죽음 상태로 설정
        animator.SetBool("isDead", true); // 죽음 애니메이션 재생
        Debug.Log("Enemy died");
    }
}

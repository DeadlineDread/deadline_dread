using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Animator animator; // 애니메이터 컴포넌트
    public Transform player; // 플레이어의 위치
    public float attackRange = 2f; // 공격 가능한 범위
    public float detectionRange = 10f; // 플레이어 감지 범위
    public float timeBetweenAttacks = 0.5f; // 공격 간격 (조정 가능)
    public float moveSpeed = 1f;
    public int attackDamage = 15; // 공격 데미지
    public int maxHealth = 100; // 체력
    public int currentHealth = 0;
    private float attackTimer; // 다음 공격까지의 시간을 추적
    private bool isDead = false; // 적이 죽었는지 여부
    private bool isGetHit = false; // 적이 피격당했는지 여부
    private bool isRun = false;
    private bool isRoar = false;
    private Rigidbody rb; // Rigidbody 컴포넌트

    // 체력바 오브젝트
    public EnemyHealthBar healthBar;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform; // 태그로 플레이어 오브젝트를 찾아 할당

        // player를 찾는 과정에서 null인 경우 예외 처리
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
                player = playerObject.transform;
        }

        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }


        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
        attackTimer = timeBetweenAttacks; // 초기화: 공격 타이머를 설정된 간격으로 초기화
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) // 적이 죽었으면 아무 것도 하지 않음
            return;

        // 발사체와의 충돌 감지
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject); // 발사체 파괴
        }
    }

    void Update()
    {
        if (isDead) // 적이 죽었으면 아무 것도 하지 않음
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // 적과 플레이어 간의 거리 계산

        if (isGetHit) // 적이 피격당했으면 아무 것도 하지 않음
            return;

        if (distanceToPlayer <= detectionRange) // 플레이어가 감지 범위 안에 있으면
        {
            // 현재 roar 애니메이션이 재생 중이 아니라면 roar 애니메이션 실행
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("roar"))
            {
                animator.SetTrigger("isRoar");
                StartCoroutine(StartRunningAfterRoar());
            }
        }
        else // 플레이어가 감지 범위 밖에 있으면
        {
            isRun = false;
            animator.SetBool("isRun", false); // 이동 애니메이션 비활성화
            animator.SetTrigger("isIdle"); // 대기 애니메이션 활성화
        }

        if (distanceToPlayer <= attackRange && attackTimer <= 0f) // 공격 범위 안에 있고 공격 타이머가 만료되었을 때
        {
            AttackPlayer(); // 공격 실행
            attackTimer = timeBetweenAttacks; // 다음 공격을 위해 타이머 재설정
        }
        attackTimer -= Time.deltaTime; // 타이머 감소
    }

    IEnumerator StartRunningAfterRoar()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // 특정 시간만큼 대기
        RunTowardsPlayer(); // 플레이어 쪽으로 달려들기
        isRoar = true;
    }

    void RunTowardsPlayer()
    {
        if (isGetHit && isRoar) // 적이 피격당했으면 아무 것도 하지 않음
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            isRun = false;
            animator.SetBool("isRun", isRun); // 이동 애니메이션 비활성화
            AttackPlayer(); // 공격 실행
        }
        else
        {
            isRun = true;
            animator.SetBool("isRun", isRun); // 이동 애니메이션 활성화
            animator.ResetTrigger("isRoar"); // roar 트리거 리셋
            isRoar = true;

            // 플레이어 쪽으로 부드럽게 이동
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z); // x, z 축 고정
            transform.LookAt(targetPosition); // 플레이어를 바라봄
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed); // 부드러운 이동
        }
    }


    void AttackPlayer()
    {
        if (isGetHit) // 적이 피격당했으면 공격 중지
        {
            EndAttack();
            return;
        }
        animator.SetBool("isRun", false); // 이동 애니메이션 비활성화

        int attackIndex = Random.Range(0, 3); // 0, 1, 2 중 랜덤한 값을 얻어옴

        if (attackTimer <= 0f) // 공격 타이머가 만료되면
        {
            animator.SetInteger("AttackIndex", attackIndex); // AttackIndex 파라미터에 랜덤한 값을 설정하여 랜덤한 공격 애니메이션을 재생
            animator.SetTrigger("isAttack");
            attackTimer = timeBetweenAttacks; // 다음 공격을 위해 타이머 재설정

            StartCoroutine(PerformAttack(attackIndex)); // 해당 공격 코루틴 시작
        }
        attackTimer -= Time.deltaTime; // 타이머 감소
    }

    IEnumerator PerformAttack(int attackIndex)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // 공격 애니메이션 길이만큼 대기

        // 플레이어에게 데미지 입힘
        player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);

        // 공격이 끝나면 다시 공격할 수 있도록
        attackTimer = 0f; // 공격 타이머를 즉시 0으로 설정하여 다음 공격 준비
        AttackPlayer(); // 공격을 다시 시작
    }

    public void TakeDamage(int damage)
    {
        if (isDead) // 적이 이미 죽었으면 아무 것도 하지 않음
            return;

        currentHealth -= damage; // 적의 체력 감소

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth); // 체력 바 업데이트
        }

        if (currentHealth <= 0 && !isDead) // 적의 체력이 0 이하이고 아직 죽지 않았다면
        {
            Die(); // 사망 처리
        }
        else
        {
            GetHit(); // 피격 애니메이션 재생
        }
    }

    void GetHit()
    {
        if (isRun)
        {
            animator.SetBool("isRun", false);
        }
        isGetHit = true; // 피격 상태로 설정
        animator.SetTrigger("isGetHit"); // 피격 애니메이션 재생
        Debug.Log("GetHit");
        StartCoroutine(ResetHit()); // 피격 상태를 일정 시간 후에 해제
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.5f); // 현재 재생 중인 애니메이션의 길이만큼 대기
        isGetHit = false; // 피격 상태 해제
    }

    void Die()
    {
        isDead = true; // 죽음 상태로 설정
        Debug.Log("Dead");
        animator.SetBool("isRun", false);
        StopAllCoroutines(); // 모든 코루틴 중지
        healthBar.gameObject.SetActive(false);
        animator.SetBool("isDead", true); // 죽음 애니메이션 재생
        rb.isKinematic = true; // 사망 후 물리 계산 멈춤
        GetComponent<CapsuleCollider>().enabled = false; // 콜라이더 비활성화
        //Destroy(gameObject, 2f); // 2초 후에 객체 파괴
    }

    void EndAttack()
    {
        animator.SetBool("isAttack", false); // 공격 애니메이션 종료
    }
}

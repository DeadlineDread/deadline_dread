using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Animator animator; // �ִϸ����� ������Ʈ
    public Transform player; // �÷��̾��� ��ġ
    public float attackRange = 2f; // ���� ������ ����
    public float detectionRange = 10f; // �÷��̾� ���� ����
    public float timeBetweenAttacks = 0.5f; // ���� ���� (���� ����)
    public float moveSpeed = 1f;
    public int attackDamage = 15; // ���� ������
    public int health = 100; // ü��
    private float attackTimer; // ���� ���ݱ����� �ð��� ����
    private bool isDead = false; // ���� �׾����� ����
    private bool isGetHit = false; // ���� �ǰݴ��ߴ��� ����
    private bool isRun = false;
    private Rigidbody rb; // Rigidbody ������Ʈ

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform; // �±׷� �÷��̾� ������Ʈ�� ã�� �Ҵ�

        rb = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ ��������

        attackTimer = timeBetweenAttacks; // �ʱ�ȭ: ���� Ÿ�̸Ӹ� ������ �������� �ʱ�ȭ
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) // ���� �׾����� �ƹ� �͵� ���� ����
            return;

        // �߻�ü���� �浹 ����
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject); // �߻�ü �ı�
        }
    }

    void Update()
    {
        if (isDead) // ���� �׾����� �ƹ� �͵� ���� ����
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // ���� �÷��̾� ���� �Ÿ� ���

        if (isGetHit) // ���� �ǰݴ������� �ƹ� �͵� ���� ����
            return;

        if (distanceToPlayer <= detectionRange) // �÷��̾ ���� ���� �ȿ� ������
        {
            // ���� roar �ִϸ��̼��� ��� ���� �ƴ϶�� roar �ִϸ��̼� ����
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("roar"))
            {
                animator.SetTrigger("isRoar");
                StartCoroutine(StartRunningAfterRoar());
            }
        }
        else // �÷��̾ ���� ���� �ۿ� ������
        {
            isRun = false;
            animator.SetBool("isRun", false); // �̵� �ִϸ��̼� ��Ȱ��ȭ
            animator.SetTrigger("isIdle"); // ��� �ִϸ��̼� Ȱ��ȭ
        }

        if (distanceToPlayer <= attackRange && attackTimer <= 0f) // ���� ���� �ȿ� �ְ� ���� Ÿ�̸Ӱ� ����Ǿ��� ��
        {
            AttackPlayer(); // ���� ����
            attackTimer = timeBetweenAttacks; // ���� ������ ���� Ÿ�̸� �缳��
        }
        attackTimer -= Time.deltaTime; // Ÿ�̸� ����
    }

    IEnumerator StartRunningAfterRoar()
    {
        yield return new WaitForSeconds(0.8f); // Ư�� �ð���ŭ ���
        isRun = true;
        RunTowardsPlayer(); // �÷��̾� ������ �޷����
    }

    void RunTowardsPlayer()
    {
        if (isGetHit) // ���� �ǰݴ������� �ƹ� �͵� ���� ����
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            isRun = false;
            animator.SetBool("isRun", isRun); // �̵� �ִϸ��̼� ��Ȱ��ȭ
            AttackPlayer(); // ���� ����
        }
        else
        {
            isRun = true;
            animator.SetBool("isRun", isRun); // �̵� �ִϸ��̼� Ȱ��ȭ
            animator.ResetTrigger("isRoar"); // roar Ʈ���� ����

            // �÷��̾� ������ �ε巴�� �̵�
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z); // x, z �� ����
            transform.LookAt(targetPosition); // �÷��̾ �ٶ�
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed); // �ε巯�� �̵�
        }
    }


    void AttackPlayer()
    {
        if (isGetHit) // ���� �ǰݴ������� ���� ����
        {
            EndAttack();
            return;
        }
        animator.SetBool("isRun", false); // �̵� �ִϸ��̼� ��Ȱ��ȭ

        int attackIndex = Random.Range(0, 3); // 0, 1, 2 �� ������ ���� ����

        if (attackTimer <= 0f) // ���� Ÿ�̸Ӱ� ����Ǹ�
        {
            animator.SetInteger("AttackIndex", attackIndex); // AttackIndex �Ķ���Ϳ� ������ ���� �����Ͽ� ������ ���� �ִϸ��̼��� ���
            animator.SetTrigger("isAttack");
            attackTimer = timeBetweenAttacks; // ���� ������ ���� Ÿ�̸� �缳��

            StartCoroutine(PerformAttack(attackIndex)); // �ش� ���� �ڷ�ƾ ����
        }
        attackTimer -= Time.deltaTime; // Ÿ�̸� ����
    }

    IEnumerator PerformAttack(int attackIndex)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // ���� �ִϸ��̼� ���̸�ŭ ���

        // �÷��̾�� ������ ����
        player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);

        // ������ ������ �ٽ� ������ �� �ֵ���
        attackTimer = 0f; // ���� Ÿ�̸Ӹ� ��� 0���� �����Ͽ� ���� ���� �غ�
        AttackPlayer(); // ������ �ٽ� ����
    }

    public void TakeDamage(int damage)
    {
        if (isDead) // ���� �̹� �׾����� �ƹ� �͵� ���� ����
            return;

        health -= damage; // ���� ü�� ����

        if (health <= 0 && !isDead) // ���� ü���� 0 �����̰� ���� ���� �ʾҴٸ�
        {
            Die(); // ��� ó��
        }
        else
        {
            GetHit(); // �ǰ� �ִϸ��̼� ���
        }
    }

    void GetHit()
    {
        if (isRun)
        {
            animator.SetBool("isRun", false);
        }
        isGetHit = true; // �ǰ� ���·� ����
        animator.SetTrigger("isGetHit"); // �ǰ� �ִϸ��̼� ���
        Debug.Log("GetHit");
        StartCoroutine(ResetHit()); // �ǰ� ���¸� ���� �ð� �Ŀ� ����
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.5f); // ���� ��� ���� �ִϸ��̼��� ���̸�ŭ ���
        isGetHit = false; // �ǰ� ���� ����
    }

    void Die()
    {
        isDead = true; // ���� ���·� ����
        Debug.Log("Dead");
        animator.SetBool("isRun", false);
        StopAllCoroutines(); // ��� �ڷ�ƾ ����
        animator.SetBool("isDead", true); // ���� �ִϸ��̼� ���
        rb.isKinematic = true; // ��� �� ���� ��� ����
        GetComponent<CapsuleCollider>().enabled = false; // �ݶ��̴� ��Ȱ��ȭ
        //Destroy(gameObject, 2f); // 2�� �Ŀ� ��ü �ı�
    }

    void EndAttack()
    {
        animator.SetBool("isAttack", false); // ���� �ִϸ��̼� ����
    }
}

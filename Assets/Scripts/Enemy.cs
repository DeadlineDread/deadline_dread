using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator; // �ִϸ����� ������Ʈ
    public Transform player; // �÷��̾��� ��ġ
    public float attackRange = 2f; // ���� ������ ����
    public float detectionRange = 10f; // �÷��̾� ���� ����
    public float timeBetweenAttacks = 0.05f; // ���� ����
    public int attackDamage = 15;   // ���� ������
    public int health = 100; // ü��
    private float attackTimer; // ���� ���ݱ����� �ð��� ����
    private bool isDead = false; // ���� �׾����� ����
    private bool isGetHit = false; // ���� �ǰݴ��ߴ��� ����'

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // �±׷� �÷��̾� ������Ʈ�� ã�� �Ҵ�
        }

        attackTimer = timeBetweenAttacks; // �ʱ�ȭ: ���� Ÿ�̸Ӹ� ������ �������� �ʱ�ȭ
    }

    void Update()
    {
        if (isDead) // ���� �׾����� �ƹ� �͵� ���� ����
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // ���� �÷��̾� ���� �Ÿ� ���

        if (isGetHit) // ���� �ǰݴ������� �ƹ� �͵� ���� ����
            return;

        if (distanceToPlayer <= attackRange) // �÷��̾ ���� ������ ���� �ȿ� ������
        {
            AttackPlayer(); // ���� ����
        }

        else if (distanceToPlayer <= detectionRange) // �÷��̾ ���� ���� �ȿ� ������
        {
            animator.SetTrigger("isRoar");
            RunTowardsPlayer();
            //StartCoroutine(ActivateRunAfterDelay()); // 3�� �Ŀ� �޸��� �ִϸ��̼� Ȱ��ȭ
        }

        else // �÷��̾ ���� ���� �ۿ� ������
        {
            animator.SetBool("isRun", false); // �̵� �ִϸ��̼� ��Ȱ��ȭ
            animator.SetTrigger("isIdle"); // �̵� �ִϸ��̼� ��Ȱ��ȭ
        }
    }

    /*IEnumerator ActivateRunAfterDelay()
    {
        animator.SetTrigger("isRoar");
        yield return new WaitForSeconds(2f); // 3�� ���
        RunTowardsPlayer(); // �޸��� �ִϸ��̼� Ȱ��ȭ
    }*/

    void RunTowardsPlayer()
    {
        animator.SetBool("isRun", true); // �̵� �ִϸ��̼� Ȱ��ȭ
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * 6f); // �÷��̾� ������ �̵�
        transform.LookAt(player); // �÷��̾ �ٶ�
    }

    void AttackPlayer()
    {
        if (isGetHit) // ���� �ǰݴ������� ���� ����
        {
            animator.ResetTrigger("isAttack");
            return;
        }

        animator.SetBool("isRun", false); // �̵� �ִϸ��̼� ��Ȱ��ȭ

        if (attackTimer <= 0f) // ���� Ÿ�̸Ӱ� ����Ǹ�
        {
            int attackIndex = Random.Range(0, 3); // 0, 1, 2 �� ������ ���� ����
            animator.SetInteger("AttackIndex", attackIndex); // AttackIndex �Ķ���Ϳ� ������ ���� �����Ͽ� ������ ���� �ִϸ��̼��� ���
            animator.SetTrigger("isAttack");
            attackTimer = timeBetweenAttacks; // ���� ������ ���� Ÿ�̸� �缳��

            // �÷��̾�� ������ ����
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
        attackTimer -= Time.deltaTime; // Ÿ�̸� ����
    }

    public void TakeDamage(int damage)
    {
        if (isDead) // ���� �̹� �׾����� �ƹ� �͵� ���� ����
            return;

        if (!isGetHit) // ���� �ǰ� ���¶��
        {
            animator.SetBool("isRun", false);
            health -= damage; // ���� ü�� ����
            GetHit(); // �ǰ� �ִϸ��̼� ���
        }

        if (health <= 0 && !isDead) // ���� ü���� 0 �����̰� ���� ���� �ʾҴٸ�
        {
            Die(); // ��� ó��
        }
    }

    void GetHit()
    {
        isGetHit = true; // �ǰ� ���·� ����
        animator.SetTrigger("isGetHit"); // �ǰ� �ִϸ��̼� ���
        Debug.Log("Enemy GetHit");
        StartCoroutine(ResetHit()); // �ǰ� ���¸� ���� �ð� �Ŀ� ����
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // ���� ��� ���� �ִϸ��̼��� ���̸�ŭ ���
        isGetHit = false; // �ǰ� ���� ����
    }

    void Die()
    {
        isDead = true; // ���� ���·� ����
        animator.SetBool("isDead", true); // ���� �ִϸ��̼� ���
        Debug.Log("Enemy died");
    }
}

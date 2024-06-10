using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100; // �� ĳ������ ü��
    public Animator animator;
    public Transform player;
    public float atttackRange = 2f;
    public float detectionRange = 10f;
    public float timeBetweenAttacks = 2f;
    private float attackTimer;
    private bool isDead = false;
    private bool isHit = false;

    private void Start()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // ������ ��ŭ ü�� ����

        // ���⿡ ü�� ���ҿ� ���� ó�� �߰� ����
    }

    public void Die()
    {
        // ��� ó�� �ִϸ��̼� ��� �Ǵ� ���� ������Ʈ ���� ���� ����
        isDead = true;
        int deathIndex = Random.Range(0, 2);
        animator.SetInteger("DeathIndex", deathIndex);
        animator.SetBool("isDead", true);
    }
}

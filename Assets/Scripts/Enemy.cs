using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100; // 적 캐릭터의 체력
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
        health -= damage; // 데미지 만큼 체력 감소

        // 여기에 체력 감소에 따른 처리 추가 가능
    }

    public void Die()
    {
        // 사망 처리 애니메이션 재생 또는 게임 오브젝트 제거 등을 수행
        isDead = true;
        int deathIndex = Random.Range(0, 2);
        animator.SetInteger("DeathIndex", deathIndex);
        animator.SetBool("isDead", true);
    }
}
